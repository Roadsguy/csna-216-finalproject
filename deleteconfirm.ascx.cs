using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace FinalProject
{
	public partial class deleteconfirm : UserControl
	{
		// Delete confirmation event
		public event CommandEventHandler confirmed;

		// Delete cancellation event
		public event Action cancelled;

		public string DeleteType
		{
			get { return Session["delConfirmType"] as string; }
			set { Session["delConfirmType"] = value; }
		}

		public string PrimaryKey
		{
			get { return Session["delPrimaryKey"] as string; }
			set { Session["delPrimaryKey"] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Session["delConfirmActive"] = "true";

			// Set label text
			StringBuilder warningText = new StringBuilder();
			warningText.Append("Confirm deletion of record");
			if (DeleteType == "multi") // Pluralize if deleting multiple records
				warningText.Append("s");
			warningText.Append("?<br />This cannot be undone!");
			lblWarning.Text = Convert.ToString(warningText);
		}

		protected void btnDeleteConfirm_Click(object sender, EventArgs e)
		{
			confirmed(sender, new CommandEventArgs("confirmed", DeleteType));
		}

		protected void btnDeleteCancel_Click(object sender, EventArgs e)
		{
			cancelled();
		}
	}
}