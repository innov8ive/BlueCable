using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using SmartControls;
namespace HMS
{
    public partial class PrintBillForMail : Page
    {
        #region Private Members
        const string IMAGEPATH = "~/Ad/images";
        #endregion

        #region Propeties

        private int InvoiceID
        {
            get
            {
                if (Request.QueryString["InvoiceID"] != null)
                    return Common.ToInt(Request.QueryString["InvoiceID"]);
                return 0;
            }
        }

        private string InvoiceNo
        {
            get
            {
                if (Request.QueryString["InvoiceNo"] != null)
                    return Common.ToString(Request.QueryString["InvoiceNo"]);
                return String.Empty;
            }
        }

        #endregion

        #region Page Events

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Transactions/Bill/") + "rptNewBill.rdlc";
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportViewer1.LocalReport.EnableExternalImages = true;

                string imagePath1 = String.Empty;
                string imagePath2 = String.Empty;
                string imagePath3 = String.Empty;
                foreach (FileInfo file in new DirectoryInfo(Server.MapPath(IMAGEPATH)).GetFiles())
                {
                    if (file.Name.ToLower().Contains("image1"))
                        imagePath1 = new Uri(HostingEnvironment.MapPath(IMAGEPATH + "/" + file.Name)).AbsoluteUri;
                    else if (file.Name.ToLower().Contains("image2"))
                        imagePath2 = new Uri(HostingEnvironment.MapPath(IMAGEPATH + "/" + file.Name)).AbsoluteUri;
                    else if (file.Name.ToLower().Contains("image3"))
                        imagePath3 = new Uri(HostingEnvironment.MapPath(IMAGEPATH + "/" + file.Name)).AbsoluteUri;
                }

                ReportParameter rptParam = new ReportParameter("ImagePath1", imagePath1);
                ReportViewer1.LocalReport.SetParameters(rptParam);
                rptParam = new ReportParameter("ImagePath2", imagePath2);
                ReportViewer1.LocalReport.SetParameters(rptParam);
                rptParam = new ReportParameter("ImagePath3", imagePath3);
                ReportViewer1.LocalReport.SetParameters(rptParam);

                DataTable dt = GetBill();
                ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);
                ReportViewer1.LocalReport.DataSources.Add(dataSource);

                ReportViewer1.LocalReport.Refresh();
                string fileName = "InvoiceBill.pdf";
                byte[] data = CreatePDF(fileName, ReportViewer1);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "inline; filename=" + fileName);
                Response.BinaryWrite(data); // create the file
                Response.Flush();
            }
        }

        #endregion

        #region Control Events


        #endregion

        #region Methods
        public byte[] CreatePDF(string fileName, ReportViewer viewer)
        {
            // Variables
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;
            byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
            return bytes;
        }
        private DataTable GetBill()
        {
            int custID = Common.ToInt(Request.QueryString["BillID"]);
            SqlConnection con = new SqlConnection(Common.GetConString());
            SqlCommand cmd = new SqlCommand(@"select * from VW_PrintBill where BillID=" + custID, con);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();
            da.Fill(ds, "T1");
            con.Close();
            return ds.Tables[0];
        }
        #endregion
    }
}
