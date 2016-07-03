using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using HMSOM;

namespace HMS
{
    //Changes 
    //Sanjay 14 Jun 2013 (Made "DataUploader" class as Generic Uploader)
    //Sanjay 14 Jun 2013 (in "ProductDataUploader", added "ProductMaster" instead of "WH.Commodity"
    public partial class STBNumberVerify : System.Web.UI.Page
    {
        #region Declaration
        static DataTable dtProduct = new DataTable();
        static string dataFileName = String.Empty;
        static STBUploader _objCustomerDataUploader;
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        #endregion

        #region Control Events
        protected void btnExport_Click(object sender, EventArgs e)
        {
            FilterData(true);
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=Format_" + DateTime.Now.ToString("dd_MM_yyyy_hh_ss") + ".csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Write("STBNo");
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
            HttpContext.Current.Response.End();
        }
        protected void btnUpdoad_Click(object sender, EventArgs e)
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            if (csvFileUpload.HasFile == false)
            {
                Common.ShowMessage("Please select data file.");
            }
            string fileName = Server.MapPath("~/NoteAttachments/") + DateTime.Now.ToString("yyyy_MMM_dd_hh_mm_tt");
            hdnFileName.Value = fileName;
            uxStatuslb.Text = "Uploading data file...";
            //First save the file
            csvFileUpload.SaveAs(fileName);
            dataFileName = fileName;

            _objCustomerDataUploader = new STBUploader("dbo.STB", "STBNo", Common.GetConString(), objSetting.OperatorID);
            string message = String.Empty;

            //Check data is valid 
            bool isValid = _objCustomerDataUploader.IsDataValidForInsert(ref message, dataFileName, objSetting.OperatorID);
            if (isValid)
            {
                dtProduct = _objCustomerDataUploader.Data;
            }
            if (dtProduct != null && isValid)//Sanjay 04 May 2013 (if data is valid)
            {
                //Adding CompanyID and IsActive Columns
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "Name", DataType = typeof(string) });
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "IsActive", DataType = typeof(bool) });
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "IsValid", DataType = typeof(bool) });
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "Outstanding", DataType = typeof(decimal) });

                dtProduct.AcceptChanges();

                DataTable dtAll = Common.GetDBResult(@"Select C.STBNo,C.FirstName+' '+ISNULL(' '+C.LastName,'') as Name,C.Outstanding,
C.IsActive from VW_Customers C where C.OperatorID=" + objSetting.OperatorID);
                DataRow[] drFind;
                //Updating Data
                foreach (DataRow dr in dtProduct.Rows)
                {
                    drFind = dtAll.Select("STBNo='" + Common.ToString(dr["STBNo"]).Replace("'", "''") + "'");
                    if (drFind.Length > 0)
                    {
                        dr["IsValid"] = true;
                        dr["Name"] = Common.ToString(drFind[0]["Name"]);
                        dr["IsActive"] = Common.ToBool(drFind[0]["IsActive"]);
                        dr["Outstanding"] = Common.ToDecimal(drFind[0]["Outstanding"]);
                    }
                    else
                    {
                        dr["IsValid"] = false;
                    }
                }
                Session["STBData"] = dtProduct;
                uploadDiv.Visible = false;
                resultDiv.Visible = true;
            }
            uxMessagetxt.InnerHtml = message;

            //Delete file from temp storage
            DeleteFile();

            //ScriptManagerHelper.RegisterStartupScript(this, "validateandsave", "ValidatingData();", true);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            FilterData(false);
        }

        private void FilterData(bool export)
        {
            if (Session["STBData"] != null)
            {
                string[] minMax = ddlOutstanding.SelectedValue.Split(',');
                int min = Common.ToInt(minMax[0]);
                int max = Common.ToInt(minMax[1]);
                StringBuilder filter = new StringBuilder();
                if (validRadioButtonList.SelectedValue != "-1")
                    filter.Append(" IsValid=").Append(validRadioButtonList.SelectedValue);
                if (activeRadioButtonList.SelectedValue != "-1")
                    filter.Append(filter.Length > 0 ? " and " : "").Append(" IsActive=").Append(validRadioButtonList.SelectedValue);
                filter.Append(filter.Length > 0 ? " and " : "").Append(" (Outstanding IS NULL OR Outstanding <=").Append(max).Append(")");
                DataRow[] drResult = ((DataTable)Session["STBData"]).Select(filter.ToString());
                DataTable dtResult;
                if (drResult.Length > 0)
                {
                    dtResult = drResult.CopyToDataTable();
                    lbTotalCustomer.Text = "Total Customers:" + drResult.Length;
                }
                else
                {
                    dtResult = null;
                    lbTotalCustomer.Text = "Total Customers:0";
                }
                uxProductGrid.DataSource = dtResult;
                uxProductGrid.DataBind();

                if (export)
                    Common.ExportDataTable(dtResult);
            }
        }
        #endregion

        #region Page Methods
        [WebMethod]
        public static string ValidatingData(int companyID)
        {
            _objCustomerDataUploader = new STBUploader("dbo.ProductMaster", "ProdID", Common.GetConString(), 1);
            string message = String.Empty;
            //Check data is valid 
            bool isValid = _objCustomerDataUploader.IsDataValidForInsert(ref message, dataFileName, companyID);
            if (isValid)
            {
                dtProduct = _objCustomerDataUploader.Data;
            }
            return message;
        }
        [WebMethod]
        public static string SavingInDatabase(int companyID)
        {
            string message = String.Empty;
            if (dtProduct != null)
            {
                //Adding CompanyID and IsActive Columns
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "CompanyID", DataType = typeof(int) });
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "IsActive", DataType = typeof(bool) });

                //Updating Data
                foreach (DataRow dr in dtProduct.Rows)
                {
                    dr["CompanyID"] = companyID;
                    dr["IsActive"] = true;
                }


                //System.Threading.Thread.Sleep(2000);
                _objCustomerDataUploader.InsertInDatabase(dtProduct);
                return "Success Rows Inserted: " + _objCustomerDataUploader.TotalSuccessInsert
                    + ", Failed Rows : " + _objCustomerDataUploader.TotalFailedInsert;
            }
            return String.Empty;
        }
        [WebMethod]
        public static void DeleteFile()
        {
            //Delete the temporary uploaded file
            try
            {
                FileInfo file = new FileInfo(dataFileName);
                if (file.Exists) file.Delete();
            }
            catch { }
        }

        #endregion
    }
    public class STBUploader : DataUploader
    {
        private int OperatorID;
        #region Constructor
        private STBUploader()
        {
        }
        public STBUploader(string tableName, string identityColumn, string conString, int oprID)
        {
            TableName = tableName;
            ConnectionString = conString;
            IdentityColumn = identityColumn;
            OperatorID = oprID;
            InitCustomerDataUploader();
        }
        #endregion

        #region Override Methods
        private List<UploadColumn> _Columns;
        public override List<UploadColumn> Columns
        {
            get
            {
                return _Columns;
            }
            set
            {
                _Columns = value;
            }
        }
        #endregion

        #region Private Methods
        private void InitCustomerDataUploader()
        {
            this.Columns = new List<UploadColumn>();

            //Add Manadatory columns
            this.Columns.Add(new UploadColumn() { ColumnName = "STBNo", ColumnType = "System.String", IsManadatory = true, DataColumnName = "STBNo", MaxLength = 50 });
        }
        #endregion
    }
}
