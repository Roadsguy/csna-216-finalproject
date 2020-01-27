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
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navPatients") as HyperLink;
			navCurrent.Enabled = false;
			navCurrent.CssClass = "nav-full nav-current";

			if (Request.Form["refresh"] == "true")
			{
				txtPatientID.Text = Session["srchPatID"].ToString();
				btnSearch_Click(sender, e);
			}
		}

		public void Delete_Click(object sender, CommandEventArgs e)
		{

		}

		public void Edit_Click(object sender, CommandEventArgs e)
		{

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			Session["srchPatID"] = txtPatientID.Text.Trim();

			try
			{
				// Initiate data tier
				LouisDataTier aPatient = new LouisDataTier();
				DataSet ds = new DataSet();
				ds = aPatient.SearchPatients(txtPatientID.Text.Trim(), txtLastName.Text.Trim(), txtFirstName.Text.Trim());

				// Populate datagrid with dataset
				grdPatients.DataSource = ds.Tables[0];
				grdPatients.DataBind();
			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load patient data')", true);
			}
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{

		}

		[WebMethod]
		public static string GetData()
		{
			return "Test string from code behind";


		}
	}
}