using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

namespace FinalProject.physicians
{
	public partial class physician_view : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			try
			{
				// Initiate data tier
				LouisDataTier aPhysician = new LouisDataTier();
				DataSet ds = new DataSet();
				ds = aPhysician.GetPhysicians(txtPhysicianID.Text.Trim(), txtFirstName.Text.Trim(), txtLastName.Text.Trim(), txtEmployer.Text.Trim());

				// Populate datagrid with dataset
				grdPhysicians.DataSource = ds.Tables[0];
				grdPhysicians.DataBind();
			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load physician data')", true);
			}
		}

		protected void grdPhysician_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		public void Delete_Click(object sender, CommandEventArgs e)
		{

		}

		public void Edit_Click(object sender, CommandEventArgs e)
		{

		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{

		}
	}
}