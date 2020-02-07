using System;
using System.Data;
using System.Web.Caching;
using System.Web.UI.WebControls;

namespace FinalProject.drugs
{
	public partial class drugs_search : UCPageType
	{
		// Create instance of gridview code and encryption class
		gridviewcode grd = new gridviewcode();
		public encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{
			// Put previously typed values back into text boxes, if any (used on return from vieweditadd page)
			if (((BasePage)Page).SearchFields.Length > 0)    //Check if any exist
			{
				txtSrchDrugID.Text = ((BasePage)Page).SearchFields[0];
				txtSrchDrugName.Text = ((BasePage)Page).SearchFields[1];
				txtSrchDrugDesc.Text = ((BasePage)Page).SearchFields[2];
			}

			// Establish GridView event handlers
			grdDrugs.PageIndexChanging += new GridViewPageEventHandler(grdDrugs_PageIndexChanging);
			grdDrugs.Sorting += new GridViewSortEventHandler(grdDrugs_Sorting);

			// Establish event handler to save search values
			SavedSearchValues += new Action(SrchDrugKeepValues);

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
						Delete_Click(sender, new CommandEventArgs("delete", Convert.ToString(Session["delDrugID"])));
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
				SrchDrugKeepValues();

				// Clear saved search data
				((BasePage)Page).SearchData = null;

				// Populate GridView
				PopulateGridView();
			}
			catch
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to load drug data"));
			}
		}

		public void SrchDrugKeepValues()
		{
			// Save currently entered TextBox values
			((BasePage)Page).SearchFields = new string[] {
				txtSrchDrugID.Text.Trim(),
				txtSrchDrugName.Text.Trim(),
				txtSrchDrugDesc.Text.Trim()
			};
		}

		protected void PopulateGridView()
		{
			if (((BasePage)Page).SearchData == null) // No saved data
			{
				// Declare variables
				string drugID = "";
				string drugName = "";
				string drugDesc = "";

				// Get text box values
				if (((BasePage)Page).SearchFields != null)                      // Check if SearchFields array exists
					if (((BasePage)Page).SearchFields.Length > 0)   // Check if array has values
					{
						drugID = ((BasePage)Page).SearchFields[0];
						drugName = ((BasePage)Page).SearchFields[1];
						drugDesc = ((BasePage)Page).SearchFields[2];
					}

				// Initiate data tier and fill data set
				LouisDataTier dataTier = new LouisDataTier();
				DataSet drugData = dataTier.SearchDrugs(drugID, drugName, drugDesc);

				// Populate GridView with dataset
				grdDrugs.DataSource = drugData.Tables[0];

				// Save data to ViewState
				((BasePage)Page).SearchData = drugData;
			}
			else // Saved data exists
			{
				// Populate GridView from saved data
				grdDrugs.DataSource = ((BasePage)Page).SearchData.Tables[0];
			}

			// Bind data to GridView
			grdDrugs.DataBind();

			// Show/Enable Delete Selected button if table has rows
			if (grdDrugs.Rows.Count > 0)
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
			Session["delDrugID"] = e.CommandArgument.ToString();							// Store drug ID of record to be deleted
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
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx");	// Load delete confirmation control
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "multi";												// Set deletion type to multiple
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();				// Clear content of delete confirmation update panel, if any
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);		// Add delete confirmation control to update panel

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
					string singleDrugID = cipher.Decrypt(Convert.ToString(Session["delDrugID"]));
					deleteSuccess = dataTier.DeleteDrug(singleDrugID);
					break;

				case "multi": // Deleting all checked records
					plural = "s";
					if (grdDrugs.Rows.Count > 0)
					{
						// Loop through rows
						foreach (GridViewRow row in grdDrugs.Rows)
						{
							// Find checkbox of row
							CheckBox chkSelectDrugID = new CheckBox();
							chkSelectDrugID = (CheckBox)row.FindControl("chkDrugID");

							// Delete record if checkbox checked
							if (chkSelectDrugID.Checked)
							{
								string multiDrugID = row.Cells[1].Text;
								deleteSuccess = dataTier.DeleteDrug(multiDrugID);
							}
						}
					}
					break;
			}

			if (deleteSuccess == true)
			{
				// Clear saved data
				Cache.Remove("drugNames");
				((BasePage)Page).SearchData = null;


				// Display success message
				RegisterAlertScript(new CommandEventArgs("script", "Drug record" + plural + " deleted successfully"));
			}
			else
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to delete drug record" + plural));
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

		protected void grdDrugs_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			int pageNum = e.NewPageIndex;
			Paging(pageNum);
		}

		private void Paging(int page)
		{
			grdDrugs.PageIndex = page;
			PopulateGridView();
		}

		protected void grdDrugs_Sorting(object sender, GridViewSortEventArgs e)
		{
			string newSortExpr = e.SortExpression;
			string sortDir = (string)Session["srchDrugSortDir"];
			string oldSortExpr = (string)Session["srchDrugSortExpr"];
			DataView drugData = new DataView(((BasePage)Page).SearchData.Tables[0]);

			var sortedDrugData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, drugData);
			grdDrugs.DataSource = sortedDrugData.Item1;
			Session["srchDrugSortDir"] = sortedDrugData.Item2;
			Session["srchDrugSortExpr"] = newSortExpr;
			grdDrugs.DataBind();
		}
	}
}