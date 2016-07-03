using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HMSOM;
using HMSBL;
using System.Threading.Tasks;
using System.Threading;

namespace HMS.Transactions.Bill
{
    public partial class GenerateBill : SimpleBasePage
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PageID = "8";
            //objSetting = (AppSettings)Session["AppSettings"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (billdateDatePicker.Text.Trim().Length <= 0)
            {
                Common.ShowMessage("Please enter Bill Date.");
                return;
            }
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            DataTable dtCustomer = Common.GetDBResult(@"Select CustomerID,
            dbo.GetOutstanding(C.CustomerID,0) as PreOutstanding from Customers C where C.IsActive=1 and C.OperatorID=" + objSetting.OperatorID);
            BillsBL objBillsBL = new BillsBL(Common.GetConString());
            foreach (DataRow dr in dtCustomer.Rows)
            {
                int customerID = Common.ToInt(dr["CustomerID"]);
                DataTable dtPackages = Common.GetDBResult(@"Select C.CustomerID,C.PackageID,C.Discount as CustDiscount
            ,P.BasicPrice as BasicPrice,P.Discount as PkgDiscount,
            P.EntTax as EntTax,P.AddOnPrice as AddOnPrice,P.ServiceTaxPerc
            from CustomerPackages C
            inner join VW_Packages P ON C.PackageID=P.PackageID 
            where C.CustomerID=" + customerID);

                decimal addOn = 0;
                decimal basic = 0;
                decimal discount = 0;
                decimal entTax = 0;
                decimal serviceTaxPerc = 0;
                decimal total = 0;
                decimal outstanding = Common.ToDecimal(dr["PreOutstanding"]);
                foreach(DataRow drPkg in dtPackages.Rows)
                {
                    decimal temp_basic = Common.ToDecimal(drPkg["BasicPrice"]);
                    basic += temp_basic;

                    decimal temp_discount = Common.ToDecimal(drPkg["CustDiscount"]) + Common.ToDecimal(drPkg["PkgDiscount"]);
                    discount += temp_discount;

                    decimal temp_entTax = Common.ToDecimal(drPkg["EntTax"]);
                    entTax += temp_entTax;

                    serviceTaxPerc = Common.ToDecimal(drPkg["ServiceTaxPerc"]);
                    decimal temp_addon = Common.ToDecimal(drPkg["AddOnPrice"]);
                    addOn += temp_addon;

                    decimal temp_total = temp_basic + temp_addon;
                    temp_total = temp_total + (temp_total * serviceTaxPerc) / 100;
                    temp_total = temp_total + temp_entTax - temp_discount;


                    total += Math.Ceiling(temp_total);
                }
                total += outstanding;

                Bills objBills = new Bills();
                objBills.AddOnPrice = addOn;
                objBills.BillDate = Common.ToDate(billdateDatePicker.Text);
                objBills.BasicPrice = basic;
                objBills.CustomerID = customerID;
                objBills.Discount = discount;
                objBills.EntTax = entTax;
                objBills.GeneratedBy = objSetting.UserID;
                objBills.GeneratedDate = Common.GetCurDate();
                objBills.ServiceTaxPerc = serviceTaxPerc;
                objBills.Outstanding = Common.ToDecimal(dr["PreOutstanding"]);
                objBills.NetBillAmount = total;
                objBillsBL.Data = objBills;
                objBillsBL.Update();
                Common.UpdateCustomerOutstanding(customerID);
                //Common.InsertNotifyQueue(Common.ToInt(objBillsBL.Data.BillID), customerID, "Email", "Bill");
                //Common.InsertNotifyQueue(Common.ToInt(objBillsBL.Data.BillID), customerID, "SMS", "Bill");
            }
            lbMessage.Text = "Bill generated successfully. To view generated bills, please go to 'Pending Bills'";

            Thread obj = new Thread(ProcessEmailQueue);
            obj.Start();
            //Thread obj2 = new Thread(ProcessSMSQueue);
            //obj2.Start();
        }
        private void ProcessEmailQueue()
        {
            Common.ProcessEmailQueue();
        }
        private void ProcessSMSQueue()
        {
            Common.SendBillSMS();
        }
    }
}