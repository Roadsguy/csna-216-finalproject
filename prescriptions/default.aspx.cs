using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinalProject.prescriptions
{
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navPrescriptions") as HyperLink;
			navCurrent.Enabled = false;
			navCurrent.CssClass = "nav-full nav-current";
		}

		public void Delete_Click(object sender, CommandEventArgs e)
		{

		}

		public void Edit_Click(object sender, CommandEventArgs e)
		{

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			Session["srchPrescID"] = txtRxNo.Text.Trim();

			try
			{
				// Initiate data tier
				LouisDataTier dataTier = new LouisDataTier();
				DataSet prescData = new DataSet();
				prescData = dataTier.SearchPrescriptions(txtRxNo.Text.Trim(), txtPatientID.Text.Trim(), txtDrugID.Text.Trim(), txtPhysicianID.Text.Trim());

				// Populate datagrid with dataset
				grdPrescriptions.DataSource = prescData.Tables[0];
				grdPrescriptions.DataBind();
			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load prescription data')", true);
			}
		}

		protected void btnAddPresc_Click(object sender, EventArgs e)
		{

		}
	}
}