using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

namespace FinalProject.patients
{
	public partial class patients_search : UCPageType
	{
		gridviewcode grd = new gridviewcode();
		public encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{
			txtSrchPatientID.Text = Convert.ToString(Session["srchPatID"]);
			txtSrchLastName.Text = Convert.ToString(Session["srchLName"]);
			txtSrchFirstName.Text = Convert.ToString(Session["srchFName"]);

			// Establish GridView event handlers
			grdPatients.PageIndexChanging += new GridViewPageEventHandler(grdPatients_PageIndexChanging);
			grdPatients.Sorting += new GridViewSortEventHandler(grdPatients_Sorting);

			if (Convert.ToString(Session["srchPatHasSearched"]) == "true")
			{
				PopulateGridView();
			}

			// Keep delete confirmation loaded on partial postback, if already loaded
			if (Convert.ToString(Session["delConfirmActive"]) == "true")
			{
				switch (Convert.ToString(Session["delConfirmType"]))
				{
					case "single":
						Delete_Click(sender, new CommandEventArgs("delete", Convert.ToString(Session["delPatientID"])));
						break;
					case "multi":
						btnDeleteChecked_Click(sender, e);
						break;
				}
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				SrchPatSaveSession();

				Cache.Remove("srchPatGridDataView");
				PopulateGridView();
			}
			catch
			{
				RegisterAlertScript(new CommandEventArgs("script", "Failed to load patient data"));
			}
		}

		protected void PopulateGridView()
		{
			// Get session values
			string patientID = Convert.ToString(Session["srchPatID"]);
			string lastName = Convert.ToString(Session["srchLName"]);
			string firstName = Convert.ToString(Session["srchFName"]);

			// Initiate data tier
			LouisDataTier aPatient = new LouisDataTier();
			DataSet patientData = new DataSet();
			patientData = aPatient.SearchPatients(patientID, lastName, firstName);

			// Populate datagrid with dataset
			grdPatients.DataSource = patientData.Tables[0];
			grdPatients.DataBind();

			Session["srchPatGridData"] = patientData;
			Session["srchPatHasSearched"] = "true";

			// Add data to cache if cache is empty
			if (Cache["srchPatGridDataView"] == null)
			{
				Cache.Add("srchPatGridDataView", new DataView(patientData.Tables[0]),
					null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes(10),
					System.Web.Caching.CacheItemPriority.Default, null);
			}
		}

		public void View_Click(object sender, CommandEventArgs e)
		{
			ViewButtonClicked(e);
		}

		public void Edit_Click(object sender, CommandEventArgs e)
		{
			EditButtonClicked(e);
		}

		public void Delete_Click(object sender, CommandEventArgs e)
		{
			// Load delete confirmation control
			Session["delPatientID"] = e.CommandArgument.ToString();
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx");
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "single";
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);

			// Assign event handlers
			ucDelConfirm.confirmed += new CommandEventHandler(DeleteConfirmed);
			ucDelConfirm.cancelled += new Action(RemoveDeleteConfirmation);
		}

		public void btnDeleteChecked_Click(object sender, EventArgs e)
		{
			// Load delete confirmation control
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx");
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "multi";
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);

			// Assign event handlers
			ucDelConfirm.confirmed += new CommandEventHandler(DeleteConfirmed);
			ucDelConfirm.cancelled += new Action(RemoveDeleteConfirmation);
		}

		public void DelRegisterAlertScript(object sender, CommandEventArgs e)
		{
			RegisterAlertScript(e);
		}

		public void DeleteConfirmed(object sender, CommandEventArgs e)
		{
			string test = grdPatients.Rows[1].Cells[1].Text;

			LouisDataTier dataTier = new LouisDataTier();
			bool deleteSuccess = false;
			string plural = "";

			switch (e.CommandArgument.ToString())
			{
				case "single": // Deleting a single record
					string singlePatientID = cipher.Decrypt(Convert.ToString(Session["delPatientID"]));
					deleteSuccess = dataTier.DeletePatient(singlePatientID);
					break;

				case "multi": // Deleting all checked records
					plural = "s";
					if (grdPatients.Rows.Count > 0)
					{
						foreach (GridViewRow row in grdPatients.Rows)
						{
							CheckBox chkSelectPatID = new CheckBox();
							chkSelectPatID = (CheckBox)row.FindControl("chkPatientID");

							if (chkSelectPatID.Checked)
							{
								string multiPatientID = row.Cells[1].Text;
								deleteSuccess = dataTier.DeletePatient(multiPatientID);
							}
						}
					}
					break;
			}

			if (deleteSuccess == true)
			{
				RegisterAlertScript(new CommandEventArgs("script", "Patient record" + plural + " deleted successfully"));
			}
			else
			{
				RegisterAlertScript(new CommandEventArgs("script", "Failed to delete patient record" + plural));
			}

			// Remove delete confirmation message and update GridView
			RemoveDeleteConfirmation();
			PopulateGridView();
		}

		public void RemoveDeleteConfirmation()
		{
			// Remove delete confirmation message
			pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();
			Session["delConfirmActive"] = false;
		}

		protected void grdPatients_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		protected void grdPatients_Sorting(object sender, GridViewSortEventArgs e)
		{
			string newSortExpr = e.SortExpression;
			string sortDir = Convert.ToString(ViewState["srchPatSortDir"]);
			string oldSortExpr = Convert.ToString(ViewState["srchPatSortExpr"]);
			DataView patientData = (DataView)Cache["srchPatGridDataView"];

			var sortedPatientData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, patientData);
			grdPatients.DataSource = sortedPatientData.Item1;
			ViewState["srchPatSortDir"] = sortedPatientData.Item2;
			ViewState["srchPatSortExpr"] = newSortExpr;
			grdPatients.DataBind();
		}

		public void SrchPatSaveSession()
		{
			Session["srchPatID"] = txtSrchPatientID.Text;
			Session["srchLName"] = txtSrchLastName.Text;
			Session["srchFName"] = txtSrchFirstName.Text;
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			SrchPatSaveSession();
			txtSrchPatientID.Text = "test";
			_default parentPage = new _default();
		}

		protected void btnDeleteConfirm_Click(object sender, EventArgs e)
		{

		}
	}
}