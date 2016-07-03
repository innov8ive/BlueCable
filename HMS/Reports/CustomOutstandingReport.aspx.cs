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
    public partial class CustomOutstandingReport : SimpleBasePage
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
                BindCB(ddlCB);
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
            CustomersDBList.Query = @"SELECT UniqueID,Area,C.Address1 as Address,
CANNo,ConnectionType,Country,
C.CustomerID,C.EmailID,
ISNULL(C.FirstName,'')+' '+ISNULL(C.MiddleName,'')+' '+ISNULL(C.LastName,'') as Name,
case C.IsActive when 1 then 'Active' else 'Deactive' end as IsActive,
LandlineNo,MobileNo,PinCode,C.Remarks,ServiceProviders.Name as MSO,
SmartCardNo,State,STBMakeID,STBNo,
C.Outstanding,CollBy.FirstName+ISNULL(' '+CollBy.LastName,'') as CollectedBy,C.CollectionBoy FROM VW_Customers C
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID  
left join VW_LatestBill VLB ON C.CustomerID=VLB.CustomerID
left join Bills B ON VLB.BillID=B.BillID
left join Users CollBy ON B.CollectedBy=CollBy.UserID
where C.OperatorID=@OperatorID and (@Paid = -1 OR @Paid = 1 and B.PaymentDate IS NOT NULL OR @Paid = 0 and B.PaymentDate IS NULL)
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID) and C.Outstanding between @Min AND @Max and C.Outstanding>0 and C.IsActive=1";
            if (ddlCB.SelectedIndex > 0)
                CustomersDBList.Query += " and exists(select 1 from CBArea where UserID=" + Common.ToInt(ddlCB.SelectedValue) + " and (CBArea.Area=C.Area OR C.Area=''))";
            int min = 0;
            int max = 0;
            string[] minMax = ddlOutstanding.SelectedValue.Split(',');
            min = Common.ToInt(minMax[0]);
            max = Common.ToInt(minMax[1]);
            Hashtable hTable = new Hashtable();
            hTable["@OperatorID"] = objSetting.OperatorID;
            hTable["@Min"] = min;
            hTable["@Max"] = max == 0 ? 10000 : max;
            hTable["@Paid"] = Common.ToInt(paidRadioButtonList.SelectedValue);
            hTable["@ServiceProviderID"] = Common.ToInt(ddlMSO.SelectedValue);
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
            CustomersDBList.Columns.Add(new Column("CANNo", "CAN No.", 90, "CANNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("STBNo", "STB No.", 90, "STBNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("SmartCardNo", "Smart Card No.", 100, "SmartCardNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("EmailID", "EmailID", 100, "C.EmailID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("MSO", "MSO", 70, "ServiceProviders.Name", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("IsActive", "Status", 70, "C.IsActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Outstanding", "Amount", 80, "C.Outstanding", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CollectedBy", "Collcted By", 100, "CollBy.FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CollectionBoy", "Collection Boy", 100, "C.CollectionBoy", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
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
        private void BindCB(DropDownList ddl)
        {
            DataTable dt = Common.GetDBResult("Select FirstName+ISNULL(' '+LastName,'') as Name,UserID from Users where IsActive=1 and UserType=3 Order By FirstName");
            ddl.DataSource = dt;
            ddl.DataValueField = "UserID";
            ddl.DataTextField = "Name";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("(All)", "0"));
        }
        private void FillTotals()
        {
            int min = 0;
            int max = 0;
            string[] minMax = ddlOutstanding.SelectedValue.Split(',');
            min = Common.ToInt(minMax[0]);
            max = Common.ToInt(minMax[1]);

            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            ReportBL objReportBL = new ReportBL(Common.GetConString());

            int totalCust = objReportBL.CustomOutstandingReport_GetCustomers(objSetting.OperatorID,
                                                                      Common.ToInt(ddlMSO.SelectedValue),
                                                                      Common.ToInt(paidRadioButtonList.SelectedValue),
                                                                      min, max, UserSetting.UserType, Common.ToInt(ddlCB.SelectedValue));
            lbTotalCustomer.Text = "Total Customers: " + totalCust;

            double totalColl = objReportBL.CustomOutstandingReport_GetCollection(objSetting.OperatorID,
                                                                      Common.ToInt(ddlMSO.SelectedValue),
                                                                     Common.ToInt(paidRadioButtonList.SelectedValue),
                                                                      min, max, UserSetting.UserType,Common.ToInt(ddlCB.SelectedValue));
            lbTotalCollection.Text = "Total Outstanding: " + totalColl.ToString("0.00");
        }
        #endregion
    }
}
