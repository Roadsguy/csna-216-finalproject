using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
	public class UCPageType : UserControl
	{
		// View button click event
		public event CommandEventHandler ViewClicked;
		public void ViewButtonClicked(CommandEventArgs e)
		{
			ViewClicked(this, e);
		}

		// Edit button click event
		public event CommandEventHandler EditClicked;
		public void EditButtonClicked(CommandEventArgs e)
		{
			EditClicked(this, e);
		}

		// Delete button click event
		public event CommandEventHandler DeleteClicked;
		public void DeleteButtonClicked(CommandEventArgs e)
		{
			DeleteClicked(this, e);
		}

		// Go Back button click event
		public event EventHandler GoBackClicked;
		public void GoBackButtonClicked(EventArgs e)
		{
			GoBackClicked(this, e);
		}

		// JavaScript trigger event
		public event CommandEventHandler AlertScriptTrigger;
		public void RegisterAlertScript(CommandEventArgs e)
		{
			AlertScriptTrigger(this, e);
		}

		// PageType property stored in ViewState
		public string PageType
		{
			get { return ViewState["controlPageType"] as string; }
			set { ViewState["controlPageType"] = value; }
		}
		
		// PrimaryKey property stored in ViewState
		public string PrimaryKey
		{
			get { return ViewState["ptPrimaryKey"] as string; }
			set { ViewState["ptPrimaryKey"] = value; }
		}
	}
}