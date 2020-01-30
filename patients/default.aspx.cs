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

		protected void Page_Load(object sender, EventArgs e)
		{
			// Establish GridView event handlers
			grdPatients.PageIndexChanging += new GridViewPageEventHandler(grdPatients_PageIndexChanging);
			// grdPatients.Sorting += new GridViewSortEventHandler(grdPatients_Sorting);

			HyperLink navCurrent = Master.FindControl("navPatients") as HyperLink;
			navCurrent.Enabled = false;
			navCurrent.CssClass = "nav-full nav-current";

			if (Request.Form["refresh"] == "true")
			{
				txtSrchPatientID.Text = Convert.ToString(Session["srchPatID"]);
				txtSrchFirstName.Text = Convert.ToString(Session["srchFName"]);
				txtSrchLastName.Text = Convert.ToString(Session["srchLName"]);

				if (Convert.ToString(Session["srchPatHasSearched"]) == "true")
				{
					grdPatients.DataSource = (DataView)Cache["srchPatGridDataView"];
					grdPatients.DataBind();
					//btnSearch_Click(sender, e);
				}
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
			Session["srchPatID"] = txtSrchPatientID.Text.Trim();
			Session["srchFName"] = txtSrchFirstName.Text.Trim();
			Session["srchLName"] = txtSrchLastName.Text.Trim();

			try
			{
				Cache.Remove("srchPatGridDataView");

				// Initiate data tier
				LouisDataTier aPatient = new LouisDataTier();
				DataSet patientData = new DataSet();
				patientData = aPatient.SearchPatients(txtSrchPatientID.Text.Trim(), txtSrchLastName.Text.Trim(), txtSrchFirstName.Text.Trim());

				// Populate datagrid with dataset
				grdPatients.DataSource = patientData.Tables[0];
				grdPatients.DataBind();

				Session["srchPatGridData"] = patientData;
				Session["srchPatHasSearched"] = "true";

				if (Cache["srchPatGridDataView"] == null)
				{
					Cache.Add("srchPatGridDataView", new DataView(patientData.Tables[0]),
						null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes(10),
						System.Web.Caching.CacheItemPriority.Default, null);
				}
			}
			catch
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load patient data')", true);
			}
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{

		}

		protected void grdPatients_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{

		}

		protected void grdPatients_Sorting(object sender, GridViewSortEventArgs e)
		{
			string newSortExpr = e.SortExpression;
			string sortDir = (string)ViewState["srchPatSortDir"];
			string oldSortExpr = (string)ViewState["srchPatSortExpr"];
			DataView patientData = (DataView)Cache["srchPatGridDataView"];

			var sortedPatientData = grd.SortRecords(oldSortExpr, newSortExpr, sortDir, patientData);
			grdPatients.DataSource = sortedPatientData.Item1;
			ViewState["srchPatSortDir"] = sortedPatientData.Item2;
			ViewState["srchPatSortExpr"] = newSortExpr;
			grdPatients.DataBind();
		}

		[WebMethod(enableSession: true)]
		public static void SrchPatSaveSession(string patientID, string lastName, string firstName)
		{
			HttpContext.Current.Session["srchPatID"] = patientID;
			HttpContext.Current.Session["srchLName"] = lastName;
			HttpContext.Current.Session["srchFName"] = firstName;
		}

		protected void btnSubmit_Click(object sender, EventArgs e)
		{
			ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('TEST')", true);
		}
	}
}