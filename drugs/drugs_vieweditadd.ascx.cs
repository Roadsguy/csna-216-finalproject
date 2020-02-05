using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.drugs
{
	public partial class drugs_vieweditadd : UCPageType
	{
		// Create instance of encryption class
		public encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{

			// Decrypt and store patient ID if included
			string DrugID = "";
			if (!string.IsNullOrEmpty(PrimaryKey))
			{
				DrugID = cipher.Decrypt(PrimaryKey);
			}

			// Do different things for each page type
			switch (PageType)
			{
				case "view":
					// Populate controls
					PopulateForms(DrugID);
					// Disable all controls
					DisableAllControls();
					// Set header text
					lblPageHeader.Text = "View Drug Record";
					break;

				case "edit":
					// Populate controls
					PopulateForms(DrugID);
					// Enable patient ID text box
					txtDrugID.Enabled = false;
					// Set header text
					lblPageHeader.Text = "Edit Drug Record";
					break;

				case "add":
					// Set header text
					lblPageHeader.Text = "Add Drug Record";
					break;

				default: // Invalid PageType
					Response.Redirect("/drugs");
					break;
			}
		}

		protected void PopulateForms(string DrugID)
		{
			try
			{
				// Populate Patient ID textbox with input value
				txtDrugID.Text = DrugID;

				// Execute stored procedure
				LouisDataTier dataTier = new LouisDataTier();
				//DataSet ds = new DataSet();
				DataSet DrugData = dataTier.GetPatient(DrugID);

				if (DrugData.Tables[0].Rows.Count > 0) // If anything is returned
				{
					// Populate text/combo boxes with values
					txtDrugID.Text = DrugData.Tables[0].Rows[0]["DrugID"].ToString();
					txtDrugName.Text = DrugData.Tables[0].Rows[0]["DrugName"].ToString();
					txtDrugDesc.Text = DrugData.Tables[0].Rows[0]["DrugDesc"].ToString();
					txtMethodOfAdmin.Text = DrugData.Tables[0].Rows[0]["methodOfAdmin"].ToString();
				}
			}
			catch
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to populate controls"));
			}
		}

		protected void DisableAllControls()
		{
			// Disable text boxes and hide buttons
			txtDrugID.Enabled = false;
			txtDrugName.Enabled = false;
			txtDrugDesc.Enabled = false;
			txtMethodOfAdmin.Enabled = false;
			btnSubmit.Enabled = false;
			btnSubmit.Visible = false;
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			// Retrieve page type
			string pageType = PageType;

			// Check for invalid null values and show error message
			StringBuilder emptyMessage = new StringBuilder();
			emptyMessage.Append("The following fields cannot be empty:" + "\\n"); // Double backslashes are required for registering JS to work
			bool emptyFields = false;

			if (txtDrugID.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Drug ID" + "\\n");
				emptyFields = true;
			}
			if (txtDrugName.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Drug Name" + "\\n");
				emptyFields = true;
			}
			if (txtDrugDesc.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Drug Description" + "\\n");
				emptyFields = true;
			}
			if (txtMethodOfAdmin.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Method Of Administration" + "\\n");
				emptyFields = true;
			}
			if (emptyFields == true)
			{
				RegisterAlertScript(new CommandEventArgs("script", emptyMessage.ToString()));
				return;
			}

			// Initiate data tier
			LouisDataTier dataTier = new LouisDataTier();

			// Determine editing or adding record
			switch (pageType)
			{
				case "edit":
					// Execute stored procedure (returns true if successful)
					bool updateSuccess = dataTier.UpdateDrug(
						txtDrugID.Text.Trim(),
						txtDrugName.Text.Trim(),
						txtDrugDesc.Text.Trim(),
						txtMethodOfAdmin.Text.Trim()
						);

					if (updateSuccess == true)
					{
						// Display success message
						RegisterAlertScript(new CommandEventArgs("script", "Drug record updated successfully"));
					}
					else
					{
						// Display failure message
						RegisterAlertScript(new CommandEventArgs("script", "Failed to update drug record"));
					}
					break;

				case "add":
					// Execute stored procedure (returns true if successful)
					bool addSuccess = dataTier.AddDrug(
						txtDrugID.Text.Trim(),
						txtDrugName.Text.Trim(),
						txtDrugDesc.Text.Trim(),
						txtMethodOfAdmin.Text.Trim()
						);

					if (addSuccess == true)
					{
						// Display success message
						RegisterAlertScript(new CommandEventArgs("script", "Drug record added successfully"));
					}
					else
					{
						// Display failure message
						RegisterAlertScript(new CommandEventArgs("script", "Failed to add Drug record"));
					}
					break;
				default:
					// Display error message
					RegisterAlertScript(new CommandEventArgs("script", "Invalid pageType"));
					break;
			}
		}

		protected void btnGoBack_Click(object sender, EventArgs e)
		{
			// Trigger event to load search page
			GoBackButtonClicked(e);
		}
	}
}