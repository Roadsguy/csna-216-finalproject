using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

namespace FinalProject.physicians
{
	public partial class physicians_search : UCPageType
	{
		public encryption cipher = new encryption();
		gridviewcode grd = new gridviewcode();

		protected void Page_Load(object sender, EventArgs e)
		{
			// Put previously typed values back into text boxes, if any (used on return from vieweditadd page)
			txtSrchPhysicianID.Text = Convert.ToString(Session["srchPhysID"]);
			txtSrchLastName.Text = Convert.ToString(Session["srchLName"]);
			txtSrchFirstName.Text = Convert.ToString(Session["srchFName"]);

			// Establish GridView event handlers
			grdPhysicians.PageIndexChanging += new GridViewPageEventHandler(grdPhysicians_PageIndexChanging);
			grdPhysicians.Sorting += new GridViewSortEventHandler(grdPhysicians_Sorting);

			if (Convert.ToString(Session["srchPhysHasSearched"]) == "true") // Check if search already performed
			{
				// Repopulate GridView
				PopulateGridView();
			}

			// Keep delete confirmation loaded on partial postback, if already loaded
			if (Convert.ToString(Session["delConfirmActive"]) == "true")
			{
				switch (Convert.ToString(Session["delConfirmType"])) // Check whether single or multi delete
				{
					case "single":
						// Load single delete confirmation
						Delete_Click(sender, new CommandEventArgs("delete", Convert.ToString(Session["delPhysicianID"])));
						break;
					case "multi":
						// Load multi delete confirmation
						btnDeleteChecked_Click(sender, e);
						break;
				}
			}
		}

		protected void grdPhysicians_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			int pageNum = e.NewPageIndex;
			Paging(pageNum);
		}

		private void Paging(int page)
		{
			grdPhysicians.PageIndex = page;
			PopulateGridView();
		}

		protected void grdPhysicians_Sorting(object sender, GridViewSortEventArgs e)
		{
			string newSortExpr = e.SortExpression;
			string sortDir = (string)Session["srchPhysSortDir"];
			string oldSortExpr = (string)Session["srchPhysSortExpr"];
			DataView physicianData = (DataView)Cache["srchPhysGridDataView"];

			var sortedPhysicianData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, physicianData);
			grdPhysicians.DataSource = sortedPhysicianData.Item1;
			Session["srchPhysSortDir"] = sortedPhysicianData.Item2;
			Session["srchPhysSortExpr"] = newSortExpr;
			grdPhysicians.DataBind();
		}



		protected void btnDeleteChecked_Click(object sender, EventArgs e)
		{
			// Loads delete confirmation control
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx"); // Load delete confirmation control
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "multi";                                              // Set deletion type to multiple
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();                // Clear content of delete confirmation update panel, if any
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);      // Add delete confirmation control to update panel

			// Assign event handlers
			ucDelConfirm.confirmed += new CommandEventHandler(DeleteConfirmed);
			ucDelConfirm.cancelled += new Action(RemoveDeleteConfirmation);
		}

		public void DeleteConfirmed(object sender, CommandEventArgs e)
		{
			// Initiate data tier and variables
			LouisDataTier dataTier = new LouisDataTier();
			bool deleteSuccess = false;
			string plural = "";

			// Check delete type
			switch (e.CommandArgument.ToString())
			{
				case "single": // Deleting a single record
					string singlePhysicianID = cipher.Decrypt(Convert.ToString(Session["delPhysicianID"]));
					deleteSuccess = dataTier.DeletePhysician(singlePhysicianID);
					break;

				case "multi": // Deleting all checked records
					plural = "s";
					if (grdPhysicians.Rows.Count > 0)
					{
						// Loop through rows
						foreach (GridViewRow row in grdPhysicians.Rows)
						{
							// Find checkbox of row
							CheckBox chkSelectPhysID = new CheckBox();
							chkSelectPhysID = (CheckBox)row.FindControl("chkPhysicianID");

							// Delete record if checkbox checked
							if (chkSelectPhysID.Checked)
							{
								string multiPhysicianID = row.Cells[1].Text;
								deleteSuccess = dataTier.DeletePatient(multiPhysicianID);
							}
						}
					}
					break;
			}

			if (deleteSuccess == true)
			{
				// Display success message
				RegisterAlertScript(new CommandEventArgs("script", "Patient record" + plural + " deleted successfully"));
			}
			else
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to delete patient record" + plural));
			}

			// Remove delete confirmation control and update GridView
			RemoveDeleteConfirmation();
			PopulateGridView();
		}

		public void RemoveDeleteConfirmation()
		{
			// Remove delete confirmation control
			pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();
			Session["delConfirmActive"] = false;
		}


		protected void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				// Save values in text boxes to session
				//SrchPatSaveSession();									 (currently not working)

				// Populate GridView
				PopulateGridView();
			}
			catch
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to load patient data"));
			}
		}

		protected void PopulateGridView()
		{
			// Get text box session values
			string physicianID = Convert.ToString(Session["srchPhysID"]);
			string lastName = Convert.ToString(Session["srchLName"]);
			string firstName = Convert.ToString(Session["srchFName"]);
			string employer = Convert.ToString(Session["srchEmployer"]);

			// Initiate data tier
			LouisDataTier aPhysician = new LouisDataTier();
			DataSet physicianData = new DataSet();
			physicianData = aPhysician.SearchPhysicians(physicianID, lastName, firstName, employer);

			// Populate datagrid with dataset
			grdPhysicians.DataSource = physicianData.Tables[0];
			grdPhysicians.DataBind();

			// Show/Enable Delete Selected button if table has rows
			if (grdPhysicians.Rows.Count > 0)
			{
				btnDeleteChecked.Enabled = true;
				btnDeleteChecked.Visible = true;
			}
			else // Table empty, hide Delete Selected button
			{
				btnDeleteChecked.Enabled = false;
				btnDeleteChecked.Visible = false;
			}

			// Set value that patient search has been performed
			Session["srchPhysHasSearched"] = "true";

			// Delete any cached GridView data
			Cache.Remove("srchPhysGridDataView");
			// Add new data to cache
			Cache.Add("srchPhysGridDataView", new DataView(physicianData.Tables[0]),
				null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes(10),
				System.Web.Caching.CacheItemPriority.Default, null);
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
			// Loads delete confirmation control
			Session["delPhysicianID"] = e.CommandArgument.ToString();                         // Store patient ID of record to be deleted
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx"); // Load delete confirmation control
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "single";                                             // Set deletion type to single
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();                // Clear content of delete confirmation update panel, if any
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);      // Add delete confirmation control to update panel

			// Assign event handlers
			ucDelConfirm.confirmed += new CommandEventHandler(DeleteConfirmed);
			ucDelConfirm.cancelled += new Action(RemoveDeleteConfirmation);
		}


	}
}