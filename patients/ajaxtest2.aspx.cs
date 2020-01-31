using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.patients
{
	public partial class ajaxtest2 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void btnPostBack_Click(object sender, EventArgs e)
		{
			btnPostBack.Text = "UpdatePanel PostBack Happened";
		}
	}
}