using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.patients
{
	public partial class ajaxtest : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			//btnLoad_Click(sender, e);
		}

		protected void Page_PreInit(object sender, EventArgs e)
		{
			if ((string)ViewState["testExists"] == "true")
			{
				//Load the control   
				Control test1Control = LoadControl("UCtest1.ascx");
				test1Control.ID = "test1";

				// Add the control to the panel  
				updTest.ContentTemplateContainer.Controls.Add(test1Control);
			}
		}

		protected void btnPostBack_Click(object sender, EventArgs e)
		{
			//btnPostBack.Text = "UpdatePanel PostBack Happened";
		}

		protected void btnClear_Click(object sender, EventArgs e)
		{
			//test1.ClearTest1();
			//test2.ClearTest2();

			updTest.ContentTemplateContainer.Controls.Clear();
		}

		protected void btnLoad_Click(object sender, EventArgs e)
		{
			//Load the control   
			Control test1Control = LoadControl("UCtest1.ascx");
			test1Control.ID = "test1";

			// Add the control to the panel  
			updTest.ContentTemplateContainer.Controls.Add(test1Control);
			ViewState["testExists"] = "true";
		}
	}
}