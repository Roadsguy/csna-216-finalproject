using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
	public partial class login : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Set Log In button as default button
			Page.Form.DefaultButton = btnLogIn.UniqueID;
		}

		protected void btnLogIn_Click(object sender, EventArgs e)
		{
			if (FormsAuthentication.Authenticate(txtUsername.Text.Trim(), txtPassword.Text.Trim()))
			{
				FormsAuthentication.RedirectFromLoginPage(txtUsername.Text.Trim(), chkRemember.Checked);
			}
			else
			{
				lblMessage.Text = "Incorrect username or password";
			}
		}
	}
}