﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.physicians
{
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navPhysicians") as HyperLink;
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
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load physician data')", true);
			}
		}
	}
}