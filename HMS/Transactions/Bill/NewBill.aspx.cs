using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;

namespace HMS.Transactions.Bill
{
    public partial class NewBill : System.Web.UI.Page
    {
        const string IMAGEPATH = "~/Ad/images";
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected override void Render(HtmlTextWriter writer)
        {
            string path = HttpContext.Current.Server.MapPath("~/Transactions/Bill/BillPage.html");
            string content = System.IO.File.ReadAllText(path);

            DataTable dt = Common.GetDBResult("Select * from VW_PrintBill where BillID=" + Common.ToInt(Request.QueryString["BillID"]));
            DataRow dr = dt.Rows[0];

            Regex regex = new Regex(@"{{.*}}");
            MatchCollection mactches = regex.Matches(content);
            foreach (Match match in mactches)
            {
                content = content.Replace(match.Value, Common.ToString(dr[match.Value.Replace("{{", String.Empty).Replace("}}", String.Empty)]));
            }

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

            content = content.Replace("image1.png", imagePath1);
            content = content.Replace("image2.png", imagePath2);
            content = content.Replace("image3.png", imagePath3);
            CreatePDF(content);
        }
        private void CreatePDF(string strPageContent)
        {
            MemoryStream objStream = new MemoryStream();
            HttpResponse response = HttpContext.Current.Response;
            //string strFileName = HttpContext.Current.Server.MapPath("test.pdf");
            // step 1: creation of a document-object
            Document document = new Document();
            // step 2:
            // we create a writer that listens to the document
            PdfWriter writer = PdfWriter.GetInstance(document, objStream);
            StringReader se = new StringReader(strPageContent);
            document.Open();
            XMLWorkerHelper.GetInstance().ParseXHtml(writer, document, se);
            document.Close();

            byte[] pdfBytes = objStream.ToArray();
            objStream.Close();
            objStream.Dispose();

            response.Clear();
            response.ContentType = "application/pdf";
            response.Buffer = true;
            response.AddHeader("content-disposition", "inline;filename="+Common.ToInt(Request.QueryString["BillID"])+".pdf");
            response.OutputStream.Write(pdfBytes, 0, pdfBytes.Length);
            response.End();
        }
    }
}