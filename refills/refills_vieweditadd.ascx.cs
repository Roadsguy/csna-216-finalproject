using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace FinalProject.refills
{
	public partial class refills_vieweditadd : UCPageType
	{
		// Create instance of encryption class
		encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{
			// Decrypt and store patient ID if included
			string rxNo = "";
			string refillNo = "";
			if (!string.IsNullOrEmpty(PrimaryKey))
			{
				// Split compound primary key
				string[] cipherPK = PrimaryKey.Split(new char[] { ',' });

				rxNo = cipher.Decrypt(cipherPK[0]);

				// Check if array has second value
				if (cipherPK.Length > 1)
				{
					refillNo = cipher.Decrypt(cipherPK[1]);
				}
			}

			// Do different things for each page type
			switch (PageType)
			{
				case "view":
					// Populate controls
					PopulateForms(rxNo, refillNo);
					// Disable all controls
					DisableAllControls();
					// Set header text
					lblPageHeader.Text = "View Refill Record";
					break;

				case "edit":
					// Populate controls
					PopulateForms(rxNo, refillNo);
					// Disable Rx No. text box
					txtRxNo.Enabled = false;
					// Hide refill number
					divRefillNo.Visible = false;
					txtRefillNo.Enabled = false;
					// Autofill refill date
					txtRefillDateTime.Text = DateTime.Now.ToString();
					// Set header text
					lblPageHeader.Text = "Edit Refill Record";
					break;

				case "refill-auto":
					// Hide refill number
					divRefillNo.Visible = false;
					txtRefillNo.Enabled = false;
					// Disable and populate Rx No. text box
					txtRxNo.Enabled = false;
					txtRxNo.Text = rxNo;
					// Autofill refill date
					txtRefillDateTime.Text = DateTime.Now.ToString();
					// Set header text
					lblPageHeader.Text = "Refill Prescription";
					break;

				case "add-manual":
					// Hide refill number
					divRefillNo.Visible = false;
					txtRefillNo.Enabled = false;
					// Autofill refill date
					txtRefillDateTime.Text = DateTime.Now.ToString();
					// Set header text
					lblPageHeader.Text = "Add Refill Record";
					break;

				default: // Invalid PageType
					Response.Redirect("/refills");
					break;
			}
		}

		protected void PopulateForms(string rxNo, string refillNo)
		{
			try
			{
				// Populate Rx No. and Refill No text boxes
				txtRxNo.Text = rxNo;
				txtRefillNo.Text = refillNo;

				// Execute stored procedure
				LouisDataTier dataTier = new LouisDataTier();
				DataSet refillData = dataTier.GetPatient(rxNo);

				if (refillData.Tables[0].Rows.Count > 0) // If anything is returned
				{
					// Populate text/combo boxes with values
					txtRefillDateTime.Text = refillData.Tables[0].Rows[0]["refillDateTime"].ToString();
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
			txtRxNo.Enabled = false;
			txtRefillNo.Enabled = false;
			txtRefillDateTime.Enabled = false;
			btnSubmit.Enabled = false;
			btnSubmit.Visible = false;
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			// Check for invalid null values and show error message
			StringBuilder emptyMessage = new StringBuilder();
			emptyMessage.Append("The following fields cannot be empty:" + "\\n"); // Double backslashes are required for registering JS to work
			bool emptyFields = false;

			if (txtRxNo.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Rx No." + "\\n");
				emptyFields = true;
			}
			if (txtRefillDateTime.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Time of Refill");
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
			switch (PageType)
			{
				case "edit":
					// Execute stored procedure (returns true if successful)
					bool updateSuccess = dataTier.UpdateRefill(
						txtRxNo.Text.Trim(),
						txtRefillNo.Text.Trim(),
						txtRefillDateTime.Text.Trim()
						);

					if (updateSuccess == true)
					{
						// Display success message
						RegisterAlertScript(new CommandEventArgs("script", "Refill record updated successfully"));

						// Clear saved data
						ClearSavedData();
					}
					else
					{
						// Display failure message
						RegisterAlertScript(new CommandEventArgs("script", "Failed to update refill record"));
					}
					break;

				case "refill-auto":
				case "add-manual":
					// Execute stored procedure (returns true if successful)
					bool addSuccess = dataTier.AddRefill(
						txtRxNo.Text.Trim(),
						txtRefillDateTime.Text.Trim()
						);

					if (addSuccess == true)
					{
						// Display success message
						RegisterAlertScript(new CommandEventArgs("script", "Prescription refilled successfully"));

						// Clear saved data
						ClearSavedData();
					}
					else
					{
						// Display failure message
						RegisterAlertScript(new CommandEventArgs("script", "Failed to refill prescriptions"));
					}
					break;
				default:
					// Display error message
					RegisterAlertScript(new CommandEventArgs("script", "Invalid pageType"));
					break;
			}

			if (PageType == "refill-auto")
			{
				// Automatically return to Prescriptions search page
				GoBackButtonClicked(e);
			}
		}

		protected void ClearSavedData()
		{
			// Clear saved data
			((BasePage)Page).SearchData = null;
		}

		protected void btnGoBack_Click(object sender, EventArgs e)
		{
			// Trigger event to load search page
			GoBackButtonClicked(e);
		}
	}
}