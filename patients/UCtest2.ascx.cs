using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject.patients
{
	public partial class UCtest2 : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		public void ClearTest2()
		{
			txtTest2.Text = string.Empty;
		}

		protected void btnTest2_Click(object sender, EventArgs e)
		{
			txtTest2.Text = "Test 2 works!";
		}
	}
}