using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace HMS.Masters.Customers
{
    public partial class CustomerView : SimpleBasePage
    {
        private int CustomerID
        {
            get { return Common.ToInt(hdnCustomerID.Value); }
        }
        private UserSetting objSetting;
        protected void Page_Load(object sender, EventArgs e)
        {
            objSetting = (UserSetting) Session["UserSetting"];
            if(!IsPostBack)
            {
                int customerID = Common.ToInt(Request.QueryString["CustomerID"]);
                hdnCustomerID.Value = customerID.ToString();
                DataTable dt = Common.GetDBResult(@"select C.FirstName+ISNULL(' '+C.MiddleName,'')+ISNULL(' '+C.LastName,'') as Name,
C.UniqueID,ISNULL(C.Address1,'')+ISNULL(','+C.Address2,'')
+ISNULL(','+C.Address3,'')+ISNULL(','+C.Area,'')+ISNULL(','+C.City,'')
+ISNULL('-'+C.PinCode,'') as Address,C.MobileNo,C.EmailID,C.IsActive,
[dbo].[GetOutstanding](C.CustomerID,0) as Outstanding from VW_Customers C
where C.CustomerID=" + customerID);

                if(dt.Rows.Count>0)
                {
                    DataRow dr = dt.Rows[0];
                    lbCustUNo.Text = Common.ToString(dr["UniqueID"]);
                    lbCustName.Text = Common.ToString(dr["Name"]);
                    lbAddress.Text = Common.ToString(dr["Address"]);
                    lbMobileNo.Text = Common.ToString(dr["MobileNo"]);
                    lbEmailID.Text = Common.ToString(dr["EmailID"]);
                    lbActive.Text = Common.ToBool(dr["IsActive"]) == true ? "Active" : "Deactive";
                    lbOutstanding.Text = Common.ToDouble(dr["Outstanding"]).ToString("0.00") + " Dr";
                }
                DataTable dtAccHistory = Common.GetDBResult(@"select top 12 B.BillID,B.BillDate,B.NetBillAmount,B.CollectedAmount,B.PaymentDate,
Case B.PaymentMode when 'ByCash' then 'By Cash' else 
'By Cheque '+B.ChequeNo+',Chq Date'+CONVERT(varchar,B.ChequeDate,103) end as PaymentMode,
UC.FirstName+ISNULL(' '+UC.LastName,'') as CollectedBy
from Bills B
left outer join Users UC ON B.CollectedBy=UC.UserID where B.CustomerID=" + customerID + " Order By BillDate desc");
                GridView1.DataSource = dtAccHistory;
                GridView1.DataBind();
                BindDevices();
            }
        }

        private void BindDevices()
        {
            string query =
                @"select CP.CANNo,CP.STBNo,CP.ConnectionType,CP.SmartCardNo
  ,SP.Name as SPName,P.PackageName from CustomerPackages CP 
  inner join Customers C ON CP.CustomerID=C.CustomerID
  left join ServiceProviders SP ON CP.ServiceProviderID=SP.ServiceProviderID
  left join Packages P ON CP.PackageID=P.PackageID where C.OperatorID={0} and CP.CustomerID={1}";
            query = String.Format(query, objSetting.OperatorID, CustomerID);

            DataTable dt = Common.GetDBResult(query);
            deviceDataList.DataSource = dt;
            deviceDataList.DataBind();
        }
    }
}