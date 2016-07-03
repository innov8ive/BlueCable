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
    public partial class Upload : System.Web.UI.Page
    {
        #region Declaration
        static DataTable dtProduct = new DataTable();
        static string dataFileName = String.Empty;
        static CustomerDataUploader _objCustomerDataUploader;
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
        protected void btnDownload_Click(object sender,EventArgs e)
        {
            string attachment = "attachment; filename=Format_" + DateTime.Now.ToString("dd_MM_yyyy_hh_ss") + ".csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Write("FirstName,MiddleName,LastName,Address,Area,City,State,Pincode,Mobile,Email,CANNo,STBNo,SMCNo,ConnectionType,CustomerStatus,ServiceProvider,Outstanding,Package");
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

            _objCustomerDataUploader = new CustomerDataUploader("dbo.Customers", "CustomerID", Common.GetConString(), objSetting.OperatorID);
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
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "OperatorID", DataType = typeof(int) });
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "CreatedDate", DataType = typeof(string) });
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "SMSEnabled", DataType = typeof(bool) });
                dtProduct.Columns.Add(new DataColumn() { ColumnName = "EmailEnabled", DataType = typeof(bool) });

                //Updating Data
                foreach (DataRow dr in dtProduct.Rows)
                {
                    dr["OperatorID"] = objSetting.OperatorID;
                    dr["CreatedDate"] = Common.GetCurDate().ToString("yyyy-MM-dd");
                    dr["SMSEnabled"] = true;
                    dr["EmailEnabled"] = true;
                }

                try
                {
                    string columns =
                        "FirstName,MiddleName,LastName,Address1,Area,City,State,Pincode,MobileNo,EmailID,StartOutstanding,IsActive,OperatorID,CreatedDate,SMSEnabled,EmailEnabled";
                    _objCustomerDataUploader.InsertInDatabaseRowByRow(dtProduct,columns.Split(','));
                    uxStatuslb.Text = "Success Rows Inserted: " + _objCustomerDataUploader.TotalSuccessInsert
                        + ", Failed Rows : " + _objCustomerDataUploader.TotalFailedInsert;
                }
                catch (Exception Ex)
                {
                    uxStatuslb.Text = Ex.Message;
                    message = Ex.Message;
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
        public static string ValidatingData(int companyID)
        {
            _objCustomerDataUploader = new CustomerDataUploader("dbo.ProductMaster", "ProdID", Common.GetConString(),1);
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
    public class CustomerDataUploader : DataUploader
    {
        private int OperatorID;
        #region Constructor
        private CustomerDataUploader()
        {
        }
        public CustomerDataUploader(string tableName, string identityColumn, string conString,int oprID)
        {
            TableName = tableName;
            ConnectionString = conString;
            IdentityColumn = identityColumn;
            OperatorID = oprID;
            InitCustomerDataUploader();
            this.OnAfterRowInsert += new AfterRowInsert(ProductDataUploader_OnAfterRowInsert);
        }
        #endregion

        #region Events
        private bool InsertIntoCustomerPackages(CustomerPackages objCustomerPackages, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Into_CustomerPackages", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;
                cmd.Parameters.Add("@CANNo", SqlDbType.VarChar, 25).Value = objCustomerPackages.CANNo;
                cmd.Parameters.Add("@ConnectionType", SqlDbType.VarChar, 10).Value = objCustomerPackages.ConnectionType;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = objCustomerPackages.CustomerID;
                cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = objCustomerPackages.Discount;
                cmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = objCustomerPackages.PackageID;
                cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = objCustomerPackages.ServiceProviderID;
                cmd.Parameters.Add("@SmartCardNo", SqlDbType.VarChar, 25).Value = objCustomerPackages.SmartCardNo;
                cmd.Parameters.Add("@STBMakeID", SqlDbType.Int).Value = objCustomerPackages.STBMakeID;
                cmd.Parameters.Add("@STBNo", SqlDbType.VarChar, 50).Value = objCustomerPackages.STBNo;
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = objCustomerPackages.TotalPrice;
                cmd.Parameters.Add("@SrNo", SqlDbType.Int).Value = objCustomerPackages.SrNo;

                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        void ProductDataUploader_OnAfterRowInsert(DataRow dr, SqlTransaction trn, int lastID)
        {
            //Update UniqueID
            SqlCommand cmd = new SqlCommand("UpdateUniqueID");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = trn.Connection;
            cmd.Transaction = trn;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = lastID;
            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = Common.ToInt(dr["OperatorID"]);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                trn.Rollback();
            }

            //(Set QtyUnit=UOM)
            CustomerPackages objCustomerPackages = new CustomerPackages();
            objCustomerPackages.CANNo = Common.ToString(dr["CANNo"]);
            objCustomerPackages.ConnectionType = Common.ToString(dr["ConnectionType"]);
            objCustomerPackages.CustomerID = lastID;
            objCustomerPackages.Discount = 0;
            objCustomerPackages.PackageID = Common.ToInt(dr["Package"]);
            objCustomerPackages.STBNo = Common.ToString(dr["STBNo"]);
            objCustomerPackages.ServiceProviderID = Common.ToInt(dr["ServiceProvider"]);
            objCustomerPackages.SmartCardNo = Common.ToString(dr["SmartCardNo"]);
            objCustomerPackages.SrNo = 1;
            objCustomerPackages.TotalPrice = 0;
            InsertIntoCustomerPackages(objCustomerPackages, trn);
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
            List<ReferenceColumn> objSPList = GetServiceProviderList();
            List<ReferenceColumn> objPkgList = GetPackageList();
            List<ReferenceColumn> objConTypeList = GetConnectionTypeList();
            List<ReferenceColumn> objStatusList = GetStatusList();
            this.Columns = new List<UploadColumn>();

            //Add Manadatory columns
            this.Columns.Add(new UploadColumn() { ColumnName = "FirstName", ColumnType = "System.String", IsManadatory = true, DataColumnName = "FirstName", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "MiddleName", ColumnType = "System.String", IsManadatory = false, DataColumnName = "MiddleName", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "LastName", ColumnType = "System.String", IsManadatory = false, DataColumnName = "LastName", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "Address", ColumnType = "System.String", IsManadatory = false, DataColumnName = "Address1", MaxLength = 500 });
            this.Columns.Add(new UploadColumn() { ColumnName = "Area", ColumnType = "System.String", IsManadatory = true, DataColumnName = "Area", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "City", ColumnType = "System.String", IsManadatory = true, DataColumnName = "City", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "State", ColumnType = "System.String", IsManadatory = true, DataColumnName = "State", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "PinCode", ColumnType = "System.String", IsManadatory = true, DataColumnName = "PinCode", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "Mobile", ColumnType = "System.String", IsManadatory = false, DataColumnName = "MobileNo", MaxLength = 12 });
            this.Columns.Add(new UploadColumn() { ColumnName = "Email", ColumnType = "System.String", IsManadatory = false, DataColumnName = "EmailID", MaxLength = 100 });
            this.Columns.Add(new UploadColumn() { ColumnName = "CANNo", ColumnType = "System.String", IsManadatory = false, DataColumnName = "CANNo", MaxLength = 25 });
            this.Columns.Add(new UploadColumn() { ColumnName = "STBNo", ColumnType = "System.String", IsManadatory = false, DataColumnName = "STBNo", MaxLength = 50 });
            this.Columns.Add(new UploadColumn() { ColumnName = "SMCNo", ColumnType = "System.String", IsManadatory = false, DataColumnName = "SmartCardNo", MaxLength = 25 });
            this.Columns.Add(new UploadColumn() { ColumnName = "ConnectionType", ColumnType = "System.String", IsManadatory = true, DataColumnName = "ConnectionType", MaxLength = 10, RefList = objConTypeList });
            this.Columns.Add(new UploadColumn() { ColumnName = "CustomerStatus", ColumnType = "System.String", IsManadatory = true, DataColumnName = "IsActive", MaxLength = 10, RefList = objStatusList });
            this.Columns.Add(new UploadColumn() { ColumnName = "ServiceProvider", ColumnType = "System.String", IsManadatory = true, DataColumnName = "ServiceProvider", MaxLength = 50, RefList = objSPList });
            this.Columns.Add(new UploadColumn() { ColumnName = "Outstanding", ColumnType = "System.Int32", IsManadatory = true, DataColumnName = "StartOutstanding" });
            this.Columns.Add(new UploadColumn() { ColumnName = "Package", ColumnType = "System.String", IsManadatory = true, DataColumnName = "Package", MaxLength = 50, RefList = objPkgList });
        }
        private List<ReferenceColumn> GetServiceProviderList()
        {
            List<ReferenceColumn> objUnitList = new List<ReferenceColumn>();
            DataTable dt = Common.GetDBResult("select ServiceProviderID,Name from dbo.ServiceProviders");
            foreach (DataRow dr in dt.Rows)
                objUnitList.Add(new ReferenceColumn() { Text = Common.ToString(dr["Name"]), Value = Common.ToString(dr["ServiceProviderID"]) });

            return objUnitList;
        }
        private List<ReferenceColumn> GetPackageList()
        {
            List<ReferenceColumn> objSysPkgUnitList = new List<ReferenceColumn>();
            DataTable dt = Common.GetDBResult("select PackageID,PackageName from dbo.Packages where OperatorID="+OperatorID);
            foreach (DataRow dr in dt.Rows)
                objSysPkgUnitList.Add(new ReferenceColumn() { Text = Common.ToString(dr["PackageName"]), Value = Common.ToString(dr["PackageID"]) });

            return objSysPkgUnitList;
        }
        private List<ReferenceColumn> GetConnectionTypeList()
        {
            List<ReferenceColumn> list = new List<ReferenceColumn>();
            list.Add(new ReferenceColumn() { Text = "Normal", Value = "Normal" });
            list.Add(new ReferenceColumn() { Text = "HD", Value = "HD" });
            return list;
        }
        private List<ReferenceColumn> GetStatusList()
        {
            List<ReferenceColumn> list = new List<ReferenceColumn>();
            list.Add(new ReferenceColumn() { Text = "Active", Value = "1" });
            list.Add(new ReferenceColumn() { Text = "Deactive", Value = "0" });
            return list;
        }
        #endregion
    }

    #region Generic Data Uploader
    public class UploadColumn
    {
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        public bool IsManadatory { get; set; }
        public string DataColumnName { get; set; }
        public int MaxLength { get; set; }
        public bool RestrictDuplicate { get; set; }
        public List<ReferenceColumn> RefList { get; set; }
    }
    public class ReferenceColumn
    {
        public string Value { get; set; }
        public string Text { get; set; }
    }
    public abstract class DataUploader
    {
        public DataUploader() { }
        protected string TableName;
        protected string IdentityColumn;
        protected string ConnectionString;
        private string fileData = String.Empty;
        public abstract List<UploadColumn> Columns { get; set; }
        private DataTable _Data;
        public DataTable Data { get { return _Data; } }
        public int TotalSuccessInsert;
        public int TotalFailedInsert;
        public bool IsDataValid(ref string message, string fileName)
        {
            bool isValid = true;
            int rowNumber = 0;
            int colNumber = 0;
            char colSep = ',';
            string colName = String.Empty;
            string colType = String.Empty;
            string dataColName = String.Empty;
            List<ReferenceColumn> refCols = null;
            ReferenceColumn refCol = null;
            DataRow dr = null;
            int tempInt;
            double tempDouble;
            DateTime tempDate;
            string colContent = String.Empty;

            if (Columns == null || Columns.Count <= 0)
            {
                message = "No Data Columns Specified";
                return false;
            }
            CreateDataTable();
            //Read file data
            StreamReader dataFileReader = new StreamReader(fileName);
            string dataContents = dataFileReader.ReadToEnd();
            dataFileReader.Close();
            DataTable dt = CSVParser.CSVParser.Parse(dataContents, true, colSep);

            //Each row
            foreach (DataRow lineContent in dt.Rows)
            {
                rowNumber++;
                colNumber = 0;

                //Check Valid No of Columns
                if (dt.Columns.Count != Columns.Count)
                {
                    message = "No of Column mismatch at row " + (colNumber + 1).ToString();
                    return false;
                }
                //Match Column
                foreach (DataColumn col in dt.Columns)
                {
                    colName = Columns[colNumber].ColumnName;
                    if (colName.ToLower() != col.ColumnName.ToLower())
                    {
                        isValid = false;
                        message = "Column [" + Columns[colNumber].ColumnName + "] not found at " + (colNumber + 1).ToString() + " position.";
                        return false;
                    }
                    colNumber++;
                }
                colNumber = 0;
                dr = Data.NewRow();
                //Each column
                foreach (DataColumn objCol in dt.Columns)
                {
                    colName = Columns[colNumber].ColumnName;
                    colContent = Common.ToString(lineContent[objCol.ColumnName]);
                    dataColName = Columns[colNumber].DataColumnName;
                    colType = Columns[colNumber].ColumnType;
                    refCols = Columns[colNumber].RefList;
                    {
                        //Check Blank
                        if (colContent.Trim() == String.Empty && Columns[colNumber].IsManadatory == true)
                        {
                            message += ",\n " + colName + " is blank at row " + rowNumber;
                        }

                        //Check Max Length
                        if (Columns[colNumber].MaxLength > 0 && colContent.Trim().Length > Columns[colNumber].MaxLength)
                        {
                            message += ",\n " + colName + " length is greater than " + Columns[colNumber].MaxLength + " at row " + rowNumber;
                        }

                        //Check Duplicate
                        if (Columns[colNumber].RestrictDuplicate && Data.Select(dataColName + "='" + Common.ToString(colContent.Trim()) + "'").Length > 0)
                        {
                            message += ",\n " + colName + " duplicate [" + colContent.Replace("\r", "") + "] at row " + rowNumber;
                        }

                        //Check DataType
                        if (!(Columns[colNumber].IsManadatory == false && colContent.Replace("\r", "").Length <= 0))
                            switch (colType.ToLower())
                            {
                                case "system.int32":
                                    tempInt = 0;
                                    if (!int.TryParse(colContent.Replace("\r", ""), out tempInt))
                                        message += ",\n " + colName + " value [" + colContent + "] is not Integer at row " + rowNumber;
                                    dr[dataColName] = tempInt;
                                    break;
                                case "system.double":
                                    tempDouble = 0;
                                    if (!double.TryParse(colContent.Replace("\r", ""), out tempDouble))
                                        message += ",\n " + colName + " value [" + colContent + "] is not Decimal at row " + rowNumber;
                                    dr[dataColName] = tempDouble;
                                    break;
                                case "system.datetime":
                                    tempDate = DateTime.Now;
                                    if (!DateTime.TryParse(colContent.Replace("\r", ""), out tempDate))
                                        message += ",\n " + colName + " value [" + colContent + "] is not DateTime at row " + rowNumber;
                                    dr[dataColName] = tempDate;
                                    break;
                                case "system.string":
                                    dr[dataColName] = colContent.Replace("\r", "").Replace("'", "''");

                                    if (refCols != null && refCols.Count > 0)
                                    {
                                        refCol = refCols.Find(Obj => Obj.Text == Common.ToString(dr[colName]));
                                        if (refCol != null)
                                            dr[dataColName] = refCol.Value;
                                        else
                                            dr[dataColName] = "0";
                                    }
                                    break;
                            }
                        else
                            dr[dataColName] = DBNull.Value;
                    }
                    colNumber++;
                }
                if (dr != null)
                    Data.Rows.Add(dr);
            }

            if (message.Length > 0 && message.StartsWith(","))
            {
                message = message.Substring(1);
                isValid = false;
            }
            return isValid;
        }
        /// <summary>
        /// Method to validate CSV data
        /// Sanjay Gupta.
        /// Rohit, Feb 10, 2014: Added CompanyID parameter.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="fileName"></param>
        /// <param name="companyID"></param>
        /// <returns></returns>
        public bool IsDataValidForInsert(ref string message, string fileName, int companyID)
        {
            int totalSuccess = 0;
            int totalFailed = 0;
            bool isValid = true;
            int rowNumber = 0;
            int colNumber = 0;
            char colSep = ',';
            string colName = String.Empty;
            string colType = String.Empty;
            string dataColName = String.Empty;
            string colMessage = String.Empty;
            List<ReferenceColumn> refCols = null;
            ReferenceColumn refCol = null;
            DataRow dr = null;
            int tempInt;
            double tempDouble;
            DateTime tempDate;
            string colContent = String.Empty;

            if (Columns == null || Columns.Count <= 0)
            {
                message = "No Data Columns Specified";
                return false;
            }
            CreateDataTable();
            //Read file data
            StreamReader dataFileReader = new StreamReader(fileName);
            string dataContents = dataFileReader.ReadToEnd();
            dataFileReader.Close();
            DataTable dt = CSVParser.CSVParser.Parse(dataContents, true, colSep);

            //Each row
            foreach (DataRow lineContent in dt.Rows)
            {
                rowNumber++;
                colNumber = 0;

                //Check Valid No of Columns
                if (dt.Columns.Count != Columns.Count)
                {
                    message = "No of Column mismatch at row " + (colNumber + 1).ToString();
                    return false;
                }
                //Match Column
                foreach (DataColumn col in dt.Columns)
                {
                    colName = Columns[colNumber].ColumnName;
                    if (colName.ToLower() != col.ColumnName.ToLower())
                    {
                        isValid = false;
                        message = "Column [" + Columns[colNumber].ColumnName + "] not found at " + (colNumber + 1).ToString() + " position.";
                        return false;
                    }
                    colNumber++;
                }
                colNumber = 0;
                dr = Data.NewRow();
                //Each column
                colMessage = String.Empty;
                foreach (DataColumn objCol in dt.Columns)
                {
                    colName = Columns[colNumber].ColumnName;
                    colContent = Common.ToString(lineContent[objCol.ColumnName]);
                    dataColName = Columns[colNumber].DataColumnName;
                    colType = Columns[colNumber].ColumnType;
                    refCols = Columns[colNumber].RefList;

                    //Check Blank
                    if (colContent.Trim() == String.Empty && Columns[colNumber].IsManadatory == true)
                    {
                        colMessage += ", " + colName + " is blank at row " + rowNumber;
                    }

                    //Check Max Length
                    if (Columns[colNumber].MaxLength > 0 && colContent.Trim().Length > Columns[colNumber].MaxLength)
                    {
                        colMessage += ", " + colName + " length is greater than " + Columns[colNumber].MaxLength + " at row " + rowNumber;
                    }

                    //Check DataType
                    if (!(Columns[colNumber].IsManadatory == false && colContent.Replace("\r", "").Length <= 0))
                        switch (colType.ToLower())
                        {
                            case "system.int32":
                                tempInt = 0;
                                if (!int.TryParse(colContent.Replace("\r", ""), out tempInt))
                                    colMessage += ",\n " + colName + " value [" + colContent + "] is not Integer at row " + rowNumber;
                                dr[dataColName] = tempInt;
                                break;
                            case "system.double":
                                tempDouble = 0;
                                if (!double.TryParse(colContent.Replace("\r", ""), out tempDouble))
                                    colMessage += ",\n " + colName + " value [" + colContent + "] is not Decimal at row " + rowNumber;
                                dr[dataColName] = tempDouble;
                                break;
                            case "system.datetime":
                                tempDate = DateTime.Now;
                                if (!DateTime.TryParse(colContent.Replace("\r", ""), out tempDate))
                                    colMessage += ",\n " + colName + " value [" + colContent + "] is not DateTime at row " + rowNumber;
                                dr[dataColName] = tempDate;
                                break;
                            case "system.string":
                                dr[dataColName] = colContent.Replace("\r", "").Trim();

                                if (refCols != null)
                                {
                                    refCol = refCols.Find(Obj => Obj.Text == Common.ToString(dr[dataColName]));
                                    if (refCol != null)
                                        dr[dataColName] = refCol.Value;
                                    else
                                    {
                                        dr[dataColName] = "0";
                                        colMessage += ",\n " + colName + " value [" + colContent + "] is not found in reference table at row " + rowNumber;
                                    }
                                }
                                break;
                        }
                    else
                        dr[dataColName] = DBNull.Value;


                    colNumber++;
                }

                if (colMessage.Length > 0)
                {
                    message += ",<br/><span style='color:red'>Failed:" + colMessage.Substring(1) + "</span>";
                    totalFailed++;
                    continue;
                }
                else if (dr != null)
                {
                    Data.Rows.Add(dr);
                    message += ",<br/><span style='color:green'>Success: row number " + rowNumber.ToString() + "</span>";
                    totalSuccess++;
                }
            }

            if (message.Length > 0 && message.StartsWith(","))
            {
                message = message.Substring(1);
            }
            TotalSuccessInsert = totalSuccess;
            TotalFailedInsert = totalFailed;
            return colMessage.Length <= 0 && message.Contains("color:red")==false;
        }
        public bool ValidateAndInsert(ref string message, string fileName)
        {
            bool IsValid = IsDataValid(ref message, fileName);
            if (IsValid)
            {
                InsertInDatabase(Data);
            }
            return IsValid;
        }
        private void CreateDataTable()
        {
            _Data = new DataTable();
            DataColumn dc;
            foreach (UploadColumn col in Columns)
            {
                dc = new DataColumn();
                dc.AllowDBNull = !col.IsManadatory;
                dc.Caption = col.DataColumnName;
                dc.ColumnName = col.DataColumnName;
                dc.ReadOnly = false;
                dc.DataType = Type.GetType(col.ColumnType);
                Data.Columns.Add(dc);
            }
        }
        public void InsertInDatabase(DataTable dt)
        {
            SqlConnection con = new SqlConnection(this.ConnectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            string[] columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            sb.Append("insert into ").Append(this.TableName).Append(" (").Append(string.Join(",", columnNames));
            sb.Append(") ");
            int counter = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (counter > 0)
                    sb.AppendLine(" UNION ALL ");
                string[] fields = row.ItemArray.Select(field => field.GetType() == typeof(DBNull) ? "null" : "'" + Common.ToString(field).Replace("'", "''") + "'").
                                                ToArray();
                sb.Append("SELECT ").Append(string.Join(",", fields));
                counter++;
            }

            SqlCommand cmd = new SqlCommand(sb.ToString(), con);
            cmd.Transaction = trn;
            try
            {
                cmd.ExecuteNonQuery();
                trn.Commit();
            }
            catch
            {
                trn.Rollback();
            }

        }
        public bool InsertInDatabaseRowByRow(DataTable dt)
        {
            SqlConnection con = new SqlConnection(this.ConnectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            string[] columnNames = dt.Columns.Cast<DataColumn>().
                                              Select(column => column.ColumnName).
                                              ToArray();
            sb.Append("insert into ").Append(this.TableName).Append(" (").Append(string.Join(",", columnNames));
            sb.Append(") ");
            StringBuilder sbCommand;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    //Fire "OnBeforeRowInsert" event if defined
                    if (this.OnBeforeRowInsert != null)
                    {
                        OnBeforeRowInsert(row);
                    }

                    sbCommand = new StringBuilder(sb.ToString());
                    string[] fields = row.ItemArray.Select(field => field.GetType() == typeof(DBNull) ? "null" : "'" + Common.ToString(field).Replace("'", "''") + "'").
                                                    ToArray();
                    sbCommand.Append("SELECT ").Append(string.Join(",", fields));

                    sbCommand.Append(";");
                    if (IdentityColumn.Length > 0)
                    {
                        sbCommand.Append("set @").Append(IdentityColumn).Append(" =@@IDENTITY");
                    }
                    SqlCommand cmd = new SqlCommand(sbCommand.ToString(), con);

                    if (IdentityColumn.Length > 0)
                    {
                        cmd.Parameters.Add("@" + IdentityColumn, SqlDbType.Int).Value = 0;
                        cmd.Parameters["@" + IdentityColumn].Direction = ParameterDirection.InputOutput;
                    }
                    cmd.Transaction = trn;

                    cmd.ExecuteNonQuery();
                    int lastID = 0;

                    if (IdentityColumn.Length > 0)
                    {
                        lastID = Common.ToInt(cmd.Parameters["@" + IdentityColumn].Value);
                    }

                    //Fire "OnAfterRowInsert" event if defined
                    if (this.OnAfterRowInsert != null)
                    {
                        OnAfterRowInsert(row, trn, lastID);
                    }
                }
                trn.Commit();
                return true;
            }
            catch (Exception Ex)
            {
                trn.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                con.Dispose();
            }
        }
        public bool InsertInDatabaseRowByRow(DataTable dt, string[] columnNames)
        {
            SqlConnection con = new SqlConnection(this.ConnectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into ").Append(this.TableName).Append(" (").Append(string.Join(",", columnNames));
            sb.Append(") ");
            StringBuilder sbCommand;
            try
            {
                foreach (DataRow row in dt.Rows)
                {
                    //Fire "OnBeforeRowInsert" event if defined
                    if (this.OnBeforeRowInsert != null)
                    {
                        OnBeforeRowInsert(row);
                    }

                    sbCommand = new StringBuilder(sb.ToString());
                    List<string> fields = new List<string>();
                    foreach (string col in columnNames)
                    {
                        if (row[col] is DBNull)
                            fields.Add("null");
                        else
                        {
                            fields.Add("'" + Common.ToString(row[col]).Replace("'", "''") + "'");
                        }
                    }
                    sbCommand.Append("SELECT ").Append(string.Join(",", fields));

                    sbCommand.Append(";");
                    if (IdentityColumn.Length > 0)
                    {
                        sbCommand.Append("set @").Append(IdentityColumn).Append(" =@@IDENTITY");
                    }
                    SqlCommand cmd = new SqlCommand(sbCommand.ToString(), con);

                    if (IdentityColumn.Length > 0)
                    {
                        cmd.Parameters.Add("@" + IdentityColumn, SqlDbType.Int).Value = 0;
                        cmd.Parameters["@" + IdentityColumn].Direction = ParameterDirection.InputOutput;
                    }
                    cmd.Transaction = trn;

                    cmd.ExecuteNonQuery();
                    int lastID = 0;

                    if (IdentityColumn.Length > 0)
                    {
                        lastID = Common.ToInt(cmd.Parameters["@" + IdentityColumn].Value);
                    }

                    //Fire "OnAfterRowInsert" event if defined
                    if (this.OnAfterRowInsert != null)
                    {
                        OnAfterRowInsert(row, trn, lastID);
                    }
                }
                trn.Commit();
                return true;
            }
            catch (Exception Ex)
            {
                trn.Rollback();
                throw new Exception(Ex.Message);
            }
            finally
            {
                con.Dispose();
            }
        }
        private DataTable GetCurrentData()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("Select * from " + TableName, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            finally
            {
                con.Dispose();
            }
            return dt;
        }
        /// <summary>
        /// Method to get data of Product Master table company wise.
        /// Rohit, Feb 10, 2014.
        /// </summary>
        /// <param name="companyID"></param>
        /// <returns></returns>
        private DataTable GetCurrentData(int companyID)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand("Select * from " + TableName + " where CompanyID = " + companyID, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
            }
            finally
            {
                con.Dispose();
            }
            return dt;
        }
        public delegate void BeforeRowInsert(DataRow dr);
        public delegate void AfterRowInsert(DataRow dr, SqlTransaction trn, int lastID);

        /// <summary>
        /// Fires just before Inserting the row in database
        /// </summary>
        public event BeforeRowInsert OnBeforeRowInsert;
        /// <summary>
        /// Fires after Inserting the row in database
        /// </summary>
        public event AfterRowInsert OnAfterRowInsert;
    }
    #endregion
}
