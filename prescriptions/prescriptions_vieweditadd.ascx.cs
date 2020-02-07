using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;
using System.Text;

namespace FinalProject.prescriptions
{
	public partial class prescriptions_vieweditadd : UCPageType
	{
		// Create instance of encryption class
		encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{
			// Decrypt and store Rx No. if included
			string rxNo = "";
			if (!string.IsNullOrEmpty(PrimaryKey))
			{
				rxNo = cipher.Decrypt(PrimaryKey);
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

			// Populate DropDownLists
			PopulateDropDownLists();

			// Do different things for each page type
			switch (PageType)
			{
				case "view":
					// Populate controls
					PopulateForms(rxNo);
					// Disable all controls
					DisableAllControls(sender, e);
					// Set header text
					lblPageHeader.Text = "View Prescription Record";
					break;

				case "edit":
					// Populate controls
					PopulateForms(rxNo);
					// Disable Rx No. text box
					txtRxNo.Enabled = false;
					// Disable/hide Refill button
					btnRefill.Enabled = false;
					btnRefill.Visible = false;
					// Set header text
					lblPageHeader.Text = "Edit Prescription Record";
					break;

				case "add":
					// Hide Refills Left field
					txtRefillsLeft.Enabled = false;
					divRefillsLeft.Visible = false;
					// Disable/hide Refill button
					btnRefill.Enabled = false;
					btnRefill.Visible = false;
					// Set header text
					lblPageHeader.Text = "Add Prescription Record";
					break;

				default: // Invalid PageType
					Response.Redirect("/prescriptions");
					break;
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
					ddlPatientName.DataSource = patientNames;
					Cache.Add("patientNames", new DataView(patientNames.Tables[0]), null, Cache.NoAbsoluteExpiration,
						System.TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);
				}
				else // Cached data exists
				{
					ddlPatientName.DataSource = Cache["patientNames"];
				}
				ddlPatientName.DataTextField = "patient";
				ddlPatientName.DataValueField = "patientID";
				ddlPatientName.Items.Insert(0, new ListItem(string.Empty, string.Empty));   // Add leading blank row
				ddlPatientName.DataBind();

				// Populate Drug Name search field
				if (Cache["drugNames"] == null) // Check if cache empty
				{
					DataSet drugNames = dataTier.PopulateDDL("drug");
					ddlDrugName.DataSource = drugNames;
					Cache.Add("drugNames", new DataView(drugNames.Tables[0]), null, Cache.NoAbsoluteExpiration,
						System.TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);
				}
				else // Cached data exists
				{
					ddlDrugName.DataSource = Cache["drugNames"];
				}
				ddlDrugName.DataTextField = "drug";
				ddlDrugName.DataValueField = "drugID";
				ddlDrugName.Items.Insert(0, new ListItem(string.Empty, string.Empty));      // Add leading blank row
				ddlDrugName.DataBind();

				// Populate Physician Name search field
				if (Cache["physicianNames"] == null) // Check if cache empty
				{
					DataSet physicianNames = dataTier.PopulateDDL("physician");
					ddlPhysicianName.DataSource = physicianNames;
					Cache.Add("physicianNames", new DataView(physicianNames.Tables[0]), null, Cache.NoAbsoluteExpiration,
						System.TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);
				}
				else // Cached data exists
				{
					ddlPhysicianName.DataSource = Cache["physicianNames"];
				}
				ddlPhysicianName.DataTextField = "physician";
				ddlPhysicianName.DataValueField = "physicianID";
				ddlPhysicianName.Items.Insert(0, new ListItem(string.Empty, string.Empty)); // Add leading blank row
				ddlPhysicianName.DataBind();
			}
			catch //(Exception ex)
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to populate DropDownLists"));
				//RegisterAlertScript(new CommandEventArgs("script", ex.Message));
			}
		}

		protected void PopulateForms(string rxNo)
		{
			try
			{
				// Populate Rx No. textbox with input value
				txtRxNo.Text = rxNo;

				// Execute stored procedure
				LouisDataTier dataTier = new LouisDataTier();
				DataSet prescriptionData = dataTier.GetPrescription(rxNo);

				if (prescriptionData.Tables[0].Rows.Count > 0) // If anything is returned
				{
					// Populate text/combo boxes with values
					txtRxNo.Text = prescriptionData.Tables[0].Rows[0]["rxNo"].ToString();
					txtPatientID.Text = prescriptionData.Tables[0].Rows[0]["patientID"].ToString();
					ddlPatientName.SelectedValue = prescriptionData.Tables[0].Rows[0]["patientID"].ToString();
					txtDrugID.Text = prescriptionData.Tables[0].Rows[0]["drugID"].ToString();
					ddlDrugName.SelectedValue = prescriptionData.Tables[0].Rows[0]["drugID"].ToString();
					txtPhysicianID.Text = prescriptionData.Tables[0].Rows[0]["physicianID"].ToString();
					ddlPhysicianName.SelectedValue = prescriptionData.Tables[0].Rows[0]["physicianID"].ToString();
					txtDosage.Text = prescriptionData.Tables[0].Rows[0]["dosage"].ToString();
					txtFrequency.Text = prescriptionData.Tables[0].Rows[0]["frequency"].ToString();
					txtRefillsGiven.Text = prescriptionData.Tables[0].Rows[0]["refillsGiven"].ToString();
					txtRefillsLeft.Text = prescriptionData.Tables[0].Rows[0]["refillsLeft"].ToString();
					txtCost.Text = prescriptionData.Tables[0].Rows[0]["cost"].ToString();
					txtStartDate.Text = prescriptionData.Tables[0].Rows[0]["startDate"].ToString().Trim().Substring(0, 10);
					txtFinishDate.Text = prescriptionData.Tables[0].Rows[0]["finishDate"].ToString().Trim().Substring(0, 10);
				}
			}
			catch
			{
				// Display failure message
				RegisterAlertScript(new CommandEventArgs("script", "Failed to populate controls"));
			}
		}

		protected void DisableAllControls(object sender, EventArgs e)
		{
			// Switch to view by ID and hide radio buttons
			rdoInputByID.Checked = true;
			rdoInputByID.Enabled = false;
			rdoInputByID.Visible = false;
			rdoInputByName.Enabled = false;
			rdoInputByName.Visible = false;

			// Disable text boxes and hide buttons
			txtRxNo.Enabled = false;
			txtPatientID.Enabled = false;
			ddlPatientName.Enabled = false;
			txtDrugID.Enabled = false;
			ddlDrugName.Enabled = false;
			txtPhysicianID.Enabled = false;
			ddlPhysicianName.Enabled = false;
			txtDosage.Enabled = false;
			txtFrequency.Enabled = false;
			txtRefillsGiven.Enabled = false;
			txtRefillsLeft.Enabled = false;
			txtCost.Enabled = false;
			txtStartDate.Enabled = false;
			txtFinishDate.Enabled = false;
			btnSubmit.Enabled = false;
			btnSubmit.Visible = false;
		}

		protected void SwitchInputType(object sender, EventArgs e)
		{
			if (rdoInputByID.Checked == true)
			{
				// Set search type
				((BasePage)Page).InputType = "byID";
				// Transfer values
				txtPatientID.Text = ddlPatientName.SelectedValue;
				txtDrugID.Text = ddlDrugName.SelectedValue;
				txtPhysicianID.Text = ddlPhysicianName.SelectedValue;
				// Disable Search By Name
				divInputByName.Visible = false;
				ddlPatientName.Visible = false;
				ddlPatientName.Enabled = false;
				ddlDrugName.Visible = false;
				ddlDrugName.Enabled = false;
				ddlPhysicianName.Visible = false;
				ddlPhysicianName.Enabled = false;
				// Enable Search By ID
				divInputByID.Visible = true;
				txtPatientID.Visible = true;
				txtPatientID.Enabled = true;
				txtDrugID.Visible = true;
				txtDrugID.Enabled = true;
				txtPhysicianID.Visible = true;
				txtPhysicianID.Enabled = true;
			}
			else // rdoInputByName.Checked == true
			{
				// Set search type
				((BasePage)Page).InputType = "byName";
				// Transfer values
				ddlPatientName.SelectedValue = txtPatientID.Text;       // No Trim() on purpose.
				ddlDrugName.SelectedValue = txtDrugID.Text;             // Trim clips leading zeroes, 
				ddlPhysicianName.SelectedValue = txtPhysicianID.Text;   // which breaks SelectedValue conversion.
				// Disable Search By ID
				divInputByID.Visible = false;
				txtPatientID.Visible = false;
				txtPatientID.Enabled = false;
				txtDrugID.Visible = false;
				txtDrugID.Enabled = false;
				txtPhysicianID.Visible = false;
				txtPhysicianID.Enabled = false;
				// Enable Search By Name
				divInputByName.Visible = true;
				ddlPatientName.Visible = true;
				ddlPatientName.Enabled = true;
				ddlDrugName.Visible = true;
				ddlDrugName.Enabled = true;
				ddlPhysicianName.Visible = true;
				ddlPhysicianName.Enabled = true;
			}
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			// Get Patient, Drug, and Physician based on input type
			string patientID = "", drugID = "", physicianID = "", msgID = "";
			switch (((BasePage)Page).InputType)
			{
				case "byName":
					patientID = txtPatientID.Text.Trim();
					drugID = txtDrugID.Text.Trim();
					physicianID = txtPhysicianID.Text.Trim();
					break;
				case "byID":
					patientID = ddlPatientName.SelectedValue;
					drugID = ddlDrugName.SelectedValue;
					physicianID = ddlPhysicianName.SelectedValue;
					msgID = " ID";
					break;
				default:
					RegisterAlertScript(new CommandEventArgs("script", "Invalid InputType"));
					return;
			}

			// Check for invalid null values and show error message
			StringBuilder emptyMessage = new StringBuilder();
			emptyMessage.Append("The following fields cannot be empty:" + "\\n"); // Double backslashes are required for registering JS to work
			bool emptyFields = false;

			if (txtRxNo.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Rx No." + "\\n");
				emptyFields = true;
			}
			if (patientID == string.Empty)
			{
				emptyMessage.Append("Patient" + msgID + "\\n");
				emptyFields = true;
			}
			if (drugID == string.Empty)
			{
				emptyMessage.Append("Drug" + msgID + "\\n");
				emptyFields = true;
			}
			if (physicianID == string.Empty)
			{
				emptyMessage.Append("Physician" + msgID + "\\n");
				emptyFields = true;
			}
			if (txtRefillsGiven.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Refills Given" + "\\n");
				emptyFields = true;
			}
			if (txtRefillsLeft.Text.Trim() == string.Empty)
			{
				emptyMessage.Append("Refills Left");
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
					bool updateSuccess = dataTier.UpdatePrescription(
						txtRxNo.Text.Trim(),
						drugID,
						patientID,
						physicianID,
						txtDosage.Text.Trim(),
						txtFrequency.Text.Trim(),
						txtStartDate.Text.Trim(),
						txtFinishDate.Text.Trim(),
						txtRefillsLeft.Text.Trim(),
						txtCost.Text.Trim(),
						txtRefillsGiven.Text.Trim()
						);

					if (updateSuccess == true)
					{
						// Display success message
						RegisterAlertScript(new CommandEventArgs("script", "Prescription record updated successfully"));
					}
					else
					{
						// Display failure message
						RegisterAlertScript(new CommandEventArgs("script", "Failed to update prescription record"));
					}
					break;

				case "add":
					// Execute stored procedure (returns true if successful)
					bool addSuccess = dataTier.AddPrescription(
						txtRxNo.Text.Trim(),
						drugID,
						patientID,
						physicianID,
						txtDosage.Text.Trim(),
						txtFrequency.Text.Trim(),
						txtStartDate.Text.Trim(),
						txtFinishDate.Text.Trim(),
						txtRefillsGiven.Text.Trim(),
						txtCost.Text.Trim()
						);

					if (addSuccess == true)
					{
						// Display success message
						RegisterAlertScript(new CommandEventArgs("script", "Prescription record added successfully"));
					}
					else
					{
						// Display failure message
						RegisterAlertScript(new CommandEventArgs("script", "Failed to add prescription record"));
					}
					break;
				default:
					// Display error message
					RegisterAlertScript(new CommandEventArgs("script", "Invalid pageType"));
					break;
			}
		}

		protected void btnRefill_Click(object sender, EventArgs e)
		{
			// Encrypt Rx No.
			string cipherRxNo = cipher.Encrypt(txtRxNo.Text.Trim());

			// Trigger event to load refill page
			RefillButtonClicked(new CommandEventArgs("rxNo", cipherRxNo));
		}

		protected void btnGoBack_Click(object sender, EventArgs e)
		{
			// Trigger event to load search page
			GoBackButtonClicked(e);
		}
	}
}