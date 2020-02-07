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
			if (((BasePage)Page).SearchFields.Length > 0)    //Check if any exist
			{
				txtSrchPhysicianID.Text = ((BasePage)Page).SearchFields[0];
				txtSrchLastName.Text = ((BasePage)Page).SearchFields[1];
				txtSrchFirstName.Text = ((BasePage)Page).SearchFields[2];
				txtSrchEmployer.Text = ((BasePage)Page).SearchFields[3];
			}

			// Establish GridView event handlers
			grdPhysicians.PageIndexChanging += new GridViewPageEventHandler(grdPhysicians_PageIndexChanging);
			grdPhysicians.Sorting += new GridViewSortEventHandler(grdPhysicians_Sorting);

			// Establish event handler to save search values
			SavedSearchValues += new Action(SrchPhysKeepValues);

			if (((BasePage)Page).HasSearched) // Check if search already performed
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
			DataView physicianData = new DataView(((BasePage)Page).SearchData.Tables[0]);

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
								deleteSuccess = dataTier.DeletePhysician(multiPhysicianID);
							}
						}
					}
					break;
			}

			if (deleteSuccess == true)
			{
				// Display success message
				RegisterAlertScript(new CommandEventArgs("script", "Physician record" + plural + " deleted successfully"));
			}
			else
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to delete physician record" + plural));
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
				SrchPhysKeepValues();

				// Clear saved search data
				((BasePage)Page).SearchData = null;

				// Populate GridView
				PopulateGridView();
			}
			catch
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to load physician data"));
			}
		}

		public void SrchPhysKeepValues()
		{
			// Save currently entered TextBox values
			((BasePage)Page).SearchFields = new string[] {
				txtSrchPhysicianID.Text.Trim(),
				txtSrchLastName.Text.Trim(),
				txtSrchFirstName.Text.Trim(),
				txtSrchEmployer.Text.Trim()
			};
		}

		protected void PopulateGridView()
		{
			if (((BasePage)Page).SearchData == null) // Cache is empty{
			{
				// Declare variables
				string physicianID = "";
				string lastName = "";
				string firstName = "";
				string employer = "";

				// Get text box values
				if (((BasePage)Page).SearchFields != null)                      // Check if SearchFields array exists
					if (((string[])((BasePage)Page).SearchFields).Length > 0)   // Check if array has values
					{
						physicianID = ((BasePage)Page).SearchFields[0];
						lastName = ((BasePage)Page).SearchFields[1];
						firstName = ((BasePage)Page).SearchFields[2];
						employer = ((BasePage)Page).SearchFields[3];
					}

				// Initiate data tier and fill data set
				LouisDataTier dataTier = new LouisDataTier();
				DataSet physicianData = dataTier.SearchPhysicians(physicianID, lastName, firstName, employer);

				// Populate GridView with dataset
				grdPhysicians.DataSource = physicianData.Tables[0];

				// Save data to ViewState
				((BasePage)Page).SearchData = physicianData;
			}
			else // Saved data exists
			{
				// Populate GridView from saved data
				grdPhysicians.DataSource = ((BasePage)Page).SearchData.Tables[0];
			}

			// Bind data to GridView
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

			// Set value that prescription search has been performed
			((BasePage)Page).HasSearched = true;
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
			Session["delPhysicianID"] = e.CommandArgument.ToString();						// Store physician ID of record to be deleted
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx");	// Load delete confirmation control
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