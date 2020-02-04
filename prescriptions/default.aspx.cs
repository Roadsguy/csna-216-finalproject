using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinalProject.prescriptions
{
	public partial class _default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			HyperLink navCurrent = Master.FindControl("navPrescriptions") as HyperLink;
			navCurrent.Enabled = false;
			navCurrent.CssClass = "nav-full nav-current";

			UCPageType ucCurrent = (UCPageType)LoadControl("prescriptions_search.ascx");
			ucCurrent.ID = "ucContent";
			this.pnlContent.ContentTemplateContainer.Controls.Clear();
			this.pnlContent.ContentTemplateContainer.Controls.Add(ucCurrent);
		}

		protected void btnAdd_Click(object sender, EventArgs e)
		{

		}
	}
}