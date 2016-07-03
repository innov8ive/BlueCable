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
    public partial class CustomersDelete : SimpleBasePage
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
SELECT '<input type=""checkbox"" value=""'+Convert(varchar,CustomerID)+'"">' as chBox, C.Address1,C.UniqueID,
C.Address2,C.Address3,C.Area,C.CANNo,C.City,C.ConnectionType,C.Country,Packages.PackageName,
C.CustomerID,C.EmailID,ISNULL(C.FirstName,'')+' '+ISNULL(C.MiddleName,'')+' '+ISNULL(C.LastName,'') as Name,
case C.IsActive when 1 then 'Active' else 'Deactive' end as IsActive,C.LandlineNo,C.MobileNo,C.PinCode,C.Remarks,C.ServiceProviderID,
C.SmartCardNo,C.State,C.STBNo,C.CreatedDate,
C.Outstanding FROM VW_Customers C inner join Packages ON C.PackageID=Packages.PackageID 
where 
(
(ISNULL(FirstName,'')+' '+ISNULL(LastName,'')+' '+ISNULL(MiddleName,'') like '%'+@Name+'%')
OR C.MobileNo like '%'+@Name+'%'
OR C.Area like '%'+@Name+'%'
OR C.STBNo like '%'+@Name+'%'
OR C.CANNo like '%'+@Name+'%'
OR C.SmartCardNo like '%'+@Name+'%'
OR C.EmailID like '%'+@Name+'%'
)
and C.OperatorID=@OperatorID";
            Hashtable ht = new Hashtable();
            ht["@Name"] = txtName.Text;
            ht["@OperatorID"] = objSetting.OperatorID;
            CustomersDBList.Parameters = ht;
        }
        private void AddDBListColumns()
        {
            CustomersDBList.Columns.Add(new Column("chBox", "", 50, "chBox", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("UniqueID", "Unique ID", 100, "C.UniqueID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Name", "Name", 100, "C.FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("MobileNo", "Mobile No.", 100, "C.MobileNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Area", "Area Code", 100, "C.Area", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Address1", "Address", 100, "C.Address1", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CANNo", "CAN No.", 100, "C.CANNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("STBNo", "STB No.", 150, "C.STBNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("SmartCardNo", "Smart Card No.", 120, "C.SmartCardNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("Outstanding", "Outstanding", 70, "C.Outstanding", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("PackageName", "Package Name", 120, "PackageName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("IsActive", "Active", 100, "C.IsActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            CustomersDBList.Columns.Add(new Column("CreatedDate", "Created Date", 100, "C.CreatedDate", HorizontalAlign.Left, HorizontalAlign.Center, "dd-MMM-yyyy", true));
        }
        #endregion

        [WebMethod]
        public static void DeleteCustomers(string customerIDs)
        {
            CustomersBL objCustomersBL = new CustomersBL(Common.GetConString());
            foreach (string id in customerIDs.Split(','))
            {
                int CustomerID = Common.ToInt(id);
                if (CustomerID > 0)
                {
                    objCustomersBL.Delete(CustomerID);
                }
            }
        }
    }
}
