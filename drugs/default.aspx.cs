using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinalProject.drugs
{
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navDrugs") as HyperLink;
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
			//Session["srchDrugID"] = txtDrugID.Text.Trim();

			//try
			//{
			//	// Initiate data tier
			//	LouisDataTier aDrugSearch = new LouisDataTier();
			//	DataSet DrugData = new DataSet();
			//	DrugData = aDrugSearch.SearchDrugs(txtDrugID.Text.Trim(), txtDrugName.Text.Trim(), txtDrugDesc.Text.Trim());

			//	// Populate datagrid with dataset
			//	grdDrugs.DataSource = DrugData.Tables[0];
			//	grdDrugs.DataBind();
			//}
			//catch
			//{
			//	ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Failed to load drug data')", true);
			//}
		}

		protected void btnAddDrug_Click(object sender, EventArgs e)
		{

		}
	}
}