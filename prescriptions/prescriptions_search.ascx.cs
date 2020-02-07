using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;

namespace FinalProject.prescriptions
{
	public partial class prescriptions_search : UCPageType
	{
		// Create instance of gridview code and encryption class
		gridviewcode grd = new gridviewcode();
		public encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{
			// Populate DropDownLists
			PopulateDropDownLists();

			// Put previously typed/selected values back into controls, if any (used on return from vieweditadd page)
			if (((BasePage)Page).SearchFields.Length > 0)    //Check if any exist
			{
				txtSrchRxNo.Text = ((BasePage)Page).SearchFields[0];
				txtSrchPatientID.Text = ((BasePage)Page).SearchFields[1];
				ddlSrchPatientName.SelectedValue = ((BasePage)Page).SearchFields[1];
				txtSrchDrugID.Text = ((BasePage)Page).SearchFields[2];
				ddlSrchDrugName.SelectedValue = ((BasePage)Page).SearchFields[2];
				txtSrchPhysicianID.Text = ((BasePage)Page).SearchFields[3];
				ddlSrchPhysicianName.SelectedValue = ((BasePage)Page).SearchFields[3];
			}

			// Set input type
			switch (((BasePage)Page).InputType)
			{
				case "byID":
					rdoInputByID.Checked = true;
					break;
				case "byName":
				default:
					rdoInputByName.Checked = true;
					break;
			}
			SwitchInputType(sender, e);

			if (((BasePage)Page).HasSearched) // Check if search already performed
			{
				// Repopulate GridView
				PopulateGridView();
			}

			// Establish GridView event handlers
			grdPrescriptions.PageIndexChanging += new GridViewPageEventHandler(grdPrescriptions_PageIndexChanging);
			grdPrescriptions.Sorting += new GridViewSortEventHandler(grdPrescriptions_Sorting);

			// Establish event handler to save search values
			SavedSearchValues += new Action(SrchPrescKeepValues);

			// Keep delete confirmation loaded on partial postback, if already loaded
			if (Convert.ToString(Session["delConfirmActive"]) == "true")
            {
                switch (Convert.ToString(Session["delConfirmType"])) // Check whether single or multi delete
                {
                    case "single":
                        // Load single delete confirmation
                        Delete_Click(sender, new CommandEventArgs("delete", Convert.ToString(Session["delRxNo"])));
                        break;
                    case "multi":
                        // Load multi delete confirmation
                        btnDeleteChecked_Click(sender, e);
                        break;
                }
            }
        }

		protected void PopulateDropDownLists()
		{
			try
			{
				// Initiate data tier
				LouisDataTier dataTier = new LouisDataTier();
				
				// Populate Patient Name search field
				if (Cache["patientNames"] == null) // Check if cache empty
				{
					DataSet patientNames = dataTier.PopulateDDL("patient");
					ddlSrchPatientName.DataSource = patientNames;
					Cache.Add("patientNames", new DataView(patientNames.Tables[0]), null, Cache.NoAbsoluteExpiration,
						System.TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);
				}
				else // Cached data exists
				{
					ddlSrchPatientName.DataSource = Cache["patientNames"];
				}
				ddlSrchPatientName.DataTextField = "patient";
				ddlSrchPatientName.DataValueField = "patientID";
				ddlSrchPatientName.Items.Insert(0, new ListItem(string.Empty, string.Empty));	// Add leading blank row
				ddlSrchPatientName.DataBind();

				// Populate Drug Name search field
				if (Cache["drugNames"] == null) // Check if cache empty
				{
					DataSet drugNames = dataTier.PopulateDDL("drug");
					ddlSrchDrugName.DataSource = drugNames;
					Cache.Add("drugNames", new DataView(drugNames.Tables[0]), null, Cache.NoAbsoluteExpiration,
						System.TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);
				}
				else // Cached data exists
				{
					ddlSrchDrugName.DataSource = Cache["drugNames"];
				}
				ddlSrchDrugName.DataTextField = "drug";
				ddlSrchDrugName.DataValueField = "drugID";
				ddlSrchDrugName.Items.Insert(0, new ListItem(string.Empty, string.Empty));		// Add leading blank row
				ddlSrchDrugName.DataBind();

				// Populate Physician Name search field
				if (Cache["physicianNames"] == null) // Check if cache empty
				{
					DataSet physicianNames = dataTier.PopulateDDL("physician");
					ddlSrchPhysicianName.DataSource = physicianNames;
					Cache.Add("physicianNames", new DataView(physicianNames.Tables[0]), null, Cache.NoAbsoluteExpiration,
						System.TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);
				}
				else // Cached data exists
				{
					ddlSrchPhysicianName.DataSource = Cache["physicianNames"];
				}
				ddlSrchPhysicianName.DataTextField = "physician";
				ddlSrchPhysicianName.DataValueField = "physicianID";
				ddlSrchPhysicianName.Items.Insert(0, new ListItem(string.Empty, string.Empty));	// Add leading blank row
				ddlSrchPhysicianName.DataBind();
			}
			catch //(Exception ex)
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to populate DropDownLists"));
				//RegisterAlertScript(new CommandEventArgs("script", ex.Message));
			}
		}

		protected void SwitchInputType(object sender, EventArgs e)
		{
			if (rdoInputByID.Checked == true)
			{
				// Set search type
				((BasePage)Page).InputType = "byID";
				// Transfer values
				txtSrchPatientID.Text = ddlSrchPatientName.SelectedValue;
				txtSrchDrugID.Text = ddlSrchDrugName.SelectedValue;
				txtSrchPhysicianID.Text = ddlSrchPhysicianName.SelectedValue;
				// Disable Search By Name
				divInputByName.Visible = false;
				ddlSrchPatientName.Visible = false;
				ddlSrchPatientName.Enabled = false;
				ddlSrchDrugName.Visible = false;
				ddlSrchDrugName.Enabled = false;
				ddlSrchPhysicianName.Visible = false;
				ddlSrchPhysicianName.Enabled = false;
				// Enable Search By ID
				divInputByID.Visible = true;
				txtSrchPatientID.Visible = true;
				txtSrchPatientID.Enabled = true;
				txtSrchDrugID.Visible = true;
				txtSrchDrugID.Enabled = true;
				txtSrchPhysicianID.Visible = true;
				txtSrchPhysicianID.Enabled = true;
			}
			else // rdoInputByName.Checked == true
			{
				// Set search type
				((BasePage)Page).InputType = "byName";
				// Transfer values
				ddlSrchPatientName.SelectedValue = txtSrchPatientID.Text;		// No Trim() on purpose.
				ddlSrchDrugName.SelectedValue = txtSrchDrugID.Text;				// Trim clips leading zeroes, 
				ddlSrchPhysicianName.SelectedValue = txtSrchPhysicianID.Text;	// which breaks SelectedValue conversion.
				// Disable Search By ID
				divInputByID.Visible = false;
				txtSrchPatientID.Visible = false;
				txtSrchPatientID.Enabled = false;
				txtSrchDrugID.Visible = false;
				txtSrchDrugID.Enabled = false;
				txtSrchPhysicianID.Visible = false;
				txtSrchPhysicianID.Enabled = false;
				// Enable Search By Name
				divInputByName.Visible = true;
				ddlSrchPatientName.Visible = true;
				ddlSrchPatientName.Enabled = true;
				ddlSrchDrugName.Visible = true;
				ddlSrchDrugName.Enabled = true;
				ddlSrchPhysicianName.Visible = true;
				ddlSrchPhysicianName.Enabled = true;
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				// Save values in text boxes to session
				SrchPrescKeepValues();

				// Clear saved search data
				((BasePage)Page).SearchData = null;

				// Populate GridView
				PopulateGridView();
			}
			catch
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to load prescription data"));
			}
		}

		public void SrchPrescKeepValues()
		{
			switch (((BasePage)Page).InputType)
			{
				case "byName":
					((BasePage)Page).SearchFields = new string[] {
						txtSrchRxNo.Text.Trim(),
						ddlSrchPatientName.SelectedValue,
						ddlSrchDrugName.SelectedValue,
						ddlSrchPhysicianName.SelectedValue
					};
					break;

				case "byID":
					// Save currently entered control values
					((BasePage)Page).SearchFields = new string[] {
						txtSrchRxNo.Text.Trim(),
						txtSrchPatientID.Text.Trim(),
						txtSrchDrugID.Text.Trim(),
						txtSrchPhysicianID.Text.Trim()
					};
					break;
			}
		}

		protected void PopulateGridView()
        {
			if (((BasePage)Page).SearchData == null) // Check for any saved data
			{
				// Declare variables
				string rxNo = "";
				string patientID = "";
				string drugID = "";
				string physicianID = "";

				// Get text box values
				if (((BasePage)Page).SearchFields != null)                      // Check if SearchFields array exists
					if (((string[])((BasePage)Page).SearchFields).Length > 0)   // Check if array has values
					{
						rxNo = ((BasePage)Page).SearchFields[0];
						patientID = ((BasePage)Page).SearchFields[1];
						drugID = ((BasePage)Page).SearchFields[2];
						physicianID = ((BasePage)Page).SearchFields[3];
					}

				// Initiate data tier and fill data set
				LouisDataTier dataTier = new LouisDataTier();
				DataSet prescriptionData = dataTier.SearchPrescriptions(rxNo, patientID, drugID, physicianID);

				// Populate GridView with dataset
				grdPrescriptions.DataSource = prescriptionData.Tables[0];

				// Save data to ViewState
				((BasePage)Page).SearchData = prescriptionData;
			}
			else // Saved data exists
			{
				// Populate GridView from saved data
				grdPrescriptions.DataSource = ((BasePage)Page).SearchData.Tables[0];
			}

			// Bind data to GridView
			grdPrescriptions.DataBind();

			// Show/Enable Delete Selected button if table has rows
			if (grdPrescriptions.Rows.Count > 0)
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

		public void Refill_Click(object sender, CommandEventArgs e)
		{
			// Trigger refill button click event for default page
			RefillButtonClicked(e);
		}

		public void Delete_Click(object sender, CommandEventArgs e)
		{
			// Loads delete confirmation control
			Session["delRxNo"] = e.CommandArgument.ToString();								// Store Rx No. of record to be deleted
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx");	// Load delete confirmation control
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "single";												// Set deletion type to single
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();				// Clear content of delete confirmation update panel, if any
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);		// Add delete confirmation control to update panel

			// Assign event handlers
			ucDelConfirm.confirmed += new CommandEventHandler(DeleteConfirmed);
			ucDelConfirm.cancelled += new Action(RemoveDeleteConfirmation);
		}

		protected void btnDeleteChecked_Click(object sender, EventArgs e)
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
					string singleRxNo = cipher.Decrypt(Convert.ToString(Session["delRxNo"]));
					deleteSuccess = dataTier.DeletePrescription(singleRxNo);
					break;

				case "multi": // Deleting all checked records
					plural = "s";
					if (grdPrescriptions.Rows.Count > 0)
					{
						// Loop through rows
						foreach (GridViewRow row in grdPrescriptions.Rows)
						{
							// Find checkbox of row
							CheckBox chkSelectRxNo = new CheckBox();
							chkSelectRxNo = (CheckBox)row.FindControl("chkRxNo");

							// Delete record if checkbox checked
							if (chkSelectRxNo.Checked)
							{
								string multiRxNo = row.Cells[1].Text;
								deleteSuccess = dataTier.DeletePrescription(multiRxNo);
							}
						}
					}
					break;
			}

			if (deleteSuccess == true)
			{
				// Display success message
				RegisterAlertScript(new CommandEventArgs("script", "Prescription record" + plural + " deleted successfully"));

				// Clear saved data
				((BasePage)Page).SearchData = null;
			}
			else
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to delete prescription record" + plural));
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

		protected void grdPrescriptions_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			int pageNum = e.NewPageIndex;
			Paging(pageNum);
		}

		private void Paging(int page)
		{
			grdPrescriptions.PageIndex = page;
			PopulateGridView();
		}

		protected void grdPrescriptions_Sorting(object sender, GridViewSortEventArgs e)
		{
			string newSortExpr = e.SortExpression;
			string sortDir = (string)Session["srchPrescSortDir"];
			string oldSortExpr = (string)Session["srchPrescSortExpr"];
			DataView prescriptionData = new DataView(((BasePage)Page).SearchData.Tables[0]);

			var sortedPrescriptionData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, prescriptionData);
			grdPrescriptions.DataSource = sortedPrescriptionData.Item1;
			Session["srchPrescSortDir"] = sortedPrescriptionData.Item2;
			Session["srchPrescSortExpr"] = newSortExpr;
			grdPrescriptions.DataBind();
		}
	}
}