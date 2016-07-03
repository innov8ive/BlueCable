using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace HMS.Transactions.Bill
{
    public partial class OldBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable dt = Common.GetDBResult("select * from VW_PrintBill where BillID=" + Common.ToInt(Request.QueryString["BillID"]));
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    lbAddOnPack.Text = Common.ToDecimal(dr["AddOnPrice"]).ToString("0.00");
                    lbAddress.Text = lbAddress2.Text = Common.ToString(dr["Address"]);
                    lbBaicPrice.Text = lbBasicRate.Text = Common.ToDecimal(dr["BasicPrice"]).ToString("0.00");
                    lbBillDate.Text = Common.ToDate(dr["BillDate"]).ToString("dd-MMM-yyyy");
                    lbBillingAmount.Text = lbNetBillAmount.Text = Common.ToDecimal(dr["NetBillAmount"]).ToString("0.00");
                    lbTotalAmount.Text = Common.ToDecimal(dr["TotalAmount"]).ToString("0.00");
                    lbBillNo.Text = lbBillNo2.Text = Common.ToInt(dr["BillID"]).ToString("0000000");
                    lbBillPeriod.Text = lbBillPeriod2.Text = Common.ToString(dr["BillMonth"]);
                    lbContact.Text = lbContact2.Text = Common.ToString(dr["CustomerContact"]);
                    lbContactNo.Text = lbContactNo2.Text = Common.ToString(dr["Contact"]);
                    lbCustomerName.Text = lbName.Text = Common.ToString(dr["Name"]);
                    lbDiscount.Text = Common.ToDecimal(dr["Discount"]).ToString("0.00");
                    lbDueDate.Text = Common.ToDate(dr["BillDate"]).AddDays(10).ToString("dd-MMM-yyyy");
                    lbEnterTax.Text = Common.ToDecimal(dr["EntTax"]).ToString("0.00");
                    lbEntTaxNo.Text = lbExtTaxNo.Text = Common.ToString(dr["IntTaxNo"]);
                    lbOfficeAddress.Text = lbOfficeAddress2.Text = Common.ToString(dr["OPAddress"]);
                    lbOperator2.Text = lbOperatorName.Text = Common.ToString(dr["OperatorName"]);
                    lbPlanName.Text = Common.ToString(dr["PackageName"]);
                    lbPreOutstanding.Text = Common.ToDecimal(dr["Outstanding"]).ToString("0.00");
                    lbServiceTax.Text = Common.ToDecimal(dr["ServiceTax"]).ToString("0.00");
                    lbServiceTaxPerc.Text = Common.ToDecimal(dr["ServiceTaxPerc"]).ToString("0.00");
                    lbSTCNo.Text = Common.ToString(dr["ServiceTaxNo"]);
                    lbUniqueID.Text = lbUniqueNo.Text = Common.ToString(dr["UniqueID"]);
                }
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            //StringBuilder sbOut = new StringBuilder();
            //StringWriter swOut = new StringWriter(sbOut);
            //HtmlTextWriter htwOut = new HtmlTextWriter(swOut);
            //base.Render(htwOut);
            //string sOut = sbOut.ToString();

            //byte[] data2 = GetBytes(sOut);
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "inline; filename=bill.pdf");
            //Response.BinaryWrite(data2); // create the file
            //Response.Flush();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "inline;filename=bill.pdf");
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            base.Render(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0.0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            Response.Flush();
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}