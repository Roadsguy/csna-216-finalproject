using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FinalProject
{
	public partial class test : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			// Check for invalid null values and show error message
			string emptyMessage = "The following fields cannot be empty:" + Environment.NewLine;
			bool emptyFields = false;

			if (txtDrugID.Text.Trim() == string.Empty)
			{
				emptyMessage += "Drug ID" + Environment.NewLine;
				emptyFields = true;
			}
			if (txtDrugName.Text.Trim() == string.Empty)
			{
				emptyMessage += "Drug Name" + Environment.NewLine;
				emptyFields = true;
			}
			if (emptyFields == true)
			{
				// MessageBox.Show(emptyMessage, "New Drug", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Execute stored procedure (returns true if successful)
			LouisDataTier aDrug = new LouisDataTier();
			bool addSuccess = aDrug.AddDrug(
				txtDrugID.Text.Trim(),
				txtDrugName.Text.Trim(),
				txtDrugDesc.Text.Trim(),
				txtMethodOfAdmin.Text.Trim()
				);

			if (addSuccess == true)
			{
				// Display success message
				// MessageBox.Show("Drug added successfully", "New Drug", MessageBoxButtons.OK, MessageBoxIcon.Information);
				// ClearFields();
			}
			else
			{
				// Display failure message
				// MessageBox.Show("Failed to add drug", "New Drug", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			// Declare variables
			string strDrugID = "", strDrugName = "", strDrugDesc = "";

			try
			{
				// Get values
				strDrugID = txtDrugIDSearch.Text.Trim();
				// strDrugName = txtDrugName.Text.Trim();
				// strDrugDesc = txtDrugDesc.Text.Trim();

				// Initiate datatier
				LouisDataTier aDrug = new LouisDataTier();
				DataSet ds = new DataSet();
				ds = aDrug.GetDrugs(strDrugID, strDrugName, strDrugDesc);

				// Populate datagrid with dataset
				GridView1.DataSource = ds.Tables[0];
				GridView1.DataBind();
			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load drug data')", true);
			}
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Selection changed!')", true);
		}
	}
}