using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Drawing;

namespace HMS
{
    public class PrintHelper
    {
        private int m_currentPageIndex;
        private IList<Stream> m_streams;
        // Routine to provide to the report renderer, in order to
        //    save an image for each page of the report.
        private Stream CreateStream(string name,
        string fileNameExtension, Encoding encoding,
        string mimeType, bool willSeek)
        {
            Stream stream = new MemoryStream();
            m_streams.Add(stream);
            return stream;
        }
        // Export the given report as an EMF (Enhanced Metafile) file.
        public void Export(LocalReport report)
        {
            StringBuilder deviceInfo = new StringBuilder();
            deviceInfo.AppendLine("<DeviceInfo>");
            deviceInfo.AppendLine("<OutputFormat>EMF</OutputFormat>");
            deviceInfo.AppendLine("<PageWidth>8.5in</PageWidth>");
            deviceInfo.AppendLine("<PageHeight>11in</PageHeight>");
            deviceInfo.Append("<MarginTop>").Append(0).AppendLine("</MarginTop>");
            deviceInfo.Append("<MarginLeft>").Append(0).AppendLine("</MarginLeft>");
            deviceInfo.AppendLine("<MarginRight>0.25in</MarginRight>");
            deviceInfo.AppendLine("<MarginBottom>0.25in</MarginBottom>");
            deviceInfo.AppendLine("</DeviceInfo>");
            Warning[] warnings;
            m_streams = new List<Stream>();
            report.Render("Image", deviceInfo.ToString(), CreateStream,
            out warnings);
            foreach (Stream stream in m_streams)
                stream.Position = 0;
        }
        // Handler for PrintPageEvents
        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
            Metafile(m_streams[m_currentPageIndex]);
            // Adjust rectangular area with printer margins.
            Rectangle adjustedRect = new Rectangle(
            ev.PageBounds.Left - (int)ev.PageSettings.HardMarginX,
            ev.PageBounds.Top - (int)ev.PageSettings.HardMarginY,
            ev.PageBounds.Width,
            ev.PageBounds.Height);
            // Draw a white background for the report
            ev.Graphics.FillRectangle(Brushes.White, adjustedRect);
            // Draw the report content
            ev.Graphics.DrawImage(pageImage, adjustedRect);
            // Prepare for the next page. Make sure we haven't hit the end.
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }
        public void Print()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }
        public void ExportPDF(ReportViewer ReportViewer1, string fileName)
        {
            //get your report datasource from the database
            LocalReport report = ReportViewer1.LocalReport;
            //code to render report as pdf document
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            Warning[] warnings = null;
            string[] streamids = null;
            //
            byte[] byteArray = report.Render("PDF", null,
            out mimeType, out encoding, out extension, out streamids, out warnings);
            //
            HttpContext.Current.Response.ContentType = "Application/pdf";
            HttpContext.Current.Response.AddHeader("Content-Disposition",
            "attachment; filename=" + fileName);
            HttpContext.Current.Response.AddHeader("Content-Length",
            byteArray.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.BinaryWrite(byteArray);
            HttpContext.Current.Response.End();
        }
        public MemoryStream GetPDFStream(ReportViewer ReportViewer1)
        {
            //get your report datasource from the database
            LocalReport report = ReportViewer1.LocalReport;
            //code to render report as pdf document
            string encoding = String.Empty;
            string mimeType = String.Empty;
            string extension = String.Empty;
            Warning[] warnings = null;
            string[] streamids = null;
            //
            byte[] byteArray = report.Render("PDF", null,
            out mimeType, out encoding, out extension, out streamids, out warnings);
            MemoryStream ms = new MemoryStream(byteArray);
            return ms;
        }
    }
}