using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
	public partial class prescriptions : System.Web.UI.Page
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
			try
			{

			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load prescription data')", true);
			}
		}
	}
}