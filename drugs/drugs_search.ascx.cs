using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

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
			txtSrchDrugID.Text = Convert.ToString(Session["srchDrugID"]);	//CHANGED
			txtSrchDrugName.Text = Convert.ToString(Session["srchDrugName"]);	//CHANGED
			txtSrchDrugDesc.Text = Convert.ToString(Session["srchDrugDesc"]);	//CHANGED

			// Establish GridView event handlers
			grdDrugs.PageIndexChanging += new GridViewPageEventHandler(grdDrugs_PageIndexChanging); //CHANGED
			grdDrugs.Sorting += new GridViewSortEventHandler(grdDrugs_Sorting); //CHANGED

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
				SrchDrugSaveSession();	//CHANGED

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
			string DrugID = Convert.ToString(Session["srchDrugID"]);    //CHANGED
			string DrugName = Convert.ToString(Session["srchDrugName"]);    //CHANGED
			string DrugDesc = Convert.ToString(Session["srchDrugDesc"]);    //CHANGED

			// Initiate data tier
			LouisDataTier aDrug = new LouisDataTier();  //CHANGED
			DataSet DrugData = new DataSet();   //CHANGED
			DrugData = aDrug.SearchDrugs(DrugID, DrugName, DrugDesc);   //CHANGED

			// Populate datagrid with dataset
			grdDrugs.DataSource = DrugData.Tables[0];   //CHANGED
			grdDrugs.DataBind();    //CHANGED

			// Show/Enable Delete Selected button if table has rows
			if (grdDrugs.Rows.Count > 0)    //CHANGED
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
			Session["srchDrugHasSearched"] = "true";    //CHANGED

			// Delete any cached GridView data
			Cache.Remove("srchDrugGridDataView");	//CHANGED
			// Add new data to cache
			Cache.Add("srchDrugGridDataView", new DataView(DrugData.Tables[0]), //CHANGED
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
			Session["delDrugID"] = e.CommandArgument.ToString();    //CHANGED                        // Store patient ID of record to be deleted
			deleteconfirm ucDelConfirm = (deleteconfirm)LoadControl("/deleteconfirm.ascx"); // Load delete confirmation control
			ucDelConfirm.ID = "ucDelConfirm";
			ucDelConfirm.DeleteType = "single";                                             // Set deletion type to single
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Clear();                // Clear content of delete confirmation update panel, if any
			this.pnlDeleteConfirm.ContentTemplateContainer.Controls.Add(ucDelConfirm);      // Add delete confirmation control to update panel

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
					string singleDrugID = cipher.Decrypt(Convert.ToString(Session["delDrugID"]));   //CHANGED 
					deleteSuccess = dataTier.DeletePatient(singleDrugID);   //CHANGED 
					break;

				case "multi": // Deleting all checked records
					plural = "s";
					if (grdDrugs.Rows.Count > 0)
					{
						// Loop through rows
						foreach (GridViewRow row in grdDrugs.Rows)
						{
							// Find checkbox of row
							CheckBox chkSelectDrugID = new CheckBox();  //CHANGED 
							chkSelectDrugID = (CheckBox)row.FindControl("chkDrugID");   //CHANGED 

							// Delete record if checkbox checked
							if (chkSelectDrugID.Checked)    //CHANGED 
							{
								string multiDrugID = row.Cells[1].Text; //CHANGED 
								deleteSuccess = dataTier.DeletePatient(multiDrugID);    //CHANGED 
							}
						}
					}
					break;
			}

			if (deleteSuccess == true)
			{
				// Display success message
				RegisterAlertScript(new CommandEventArgs("script", "Drug record" + plural + " deleted successfully"));  //CHANGED 
			}
			else
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to delete drug record" + plural));   //CHANGED 
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

		protected void grdDrugs_PageIndexChanging(object sender, GridViewPageEventArgs e)   //CHANGED 
		{
			int pageNum = e.NewPageIndex;
			Paging(pageNum);
		}

		private void Paging(int page)
		{
			grdDrugs.PageIndex = page;  //CHANGED 
			PopulateGridView();
		}

		protected void grdDrugs_Sorting(object sender, GridViewSortEventArgs e)
		{
			string newSortExpr = e.SortExpression;
			string sortDir = (string)Session["srchDrugSortDir"];    //CHANGED 
			string oldSortExpr = (string)Session["srchDrugSortExpr"];   //CHANGED 
			DataView DrugData = (DataView)Cache["srchDrugGridDataView"];    //CHANGED 

			var sortedDrugData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, DrugData);
			grdDrugs.DataSource = sortedDrugData.Item1;
			Session["srchDrugSortDir"] = sortedDrugData.Item2;
			Session["srchDrugSortExpr"] = newSortExpr;
			grdDrugs.DataBind();
		}

		public void SrchDrugSaveSession()
		{
			// Save currently entered text box values to session
			Session["srchDrugID"] = txtSrchDrugID.Text;
			Session["srchDrugName"] = txtSrchDrugName.Text;
			Session["srchDrugDesc"] = txtSrchDrugDesc.Text;
		}

	}
}