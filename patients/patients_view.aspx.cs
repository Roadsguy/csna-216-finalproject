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
	public partial class patients_view : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			txtAjax.Text = Request.Form["ID"];
		}
	}
}