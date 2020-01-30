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
	public partial class patients_vieweditadd : System.Web.UI.Page
	{
		encryption cipher = new encryption();

		protected void Page_Load(object sender, EventArgs e)
		{
			string pageType = "";

			if (Request.Form["type"] != null)
			{
				pageType = cipher.Decrypt(Request.Form["type"]);
			}
			else
			{
				Response.Redirect("/patients");
			}

			string cipherpatientID = Request.Form["ID"];

			// Populate state combo box
			ddlAddrState.DataSource = StateManager.getStates();
			ddlAddrState.DataTextField = "abbreviation";
			ddlAddrState.DataValueField = "abbreviation";
			ddlAddrState.SelectedIndex = 0;
			ddlAddrState.DataBind();

			switch (pageType)
			{
				case "view":
					PopulateForms(cipherpatientID);
					DisableAllControls();
					lblPageHeader.Text = "View Patient Record";
					break;

				case "edit":
					PopulateForms(cipherpatientID);
					txtPatientID.Enabled = false;
					lblPageHeader.Text = "Edit Patient Record";
					break;

				case "add":
					lblPageHeader.Text = "Add Patient Record";
					break;

				default:
					Response.Redirect("/patients");
					break;
			}
		}

		protected void PopulateForms(string cipherPatientID)
		{
			try
			{
				string patientID = cipher.Decrypt(cipherPatientID);

				// Populate Patient ID textbox with input value
				txtPatientID.Text = patientID;

				// Execute stored procedure
				LouisDataTier aPatient = new LouisDataTier();
				//DataSet ds = new DataSet();
				DataSet ds = aPatient.GetPatient(patientID);

				if (ds.Tables[0].Rows.Count > 0) // If anything is returned
				{
					// Populate text/combo boxes with values
					txtLastName.Text = ds.Tables[0].Rows[0]["lName"].ToString();
					txtFirstName.Text = ds.Tables[0].Rows[0]["fName"].ToString();
					txtMidInit.Text = ds.Tables[0].Rows[0]["mInit"].ToString();
					txtDateOfBirth.Text = ds.Tables[0].Rows[0]["dateOfBirth"].ToString().Trim().Substring(0, 10);
					txtInsuranceCo.Text = ds.Tables[0].Rows[0]["insuranceCo"].ToString();
					txtAcctBalance.Text = ds.Tables[0].Rows[0]["acctBalance"].ToString();
					txtAddrLine1.Text = ds.Tables[0].Rows[0]["addrLine1"].ToString();
					txtAddrLine2.Text = ds.Tables[0].Rows[0]["addrLine2"].ToString();
					txtAddrCity.Text = ds.Tables[0].Rows[0]["addrCity"].ToString();
					ddlAddrState.SelectedValue = ds.Tables[0].Rows[0]["addrState"].ToString().Trim();
					txtAddrZip.Text = ds.Tables[0].Rows[0]["addrZip"].ToString();
					txtEmail1.Text = ds.Tables[0].Rows[0]["email1"].ToString();
					txtEmail2.Text = ds.Tables[0].Rows[0]["email2"].ToString();
					txtHomePhoneNo.Text = ds.Tables[0].Rows[0]["homePhoneNo"].ToString();
					txtCellPhoneNo.Text = ds.Tables[0].Rows[0]["cellPhoneNo"].ToString();

					// Select gender radio button
					char selectedGender;
					char.TryParse(ds.Tables[0].Rows[0]["gender"].ToString().Trim(), out selectedGender);

					if (selectedGender == 'M')
					{
						rdoGenderM.Checked = true;
						rdoGenderF.Checked = false;
					}
					else
					{
						rdoGenderF.Checked = true;
						rdoGenderM.Checked = false;
					}
				}
			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to populate controls')", true);
			}
		}

		protected void DisableAllControls()
		{
			txtPatientID.Enabled = false;
			txtLastName.Enabled = false;
			txtFirstName.Enabled = false;
			txtMidInit.Enabled = false;
			rdoGenderM.Enabled = false;
			rdoGenderF.Enabled = false;
			txtDateOfBirth.Enabled = false;
			txtAddrLine1.Enabled = false;
			txtAddrLine2.Enabled = false;
			txtAddrCity.Enabled = false;
			ddlAddrState.Enabled = false;
			txtAddrZip.Enabled = false;
			txtEmail1.Enabled = false;
			txtEmail2.Enabled = false;
			txtHomePhoneNo.Enabled = false;
			txtCellPhoneNo.Enabled = false;
			txtAcctBalance.Enabled = false;
			txtInsuranceCo.Enabled = false;
			btnSubmit.Enabled = false;
			btnSubmit.Visible = false;
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('TEST')", true);

			// Retrieve page type
			string pageType = cipher.Decrypt(Request.Form["type"]);

			// Interpret empty account balance as $0.00
			string strAcctBalance = txtAcctBalance.Text.Trim();

			if (strAcctBalance == string.Empty)
			{
				strAcctBalance = "0.00";
			}

			// Check for invalid null values and show error message
			string emptyMessage = "The following fields cannot be empty:" + Environment.NewLine;
			bool emptyFields = false;

			if (txtPatientID.Text.Trim() == string.Empty)
			{
				emptyMessage += "Patient ID" + Environment.NewLine;
				emptyFields = true;
			}
			if (txtLastName.Text.Trim() == string.Empty)
			{
				emptyMessage += "Last Name" + Environment.NewLine;
				emptyFields = true;
			}
			if (txtFirstName.Text.Trim() == string.Empty)
			{
				emptyMessage += "First Name" + Environment.NewLine;
				emptyFields = true;
			}
			if (rdoGenderM.Checked == false && rdoGenderF.Checked == false)
			{
				emptyMessage += "Gender";
				emptyFields = true;
			}
			if (emptyFields == true)
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + emptyMessage + "')", true);
				return;
			}

			// Determine selected gender
			char selectedGender = 'Z';
			if (rdoGenderM.Checked == true)
			{
				selectedGender = 'M';
			}
			else // rdoGenderF.Checked == true
			{
				selectedGender = 'F';
			}

			// Initiate data tier
			LouisDataTier dataTier = new LouisDataTier();

			switch (pageType)
			{
				case "edit":
					// Execute stored procedure (returns true if successful)
					bool updateSuccess = dataTier.UpdatePatient(
						txtPatientID.Text.Trim(),
						txtFirstName.Text.Trim(),
						txtLastName.Text.Trim(),
						txtMidInit.Text.Trim(),
						selectedGender,
						txtDateOfBirth.Text.Trim(),
						strAcctBalance,
						txtInsuranceCo.Text.Trim(),
						txtAddrLine1.Text.Trim(),
						txtAddrLine2.Text.Trim(),
						txtAddrCity.Text.Trim(),
						ddlAddrState.SelectedValue.ToString(),
						txtAddrZip.Text.Trim(),
						txtEmail1.Text.Trim(),
						txtEmail2.Text.Trim(),
						txtHomePhoneNo.Text.Trim(),
						txtCellPhoneNo.Text.Trim()
						);

					if (updateSuccess == true)
					{
						// Display success message
						ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Patient record updated successfully')", true);
					}
					else
					{
						// Display failure message
						ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to update patient record')", true);
					}
					break;
				case "add":
					// Execute stored procedure (returns true if successful)
					bool addSuccess = dataTier.AddPatient(
						txtPatientID.Text.Trim(),
						txtFirstName.Text.Trim(),
						txtLastName.Text.Trim(),
						txtMidInit.Text.Trim(),
						selectedGender,
						txtDateOfBirth.Text.Trim(),
						strAcctBalance,
						txtInsuranceCo.Text.Trim(),
						txtAddrLine1.Text.Trim(),
						txtAddrLine2.Text.Trim(),
						txtAddrCity.Text.Trim(),
						ddlAddrState.SelectedValue.ToString(),
						txtAddrZip.Text.Trim(),
						txtEmail1.Text.Trim(),
						txtEmail2.Text.Trim(),
						txtHomePhoneNo.Text.Trim(),
						txtCellPhoneNo.Text.Trim()
						);

					if (addSuccess == true)
					{
						// Display success message
						ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Patient record added successfully')", true);
					}
					else
					{
						// Display failure message
						ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to add patient record')", true);
					}
					break;
				default:
					// Display error message
					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid pageType')", true);
					break;
			}
		}
	}
}