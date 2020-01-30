using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace FinalProject
{
	public class gridviewcode
	{
		public void Paging(object sender, int page)
		{
			GridView grd = sender as GridView;
			grd.PageIndex = page;
		}

		public (DataView, string) SortRecords(string oldSortExpr, string newSortExpr, string oldSortDir, DataView gridData)
		{
			string sortDir;

			if (oldSortExpr == newSortExpr)
			{
				if (oldSortDir == "ASC")
				{
					sortDir = "DESC";
				}
				else
				{
					sortDir = "ASC";
				}
			}
			else
			{
				sortDir = "ASC";
			}

			gridData.Sort = "" + newSortExpr + " " + sortDir;

			return (gridData, sortDir);
		}
	}
}