using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.IO;

namespace FinalProject.prescriptions
{
	public partial class prescriptions_search : UCPageType
	{
		public encryption cipher = new encryption();
        gridviewcode grd = new gridviewcode();

        protected void Page_Load(object sender, EventArgs e)
		{
            // Put previously typed values back into text boxes, if any (used on return from vieweditadd page)
            txtSrchRxNo.Text = Convert.ToString(Session["srchRxNo"]);
            txtSrchPatientID.Text = Convert.ToString(Session["srchPatientID"]);
            txtSrchDrugID.Text = Convert.ToString(Session["srchDrugID"]);
            txtSrchPhysicianID.Text = Convert.ToString(Session["srchPhysicianID"]);
            // Establish GridView event handlers
            grdPrescriptions.PageIndexChanging += new GridViewPageEventHandler(grdPrescriptions_PageIndexChanging);
            grdPrescriptions.Sorting += new GridViewSortEventHandler(grdPrescriptions_Sorting);

            if (Convert.ToString(Session["srchPatHasSearched"]) == "true") // Check if search already performed
            {
                // Repopulate GridView
                PopulateGridView();
            }

            // Keep delete confirmation loaded on partial postback, if already loaded
            if (Convert.ToString(Session["delConfirmActive"]) == "true")
            {
                switch (Convert.ToString(Session["delConfirmType"])) // Check whether single or multi delete
                {
                    case "single":
                        // Load single delete confirmation
                        Delete_Click(sender, new CommandEventArgs("delete", Convert.ToString(Session["delPrescriptionID"])));
                        break;
                    case "multi":
                        // Load multi delete confirmation
                        btnDeleteChecked_Click(sender, e);
                        break;
                }
            }
        }

        protected void PopulateGridView()
        {
            // Get text box session values
            string RxNo = Convert.ToString(Session["srchRxNo"]);
            string PatientID = Convert.ToString(Session["srchPatientID"]);
            string DrugID = Convert.ToString(Session["srchDrugID"]);
            string PhysicianID = Convert.ToString(Session["srchPhysicianID"]);
            // Initiate data tier
            LouisDataTier aPatient = new LouisDataTier();
            DataSet prescriptionData = new DataSet();
            prescriptionData = aPatient.SearchPrescriptions(RxNo, PatientID, DrugID, PhysicianID);

            // Populate datagrid with dataset
            grdPrescriptions.DataSource = prescriptionData.Tables[0];
            grdPrescriptions.DataBind();

            // Show/Enable Delete Selected button if table has rows
            if (grdPrescriptions.Rows.Count > 0)
            {
                btnDeleteChecked.Enabled = true;
                btnDeleteChecked.Visible = true;
            }
            else // Table empty, hide Delete Selected button
            {
                btnDeleteChecked.Enabled = false;
                btnDeleteChecked.Visible = false;
            }

            // Set value that patient search has been performed
            Session["srchPatHasSearched"] = "true";

            // Delete any cached GridView data
            Cache.Remove("srchPatGridDataView");
            // Add new data to cache
            Cache.Add("srchPatGridDataView", new DataView(prescriptionData.Tables[0]),
                null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.TimeSpan.FromMinutes(10),
                System.Web.Caching.CacheItemPriority.Default, null);
        }
        protected void btnDeleteChecked_Click(object sender, EventArgs e)
		{

		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
            try
            {
                // Save values in text boxes to session
                SrchPrescSaveSession();

                // Populate GridView
                PopulateGridView();
            }
            catch
            {
                // Display failure message
                RegisterAlertScript(new CommandEventArgs("script", "Failed to load prescription data"));
            }
        }

		public void View_Click(object sender, CommandEventArgs e)
		{

		}

		public void Edit_Click(object sender, CommandEventArgs e)
		{

		}

		public void Delete_Click(object sender, CommandEventArgs e)
		{

		}
	}
}