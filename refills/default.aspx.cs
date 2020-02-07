using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.refills
{
	public partial class _default : BasePage
	{
		// Create instance of encryption class
		public encryption cipher = new encryption();

		// Create instance of user control class
		public UCPageType ucCurrent;

		protected void Page_Load(object sender, EventArgs e)
		{
			// Highlight/disable current page in navigation
			HyperLink navCurrent = Master.FindControl("navRefills") as HyperLink;
			navCurrent.Enabled = false;
			navCurrent.CssClass = "nav-full nav-current";

			// Keep currently loaded user control loaded
			LoadUserControl((string)ViewState["pageType"], "", "");
		}

		public void LoadUserControl(string pageType, string cipherRxNo, string cipherRefillNo)
		{
			string controlURL = "";
			switch (pageType) // Check which page type is being loaded
			{
				case "view":
				case "edit":
				case "add-auto":
				case "add-manual":
					// Set vieweditadd user control to be loaded
					controlURL = "/refills/refills_vieweditadd.ascx";
					ViewState["pageType"] = pageType;
					// Stop delete confirmation from reappearing on Go Back if already active
					Session["delConfirmActive"] = "false";
					break;
				case "search":
				default:
					// Set search user control to be loaded
					controlURL = "/refills/refills_search.ascx";
					pageType = "search";
					ViewState["pageType"] = pageType;
					break;
			}

			// Load user control into update panel
			ucCurrent = (UCPageType)LoadControl(controlURL);
			ucCurrent.ID = "ucContent";
			ucCurrent.PageType = pageType;										// Set page type
			ucCurrent.PrimaryKey = cipherRxNo + "," + cipherRefillNo;			// Pass Rx No. and Refill No.
			this.pnlContent.ContentTemplateContainer.Controls.Clear();			// Clear update panel
			this.pnlContent.ContentTemplateContainer.Controls.Add(ucCurrent);	// Add user control to update panel

			switch (pageType) // Check page type to determine event handlers to create
			{
				case "view":
				case "edit":
				case "add-auto":
				case "add-manual":
					// Event handler for GoBack button
					ucCurrent.GoBackClicked += new EventHandler(GoBack);
					break;
				case "search":
				default:
					// Create instance of btnAdd and add event handler
					LinkButton btnAdd = (LinkButton)ucCurrent.FindControl("btnAdd");
					btnAdd.Click += new EventHandler(btnAdd_Click);
					// Create event handlers for View and Edit buttons to load respective pages
					ucCurrent.ViewClicked += new CommandEventHandler(Load_View);
					ucCurrent.EditClicked += new CommandEventHandler(Load_Edit);
					break;
			}

			// Add script trigger event handler
			ucCurrent.AlertScriptTrigger += new CommandEventHandler(RegisterAlertScript);
		}

		protected void RegisterAlertScript(object sender, CommandEventArgs e)
		{
			// Registers alert script with specified message
			ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('" + e.CommandArgument.ToString() + "')", true);
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			// Save search values from fields
			ucCurrent.SaveSearchValues();

			// Load vieweditadd control with PageType add-manual
			LoadUserControl("add-manual", "", "");
		}

		protected void Load_View(object sender, CommandEventArgs e)
		{
			// Split compound primary key
			string[] viewArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

			// Load vieweditadd control with PageType view and compound primary key from CommandEventArgs
			LoadUserControl("view", viewArgs[0], viewArgs[1]);
		}

		protected void Load_Edit(object sender, CommandEventArgs e)
		{
			// Split compound primary key
			string[] editArgs = e.CommandArgument.ToString().Split(new char[] { ',' });

			// Load vieweditadd control with PageType edit and compound primary key from CommandEventArgs
			LoadUserControl("edit", editArgs[0], editArgs[1]);
		}

		protected void GoBack(object sender, EventArgs e)
		{
			// Return to search page control
			LoadUserControl("search", "", "");
		}
	}
}