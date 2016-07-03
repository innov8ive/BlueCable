using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Services;
using HMSBL;
using SmartControls;
namespace HMS
{
    public partial class CollectionReport : SimpleBasePage
    {
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.DataBind();
            if (!IsPostBack)
            {
                CustomersDBList.ShowSrNo = true;
                BindServiceProvidersDDL(ddlMSO);
                BindCollectionBoys(ddlExecutive);
                ReadyDBList();
            }
            AddDBListColumns();
        }
        #endregion

        #region Control Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReadyDBList();
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            CustomersDBList.ExportToExcel();
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            FillTotals();
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            CustomersDBList.AppKey = "Current";
            CustomersDBList.Query = @"
SELECT UniqueID,Area,C.Address1 as Address,
CANNo,ConnectionType,Country,
C.CustomerID,C.EmailID,
ISNULL(C.FirstName,'')+' '+ISNULL(C.MiddleName,'')+' '+ISNULL(C.LastName,'') as Name,
C.IsActive,LandlineNo,MobileNo,Outstanding,PinCode,Remarks,ServiceProviders.Name as MSO,
SmartCardNo,State,STBMakeID,STBNo,U.FirstName+' '+ISNULL(U.LastName,'') as CollectedBy,
COL.CollectedAmount FROM GetCollection(@StartDate,@EndDate) as COL
inner join VW_Customers C ON COL.CustomerID=C.CustomerID
left join Users U ON COL.CollectedBy=U.UserID 
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)
and (@CollectedBy =-1 OR COL.CollectedBy=@CollectedBy)";
            DateTime? StartDate = colDateRange.StartDate;
            DateTime? EndDate = colDateRange.EndDate;
            if (StartDate == null)
                StartDate = Common.GetCurDate().AddYears(-1);
            if (EndDate == null)
                EndDate = Common.GetCurDate();
            EndDate = EndDate.Value.AddDays(1).AddMinutes(-1);
            Hashtable hTable = new Hashtable();
            hTable["@StartDate"] = StartDate;
            hTable["@EndDate"] = EndDate;
            hTable["@OperatorID"] = objSetting.OperatorID;
            hTable["@ServiceProviderID"] = Common.ToInt(ddlMSO.SelectedValue);
            hTable["@CollectedBy"] = Common.ToInt(ddlExecutive.SelectedValue);
            CustomersDBList.Parameters = hTable;
        }
        private void AddDBListColumns()
        {
            CustomersDBList.Columns.Add(new Column("RSRNO", "Sr. No.", 50, "RSRNO", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("UniqueID", "Unique ID", 100, "UniqueID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Name", "Name", 100, "FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("MobileNo", "Mobile No.", 100, "MobileNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Area", "Area Code", 100, "Area", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Address", "Address", 150, "Address1", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CANNo", "CAN No.", 90, "CANNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("STBNo", "STB No.", 90, "STBNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("SmartCardNo", "Smart Card No.", 90, "SmartCardNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("EmailID", "EmailID", 100, "C.EmailID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("MSO", "MSO", 80, "ServiceProviders.Name", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CollectedAmount", "Amount", 100, "COL.CollectedAmount", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CollectedBy", "CollectedBy", 120, "U.FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
        }
        private void BindServiceProvidersDDL(DropDownList ddl)
        {
            DataTable dt = Common.GetDBResult("Select * from ServiceProviders where IsActive=1 Order By Name");
            ddl.DataSource = dt;
            ddl.DataValueField = "ServiceProviderID";
            ddl.DataTextField = "Name";
            ddl.DataBind();

            ddl.Items.Add(new ListItem("Others", "0"));
            ddl.Items.Insert(0, new ListItem("(Any)", "-1"));
        }
        private void BindCollectionBoys(DropDownList ddl)
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            DataTable dt = Common.GetDBResult("Select FirstName+' '+LastName as Name,UserID from Users where IsActive=1 and UserType=3 and OperatorID=" + objSetting.OperatorID + " Order By FirstName");
            ddl.DataSource = dt;
            ddl.DataValueField = "UserID";
            ddl.DataTextField = "Name";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("(Any)", "-1"));
        }

        private void FillTotals()
        {
            DateTime? StartDate = colDateRange.StartDate;
            DateTime? EndDate = colDateRange.EndDate;
            if (StartDate == null)
                StartDate = Common.GetCurDate().AddYears(-1);
            if (EndDate == null)
                EndDate = Common.GetCurDate();
            EndDate = EndDate.Value.AddDays(1).AddMinutes(-1);
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            ReportBL objReportBL = new ReportBL(Common.GetConString());

            int totalCust = objReportBL.CollectionReport_GetCustomers(objSetting.OperatorID,
                                                                      Common.ToInt(ddlMSO.SelectedValue)
                                                                      , Common.ToInt(ddlExecutive.SelectedValue),
                                                                      StartDate.Value, EndDate.Value);
            lbTotalCustomer.Text = "Total Customers: " + totalCust;

            double totalColl = objReportBL.CollectionReport_GetCollection(objSetting.OperatorID,
                                                                      Common.ToInt(ddlMSO.SelectedValue)
                                                                      , Common.ToInt(ddlExecutive.SelectedValue),
                                                                      StartDate.Value, EndDate.Value);
            lbTotalCollection.Text = "Total Collection: " + totalColl.ToString("0.00");
        }
        #endregion
    }
}
