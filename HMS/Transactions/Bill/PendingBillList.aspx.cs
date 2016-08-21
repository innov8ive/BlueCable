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
    public partial class PendingBillList : SimpleBasePage
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
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int CustomerID = Common.ToInt(hdnCustomerID.Value);
            if (CustomerID > 0)
            {
                CustomersBL objCustomersBL = new CustomersBL(Common.GetConString());
                objCustomersBL.Delete(Common.ToInt(hdnCustomerID.Value));
                hdnCustomerID.Value = "0";
                ReadyDBList();
            }
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            CustomersDBList.AppKey = "Current";
            CustomersDBList.Query = @"
SELECT Address1,UniqueID,Address2,Address3,Area,CANNo,City,ConnectionType,Country,
LB.CustomerID,LB.BillID,EmailID,ISNULL(FirstName,'')+' '+ISNULL(MiddleName,'')+' '+ISNULL(LastName,'') as Name,IsActive,
LandlineNo,MobileNo,B.Outstanding,PinCode,VW_Customers.Remarks,ServiceProviderID,SmartCardNo,State,STBMakeID,STBNo,
VW_Customers.Outstanding as DueAmount,VW_Customers.CollectionBoy FROM VW_LatestBill LB inner join VW_Customers ON LB.CustomerID= VW_Customers.CustomerID 
inner join Bills B ON B.BillID=LB.BillID
where VW_Customers.Outstanding<>0 and IsActive =1 and 
(
UniqueID like '%'+@Name+'%'
OR (ISNULL(FirstName,'')+' '+ISNULL(LastName,'')+' '+ISNULL(MiddleName,'') like '%'+@Name+'%')
OR MobileNo like '%'+@Name+'%'
OR Area like '%'+@Name+'%'
OR STBNo like '%'+@Name+'%'
OR CANNo like '%'+@Name+'%'
OR SmartCardNo like '%'+@Name+'%'
OR EmailID like '%'+@Name+'%'
)
and OperatorID=" + objSetting.OperatorID;
            if (UserSetting.UserType == 3)//Collection Boy
            {
                CustomersDBList.Query += " and exists(select 1 from CBArea where UserID=" + UserSetting.UserID + " and (CBArea.Area=VW_Customers.Area OR VW_Customers.Area=''))";
            }
            Hashtable ht = new Hashtable();
            ht["@Name"] = txtUID.Text;
            CustomersDBList.Parameters = ht;
        }
        private void AddDBListColumns()
        {
            CustomersDBList.Columns.Add(new Column("RSRNO", "Sr. No.", 50, "RSRNO", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("UniqueID", "Unique ID", 100, "UniqueID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Name", "Name", 200, "FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("MobileNo", "Mobile No.", 100, "MobileNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Area", "Area Code", 100, "Area", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Address1", "Address", 100, "Address1", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CANNo", "CAN No.", 100, "CANNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("STBNo", "STB No.", 150, "STBNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("SmartCardNo", "Smart Card No.", 150, "SmartCardNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("DueAmount", "Due Amount", 100, "DueAmount", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CollectionBoy", "Collection Boy", 100, "CollectionBoy", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
        }
        #endregion
    }
}
