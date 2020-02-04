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
		gridviewcode grd = new gridviewcode();
		public encryption cipher = new encryption();

		public UCPageType ucCurrent;

		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navPatients") as HyperLink;
			navCurrent.Enabled = false;
			navCurrent.CssClass = "nav-full nav-current";

			LoadUserControl((string)ViewState["pageType"], "");
		}

		public void LoadUserControl(string pageType, string cipherPatientID)
		{
			string controlURL = "";
			switch (pageType)
			{
				case "view":
				case "edit":
				case "add":
					controlURL = "/patients/patients_vieweditadd.ascx";
					ViewState["pageType"] = pageType;
					break;
				case "search":
				default:
					controlURL = "/patients/patients_search.ascx";
					pageType = "search";
					ViewState["pageType"] = pageType;
					break;
			}

			ucCurrent = (UCPageType)LoadControl(controlURL);
			ucCurrent.ID = "ucContent";
			ucCurrent.PageType = pageType;
			ucCurrent.PrimaryKey = cipherPatientID;
			this.pnlContent.ContentTemplateContainer.Controls.Clear();
			this.pnlContent.ContentTemplateContainer.Controls.Add(ucCurrent);

			switch (pageType)
			{
				case "view":
				case "edit":
				case "add":
					ucCurrent.GoBackClicked += new EventHandler(GoBack);
					break;
				case "search":
				default:
					Button btnAdd = (Button)ucCurrent.FindControl("btnAdd");
					btnAdd.Click += new EventHandler(btnAdd_Click);
					ucCurrent.ViewClicked += new CommandEventHandler(Load_View);
					ucCurrent.EditClicked += new CommandEventHandler(Load_Edit);
					break;
			}

			// Add script trigger event handler
			ucCurrent.AlertScriptTrigger += new CommandEventHandler(RegisterAlertScript);
		}

		protected void RegisterAlertScript(object sender, CommandEventArgs e)
		{
			ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('" + e.CommandArgument.ToString() + "')", true);
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{
			TextBox txtSrchPatientID = (TextBox)ucCurrent.FindControl("txtSrchPatientID");
			TextBox txtSrchLastName = (TextBox)ucCurrent.FindControl("txtSrchLastName");
			TextBox txtSrchFirstName = (TextBox)ucCurrent.FindControl("txtSrchFirstName");

			Session["srchPatID"] = txtSrchPatientID.Text;
			Session["srchLName"] = txtSrchLastName.Text;
			Session["srchFName"] = txtSrchFirstName.Text;

			LoadUserControl("add", "");
		}

		protected void Load_View(object sender, CommandEventArgs e)
		{
			LoadUserControl("view", e.CommandArgument.ToString());
		}

		protected void Load_Edit(object sender, CommandEventArgs e)
		{
			LoadUserControl("edit", e.CommandArgument.ToString());
		}

		protected void GoBack(object sender, EventArgs e)
		{
			LoadUserControl("search", "");
		}

		protected void grdPatients_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		protected void grdPatients_Sorting(object sender, GridViewSortEventArgs e)
		{
			/*
			string newSortExpr = e.SortExpression;
			string sortDir = (string)ViewState["srchPatSortDir"];
			string oldSortExpr = (string)ViewState["srchPatSortExpr"];
			DataView patientData = (DataView)Cache["srchPatGridDataView"];

			var sortedPatientData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, patientData);
			grdPatients.DataSource = sortedPatientData.Item1;
			ViewState["srchPatSortDir"] = sortedPatientData.Item2;
			ViewState["srchPatSortExpr"] = newSortExpr;
			grdPatients.DataBind();
			*/
		}

		public void SrchPatSaveSession()
		{
			//Session["srchPatID"] = txtSrchPatientID;
			//Session["srchLName"] = txtSrchLastName;
			//Session["srchFName"] = txtSrchFirstName;
		}
	}
}