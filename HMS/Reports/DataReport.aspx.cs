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
    public partial class DataReport : SimpleBasePage
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
                BindCollectionBoys(ddlCB);
                CustomersDBList.ShowSrNo = true;
                BindServiceProvidersDDL(ddlMSO);
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
            string query = @"
SELECT UniqueID,Area,C.Address1 as Address,
CANNo,ConnectionType,Country,
C.CustomerID,C.EmailID,
ISNULL(C.FirstName,'')+' '+ISNULL(C.MiddleName,'')+' '+ISNULL(C.LastName,'') as Name,
case C.IsActive when 1 then 'Active' else 'Deactive' end as IsActive,LandlineNo,MobileNo,Outstanding,PinCode,Remarks,
ISNULL(ServiceProviders.Name,'Others') as MSO,
SmartCardNo,State,STBMakeID,STBNo,C.CollectionBoy,P.PackageName as Package  FROM VW_Customers C
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
left join Packages P ON C.PackageID=P.PackageID
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)
and (@Active =-1 OR (C.IsActive=1 and @Active=1) OR (C.IsActive=0 and @Active=0))";

            if (ddlCB.SelectedIndex > 0)
                query += " and exists(select 1 from CBArea where UserID=" + Common.ToInt(ddlCB.SelectedValue) + " and (CBArea.Area=C.Area OR C.Area=''))";
            CustomersDBList.Query = query;

            Hashtable hTable = new Hashtable();
            hTable["@OperatorID"] = objSetting.OperatorID;
            hTable["@ServiceProviderID"] = Common.ToInt(ddlMSO.SelectedValue);
            hTable["@Active"] = Common.ToInt(ddlActive.SelectedValue);
            CustomersDBList.Parameters = hTable;
        }
        private void AddDBListColumns()
        {
            CustomersDBList.Columns.Add(new Column("RSRNO", "Sr. No.", 50, "RSRNO", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("UniqueID", "Unique ID", 130, "UniqueID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Name", "Name", 100, "FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("MobileNo", "Mobile No.", 80, "MobileNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Area", "Area Code", 100, "Area", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Address", "Address", 150, "Address1", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Package", "Package Name", 120, "Package", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CANNo", "CAN No.", 90, "CANNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("STBNo", "STB No.", 120, "STBNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("SmartCardNo", "Smart Card No.", 100, "SmartCardNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("EmailID", "EmailID", 100, "C.EmailID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("MSO", "MSO", 80, "ServiceProviders.Name", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("IsActive", "Status", 100, "C.IsActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("ConnectionType", "Type", 80, "C.ConnectionType", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CollectionBoy", "Collection Boy", 150, "C.CollectionBoy", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true)); 
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
            DataTable dt = Common.GetDBResult("Select FirstName+ISNULL(' '+LastName,'') as Name,UserID from Users where IsActive=1 and UserType=3 Order By FirstName");
            ddl.DataSource = dt;
            ddl.DataValueField = "UserID";
            ddl.DataTextField = "Name";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("(Any)", "0"));
        }
        private void FillTotals()
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            ReportBL objReportBL = new ReportBL(Common.GetConString());

            int totalCust = objReportBL.DateReport_GetCustomers(objSetting.OperatorID,
                                                                      Common.ToInt(ddlMSO.SelectedValue)
                                                                      , Common.ToInt(ddlActive.SelectedValue), Common.ToInt(ddlCB.SelectedValue));
            lbTotalCustomer.Text = "Total Customers: " + totalCust;
        }
        #endregion
    }
}
