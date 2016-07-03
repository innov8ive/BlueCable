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
    public partial class ActiveDeactive : System.Web.UI.Page
    {
        #region Declaration
        static DataTable dtProduct = new DataTable();
        static string dataFileName = String.Empty;
        static ActiveDeactiveUploader _objCustomerDataUploader;
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
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=Format_" + DateTime.Now.ToString("dd_MM_yyyy_hh_ss") + ".csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Write("UniqueID,Active");
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

            _objCustomerDataUploader = new ActiveDeactiveUploader("dbo.STB", "UniqueID,Active", Common.GetConString(), objSetting.OperatorID);
            string message = String.Empty;

            //Check data is valid 
            bool isValid = _objCustomerDataUploader.IsDataValidForInsert(ref message, dataFileName, objSetting.OperatorID);
            if (isValid)
            {
                dtProduct = _objCustomerDataUploader.Data;
            }
            if (dtProduct != null && isValid)//Sanjay 04 May 2013 (if data is valid)
            {
                //Adding CompanyID and IsActive C  olumns
                //Updating Data
                foreach (DataRow dr in dtProduct.Rows)
                {
                    Common.ExecuteNonQuery("Update Customers set IsActive=@IsActive where UniqueID=@UniqueID",
                        "@IsActive",SqlDbType.Int, Common.ToInt(dr["Active"]),
                        "@UniqueID",SqlDbType.VarChar, Common.ToString(dr["UniqueID"]));
                }
            }
            uxMessagetxt.InnerHtml = message;

            //Delete file from temp storage
            DeleteFile();

            //ScriptManagerHelper.RegisterStartupScript(this, "validateandsave", "ValidatingData();", true);
        }

        #endregion

        #region Page Methods
        
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
    public class ActiveDeactiveUploader : DataUploader
    {
        private int OperatorID;
        #region Constructor
        private ActiveDeactiveUploader()
        {
        }
        public ActiveDeactiveUploader(string tableName, string identityColumn, string conString, int oprID)
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
            this.Columns.Add(new UploadColumn() { ColumnName = "UniqueID", ColumnType = "System.String", IsManadatory = true, DataColumnName = "UniqueID", MaxLength = 50 });
            this.Columns.Add(new UploadColumn() { ColumnName = "Active", ColumnType = "System.Int32", IsManadatory = true, DataColumnName = "Active" });
        }
        #endregion
    }
}
