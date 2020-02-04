using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
	public class UCDelConfirm : UserControl
	{
		// Delete confirmation event
		public event Action confirmed;
		public void DeleteConfirmed()
		{
			confirmed();
		}

		// JavaScript trigger event
		public event CommandEventHandler AlertScriptTrigger;
		public void RegisterAlertScript(CommandEventArgs e)
		{
			AlertScriptTrigger(this, e);
		}

		public string DeleteType
		{
			get { return Session["delConfirmType"] as string; }
			set { Session["delConfirmType"] = value; }
		}

		public string PrimaryKey
		{
			get { return Session["delPrimaryKey"] as string; }
			set { Session["delPrimaryKey"] = value; }
		}
	}
}