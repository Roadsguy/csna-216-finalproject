using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalProject
{
	public class BasePage : Page
	{
		// Stores whether search performed
		public bool HasSearched
		{
			get
			{
				if (ViewState["hasSearched"] != null)
					return (bool)ViewState["hasSearched"];
				else
					return false;
			}
			set
			{
				ViewState["hasSearched"] = value;
			}
		}

		// Stores search field values
		public string[] SearchFields
		{
			get
			{
				if (ViewState["searchFields"] != null)
					return (string[])ViewState["searchFields"];
				else
					return new string[] { };
			}
			set
			{
				ViewState["searchFields"] = value;
			}
		}

		public string InputType
		{
			get
			{
				if (ViewState["inputType"] != null)
					return (string)ViewState["inputType"];
				else
					return "byName";
			}
			set
			{
				ViewState["inputType"] = value;
			}
		}

		// Property to determine if UserControl is loaded for first time
		public bool IsUCPostBack
		{
			get
			{
				if (ViewState["isUCPostBack"] == null)
					return false;
				else
					return true;
			}
			set
			{
				ViewState["isUCPostBack"] = true;
			}
		}

		// GridView data stored in ViewState
		public DataSet SearchData
		{
			get
			{
				if (ViewState["searchData"] == null)
					return null;
				else
					return (DataSet)ViewState["searchData"];
			}
			set
			{
				ViewState["searchData"] = value;
			}
		}
	}
}