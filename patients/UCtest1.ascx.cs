using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.patients
{
	public partial class WebUserControl1 : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public void ClearTest1()
		{
			txtTest1.Text = string.Empty;
		}

		protected void btnTest1_Click(object sender, EventArgs e)
		{
			txtTest1.Text = "Test 1 worked!";
		}
	}
}