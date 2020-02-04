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
		// Create instance of gridview code and encryption class
		gridviewcode grd = new gridviewcode();
		public encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{
			// Put previously typed values back into text boxes, if any (used on return from vieweditadd page)
			txtSrchPatientID.Text = Convert.ToString(Session["srchPatID"]);
			txtSrchLastName.Text = Convert.ToString(Session["srchLName"]);
			txtSrchFirstName.Text = Convert.ToString(Session["srchFName"]);

			// Establish GridView event handlers
			grdPatients.PageIndexChanging += new GridViewPageEventHandler(grdPatients_PageIndexChanging);
			grdPatients.Sorting += new GridViewSortEventHandler(grdPatients_Sorting);

			if (Convert.ToString(Session["srchPatHasSearched"]) == "true") // Check if search already performed
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
						Delete_Click(sender, new CommandEventArgs("delete", Convert.ToString(Session["delPatientID"])));
						break;
					case "multi":
						// Load multi delete confirmation
						btnDeleteChecked_Click(sender, e);
						break;
				}
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				// Save values in text boxes to session
				SrchPatSaveSession();

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

			// Show/Enable Delete Selected button if table has rows
			if (grdPatients.Rows.Count > 0)
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
			Session["srchPatHasSearched"] = "true";

			// Delete any cached GridView data
			Cache.Remove("srchPatGridDataView");
			// Add new data to cache
			Cache.Add("srchPatGridDataView", new DataView(patientData.Tables[0]),
				null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes(10),
				System.Web.Caching.CacheItemPriority.Default, null);
		}

		public void View_Click(object sender, CommandEventArgs e)
		{
			// Trigger view button click event for default page
			ViewButtonClicked(e);
		}

		public void Edit_Click(object sender, CommandEventArgs e)
		{
			// Trigger edit button click event for default page
			EditButtonClicked(e);
		}

		public void Delete_Click(object sender, CommandEventArgs e)
		{
			// Loads delete confirmation control
			Session["delPatientID"] = e.CommandArgument.ToString();							// Store patient ID of record to be deleted
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx");	// Load delete confirmation control
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "single";												// Set deletion type to single
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();				// Clear content of delete confirmation update panel, if any
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);		// Add delete confirmation control to update panel

			// Assign event handlers
			ucDelConfirm.confirmed += new CommandEventHandler(DeleteConfirmed);
			ucDelConfirm.cancelled += new Action(RemoveDeleteConfirmation);
		}

		public void btnDeleteChecked_Click(object sender, EventArgs e)
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

		public void DelRegisterAlertScript(object sender, CommandEventArgs e)
		{
			// Pass alert script event from delete confirmation control to main page
			RegisterAlertScript(e);
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
					string singlePatientID = cipher.Decrypt(Convert.ToString(Session["delPatientID"]));
					deleteSuccess = dataTier.DeletePatient(singlePatientID);
					break;

				case "multi": // Deleting all checked records
					plural = "s";
					if (grdPatients.Rows.Count > 0)
					{
						// Loop through rows
						foreach (GridViewRow row in grdPatients.Rows)
						{
							// Find checkbox of row
							CheckBox chkSelectPatID = new CheckBox();
							chkSelectPatID = (CheckBox)row.FindControl("chkPatientID");

							// Delete record if checkbox checked
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

		protected void grdPatients_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			int pageNum = e.NewPageIndex;
			Paging(pageNum);
		}

		private void Paging(int page)
		{
			grdPatients.PageIndex = page;
			PopulateGridView();
		}

		protected void grdPatients_Sorting(object sender, GridViewSortEventArgs e)
		{
			string newSortExpr = e.SortExpression;
			string sortDir = (string)Session["srchPatSortDir"];
			string oldSortExpr = (string)Session["srchPatSortExpr"];
			DataView patientData = (DataView)Cache["srchPatGridDataView"];

			var sortedPatientData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, patientData);
			grdPatients.DataSource = sortedPatientData.Item1;
			Session["srchPatSortDir"] = sortedPatientData.Item2;
			Session["srchPatSortExpr"] = newSortExpr;
			grdPatients.DataBind();
		}

		public void SrchPatSaveSession()
		{
			// Save currently entered text box values to session
			Session["srchPatID"] = txtSrchPatientID.Text;
			Session["srchLName"] = txtSrchLastName.Text;
			Session["srchFName"] = txtSrchFirstName.Text;
		}
	}
}