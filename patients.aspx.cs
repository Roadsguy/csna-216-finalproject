using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinalProject
{
	public partial class patients : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navPatients") as HyperLink;
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
			try
			{
				// Initiate data tier
				LouisDataTier aPatient = new LouisDataTier();
				DataSet ds = new DataSet();
				ds = aPatient.GetPatients(txtStudentID.Text.Trim(), txtFirstName.Text.Trim(), txtLastName.Text.Trim());

				// Populate datagrid with dataset
				grdPatients.DataSource = ds.Tables[0];
				grdPatients.DataBind();
			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load patient data')", true);
			}
		}
	}
}