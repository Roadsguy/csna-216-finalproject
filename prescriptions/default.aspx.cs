using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

namespace FinalProject.prescriptions
{
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navPrescriptions") as HyperLink;
			navCurrent.Enabled = false;
			navCurrent.CssClass = "nav-full nav-current";

			UCPageType ucCurrent = (UCPageType)LoadControl("prescriptions_vieweditadd.ascx");
			ucCurrent.ID = "ucContent";
			this.pnlContent.ContentTemplateContainer.Controls.Clear();
			this.pnlContent.ContentTemplateContainer.Controls.Add(ucCurrent);
		}

        public partial class _default : System.Web.UI.Page
        {
            // Create instance of encryption class
            public encryption cipher = new encryption();

            // Create instance of user control class
            public UCPageType ucCurrent;

            protected void Page_Load(object sender, EventArgs e)
            {
                // Highlight/disable current page in navigation
                HyperLink navCurrent = Master.FindControl("navPrescriptions") as HyperLink;
                navCurrent.Enabled = false;
                navCurrent.CssClass = "nav-full nav-current";

                // Keep currently loaded user control loaded
                LoadUserControl((string)ViewState["pageType"], "");
            }

            public void LoadUserControl(string pageType, string cipherPrescriptionID)
            {
                string controlURL = "";
                switch (pageType) // Check which page type is being loaded
                {
                    case "view":
                    case "edit":
                    case "add":
                        // Set vieweditadd user control to be loaded
                        controlURL = "/prescriptions/prescriptionss_vieweditadd.ascx";
                        ViewState["pageType"] = pageType;
                        // Stop delete confirmation from reappearing on Go Back if already active
                        Session["delConfirmActive"] = "false";
                        break;
                    case "search":
                    default:
                        // Set search user control to be loaded
                        controlURL = "/prescriptions/prescriptions_search.ascx";
                        pageType = "search";
                        ViewState["pageType"] = pageType;
                        break;
                }

                // Load user control into update panel
                ucCurrent = (UCPageType)LoadControl(controlURL);
                ucCurrent.ID = "ucContent";
                ucCurrent.PageType = pageType;                                      // Set page type
                ucCurrent.PrimaryKey = cipherPrescriptionID;                             // Pass patient ID
                this.pnlContent.ContentTemplateContainer.Controls.Clear();          // Clear update panel
                this.pnlContent.ContentTemplateContainer.Controls.Add(ucCurrent);   // Add user control to update panel

                switch (pageType) // Check page type to determine event handlers to create
                {
                    case "view":
                    case "edit":
                    case "add":
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
                // Get current values of text boxes in search control
                TextBox txtSrchRxNo = (TextBox)ucCurrent.FindControl("txtSrchRxNo");
                TextBox txtSrchPatientID = (TextBox)ucCurrent.FindControl("txtSrchPatientID");
                TextBox txtSrchDrugID = (TextBox)ucCurrent.FindControl("txtSrchDrugID");
                TextBox txtSrchPhysicianID = (TextBox)ucCurrent.FindControl("txtSrchPhysicianID");
                // Save typed values in search control to session (to be retrieved when gone back)
                Session["srchRxNo"] = txtSrchRxNo.Text;
                Session["srchPatientID"] = txtSrchPatientID.Text;
                Session["srchDrugID"] = txtSrchDrugID.Text;
                Session["srchPhysicianID"] = txtSrchPhysicianID.Text;
                // Load vieweditadd control with PageType add
                LoadUserControl("add", "");
            }
            protected void Load_View(object sender, CommandEventArgs e)
            {
                // Load vieweditadd control with PageType view and prescription ID from CommandEventArgs
                LoadUserControl("view", e.CommandArgument.ToString());
            }

            protected void Load_Edit(object sender, CommandEventArgs e)
            {
                // Load vieweditadd control with PageType edit and prescription ID from CommandEventArgs
                LoadUserControl("edit", e.CommandArgument.ToString());
            }

            protected void GoBack(object sender, EventArgs e)
            {
                // Return to search page control
                LoadUserControl("search", "");
            }

            protected void btnAdd_Click(object sender, EventArgs e)
		{

		}
	}
}