using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HMSBL;

namespace HMS.Transactions.Bill
{
    public partial class ViewBill : SimpleBasePage
    {
        private UserSetting objSetting;
        protected void Page_Load(object sender, EventArgs e)
        {
            objSetting = (UserSetting)Session["UserSetting"];
            if (!IsPostBack)
            {
                if (objSetting.UserType == 3)
                {
                    txtDiscount.ReadOnly = true;
                    btnUpdateDiscount.Visible = false;
                }
                else
                {
                    txtDiscount.ReadOnly = false;
                    btnUpdateDiscount.Visible = true;
                }
                int custID = Common.ToInt(Request.QueryString["CustomerID"]);
                int billID = Common.ToInt(Request.QueryString["BillID"]);

                DataTable dt;
                if (billID == 0)
                {
                    dt = Common.GetDBResult("Select * from VW_PrintBill where CustomerID=" + custID);
                }
                else
                {
                    dt = Common.GetDBResult("Select * from VW_Bill where BillID=" + billID);
                }
                if (dt.Rows.Count > 0)
                {
                    InitPage(dt);
                }
            }
        }

        private void InitPage(DataTable dt)
        {
            DataRow dr = dt.Rows[0];
            hdnBillID.Value = Common.ToInt(dr["BillID"]).ToString();
            lbBillNo.Text = "Bill No." + Common.ToInt(dr["BillID"]);
            lbBillMonth.Text = Common.ToDate(dr["BillMonth"]).ToString("MMM-yyyy");
            lbBillDate.Text = "Bill Date." + Common.ToString(dr["BillDate"]);

            lbCustName.Text = Common.ToString(dr["Name"]);
            lbCustAddress.Text = Common.ToString(dr["Address"]);
            lbCANNo.Text = "CAN No." + Common.ToString(dr["CANNo"]);
            lbSTBNo.Text = "STB No." + Common.ToString(dr["STBNo"]);
            lbSMCNo.Text = "SMC No." + Common.ToString(dr["SmartCardNo"]);

            lbBasicPrice.Text = Common.ToDecimal(dr["BasicPrice"]).ToString("0.00");
            txtAddOnPrice.Text = Common.ToDecimal(dr["AddOnPrice"]).ToString("0.00");
            lbEntTax.Text = Common.ToDecimal(dr["EntTax"]).ToString("0.00");
            lbServiceTax.Text = Common.ToDecimal(dr["ServiceTax"]).ToString("0.00");
            hdnServiceTaxPerc.Value = Common.ToDecimal(dr["ServiceTaxPerc"]).ToString();
            lbServiceTaxPerc.Text = "Service Tax (" + Common.ToDecimal(dr["ServiceTaxPerc"]).ToString("0.00") +
                                    "%)";
            lbOutstanding.Text = Common.ToDecimal(dr["Outstanding"]).ToString("0.00");
            txtDiscount.Text = Common.ToDecimal(dr["Discount"]).ToString("0.00");
            lbTotal.Text =
                (Common.ToDecimal(dr["NetBillAmount"]) + Common.ToDecimal(dr["Discount"])).ToString("0.00");
            lbNetBillAmount.Text = Common.ToDecimal(dr["NetBillAmount"]).ToString("0.00");
            InitPageWithObjects(Common.ToInt(dr["BillID"]));
        }

        private void InitPageWithObjects(int billID)
        {
            BillsBL objBillsBL = new BillsBL(Common.GetConString());
            objBillsBL.Load(billID);
            Common.SetDDL(ddlPaymentMode, objBillsBL.Data.PaymentMode);
            txtChequeNo.Text = objBillsBL.Data.ChequeNo;
            txtRemarks.Text = objBillsBL.Data.Remarks;
            txtBranchName.Text = objBillsBL.Data.BranchName;
            txtBankName.Text = objBillsBL.Data.BankName;
            amountNE.Text = Common.ToDecimal(objBillsBL.Data.CollectedAmount, 0).Value.ToString("0.00");
            txtChequeDate.Text = objBillsBL.Data.ChequeDate == null
                                     ? String.Empty
                                     : Common.ToDate(objBillsBL.Data.ChequeDate).ToString("dd-MMM-yyyy");
        }

        protected void btnMakePayment_Click(object sender, EventArgs e)
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            int billID = Common.ToInt(hdnBillID.Value);
            BillsBL objBillsBL = new BillsBL(Common.GetConString());
            objBillsBL.Load(billID);
            objBillsBL.Data.PaymentDate = Common.GetCurDate();
            objBillsBL.Data.PaymentMode = ddlPaymentMode.SelectedValue;
            objBillsBL.Data.CollectedAmount = Common.ToDecimal(amountNE.Text);
            objBillsBL.Data.CollectedBy = objSetting.UserID;
            if (objBillsBL.Data.PaymentMode == "ByCheque")
            {
                objBillsBL.Data.BankName = txtBankName.Text;
                objBillsBL.Data.BranchName = txtBranchName.Text;
                objBillsBL.Data.ChequeNo = txtChequeNo.Text;
                objBillsBL.Data.ChequeDate = Common.ToDate(txtChequeDate.Text);
            }
            else
            {
                objBillsBL.Data.BankName = objBillsBL.Data.BranchName =
                                           objBillsBL.Data.ChequeNo = String.Empty;
                objBillsBL.Data.ChequeDate = null;
            }
            objBillsBL.Data.Remarks = txtRemarks.Text;
            if (objBillsBL.Update())
            {
                Common.UpdateCustomerOutstanding(Common.ToInt(objBillsBL.Data.CustomerID));
                lbMessage.Text = "Data Saved Successfully";
                Common.InsertNotifyQueue(Common.ToInt(objBillsBL.Data.BillID), Common.ToInt(objBillsBL.Data.CustomerID), "SMS", "Payment");
                Thread obj = new Thread(ProcessSMSQueue);
                obj.Start();
            }
            else
            {
                lbMessage.Text = "<span style='color:red;'>Data not saved!</span>";
            }
        }
        private void ProcessSMSQueue()
        {
            Common.SendPaymentSMS();
        }
        protected void btnUpdateDiscount_Click(object sender, EventArgs e)
        {
            BillsBL objBillsBL = new BillsBL(Common.GetConString());
            int billID = Common.ToInt(hdnBillID.Value);
            objBillsBL.Load(billID);
            objBillsBL.Data.Discount = Common.ToDecimal(txtDiscount.Text);
            objBillsBL.Data.AddOnPrice = Common.ToDecimal(txtAddOnPrice.Text);

            decimal basic = Common.ToDecimal(objBillsBL.Data.BasicPrice);
            decimal entTax = Common.ToDecimal(objBillsBL.Data.EntTax);
            decimal serviceTaxPerc = Common.ToDecimal(objBillsBL.Data.ServiceTaxPerc);
            decimal addon = Common.ToDecimal(objBillsBL.Data.AddOnPrice);
            decimal outstanding = Common.ToDecimal(objBillsBL.Data.Outstanding);

            decimal temp_total = basic + addon;
            temp_total = temp_total + (temp_total * serviceTaxPerc) / 100;
            temp_total = temp_total + outstanding + entTax - objBillsBL.Data.Discount.Value;
            objBillsBL.Data.NetBillAmount = Math.Ceiling(temp_total);

            objBillsBL.Update();
            Common.UpdateCustomerOutstanding(Common.ToInt(objBillsBL.Data.CustomerID));
            Common.ShowMessage("Discount/AddOn updated");
            DataTable dt = Common.GetDBResult("Select * from VW_Bill where BillID=" + billID);
            InitPage(dt);
        }
        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentMode.SelectedValue == "ByCash")
            {
                txtBankName.Enabled = txtChequeNo.Enabled = txtChequeDate.Enabled = txtBranchName.Enabled = false;
            }
            else
            {
                txtBankName.Enabled = txtChequeNo.Enabled = txtChequeDate.Enabled = txtBranchName.Enabled = true;
            }
        }
    }
}