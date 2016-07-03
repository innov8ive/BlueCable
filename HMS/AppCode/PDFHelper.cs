using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using HMS;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Web.UI;
namespace ConSys
{
    public class DataTableToPDF
    {
        private string _Header = String.Empty;
        public string Header
        {
            get { return _Header; }
            set { _Header = value; }
        }
        private string _SubHeader = String.Empty;
        public string SubHeader
        {
            get { return _SubHeader; }
            set { _SubHeader = value; }
        }
        private float _PageTopMargin = 36;
        public float PageTopMargin
        {
            get { return _PageTopMargin; }
            set { _PageTopMargin = value; }
        }
        private float _PageBottomMargin = 36;
        public float PageBottomMargin
        {
            get { return _PageBottomMargin; }
            set { _PageBottomMargin = value; }
        }
        public void DataTableToPdf(DataTable dt, float[] widths)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.ContentType = "application/pdf";
            response.AddHeader("content-disposition", "inline;filename=Panel.pdf");
            response.Cache.SetCacheability(HttpCacheability.NoCache);
            Document doc = new Document(PageSize.A4, 6, 6, _PageTopMargin, _PageBottomMargin);
            PdfWriter writer = PdfWriter.GetInstance(doc, response.OutputStream);
            writer.PageEvent = new ITextEvents(_Header, _SubHeader);
            doc.Open();
            doc.Add(DataTableToPdfTable(dt, true, false, widths));
            doc.Close();
            response.Write(doc);
            response.End();
        }
        public PdfPTable DataTableToPdfTable(DataTable dt, bool printHeader, bool printBorder, float[] widths)
        {
            PdfPTable table = new PdfPTable(dt.Columns.Count);
            table.WidthPercentage = 100;
            table.SetWidths(widths);
            table.DefaultCell.Padding = 4;
                
            PrintTableHeader(dt, printHeader, printBorder, table);
            PdfPCell cell = null;
            Paragraph p = null;
            int align = 0;
            string ColVal = String.Empty;
            iTextSharp.text.Font fontTitle = FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.NORMAL);
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    align = 0;
                    ColVal = String.Empty;
                    if (!Convert.IsDBNull(dr[dc.ColumnName]))
                    {
                        if (dc.DataType == typeof(string))
                        {
                            align = 0;//0=Left, 1=Centre, 2=Right
                            ColVal = Common.ToString(dr[dc.ColumnName]);
                        }
                        else if (dc.DataType == typeof(decimal) || dc.DataType == typeof(double))
                        {
                            align = 2;//0=Left, 1=Centre, 2=Right
                            ColVal = Common.ToDecimal(dr[dc.ColumnName]).ToString("0.00");
                        }
                        else if (dc.DataType == typeof(int))
                        {
                            align = 2;//0=Left, 1=Centre, 2=Right
                            ColVal = Common.ToInt(dr[dc.ColumnName]).ToString();
                        }
                        else if (dc.DataType == typeof(DateTime))
                        {
                            align = 0;//0=Left, 1=Centre, 2=Right
                            ColVal = Common.ToDate(dr[dc.ColumnName]).ToString("dd-MMM-yyyy");
                        }
                    }
                    p = new Paragraph();
                    foreach (string s in ColVal.Split('`'))
                    {
                        p.Add(new Phrase(s + "\n", fontTitle));
                    }
                    cell = new PdfPCell(p);
                    cell.Padding = 4;
                    cell.HorizontalAlignment = align;
                    if (!printBorder) cell.Border = 0;
                    table.AddCell(cell);
                }
            }
            return table;
        }
        private void PrintTableHeader(DataTable dt, bool printHeader, bool printBorder, PdfPTable table)
        {
            if (printHeader)
            {
                table.HeaderRows = 1;
                PdfPCell cell = null;
                foreach (DataColumn dc in dt.Columns)
                {
                    cell = new PdfPCell(new Phrase(dc.Caption));
                    cell.HorizontalAlignment = 1;//0=Left, 1=Centre, 2=Right
                    if (!printBorder) cell.Border = 0;
                    cell.BackgroundColor = Color.GRAY;
                    cell.Padding = 4;
                    table.AddCell(cell);
                }
            }
        }
    }
    public class ITextEvents : PdfPageEventHelper
    {
        // This is the contentbyte object of the writer
        PdfContentByte cb;
        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;
        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;
        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;
        #region Fields
        private string _header;
        private string _subHeader;
        #endregion
        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }
        public string SubHeader
        {
            get { return _subHeader; }
            set { _subHeader = value; }
        }
        #endregion
        public ITextEvents(string header, string subHeader)
        {
            _header = header;
            _subHeader = subHeader;
        }
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 50);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
            }
            catch (System.IO.IOException ioe)
            {
            }
        }
        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(Font.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, Color.BLACK);
            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(Font.HELVETICA, 12f, iTextSharp.text.Font.BOLD, Color.BLACK);
            Paragraph p = new Paragraph();
            Phrase p1Header = new Phrase(Header, baseFontBig);
            p.Add(p1Header);
            if (SubHeader.Length > 0)
            {
                Phrase p2Header = new Phrase("\n" + SubHeader, baseFontNormal);
                p.Add(p2Header);
            }
            //Create PdfTable object
            PdfPTable pdfTab = new PdfPTable(1);
            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            PdfPCell pdfCell2 = new PdfPCell(p);
            String text = "Page " + writer.PageNumber + " of ";
            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(20));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(20));
            }
            //set the alignment of all three cells and set border to 0
            pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
            pdfCell2.VerticalAlignment = Element.ALIGN_MIDDLE;
            pdfCell2.Border = 0;
            //add all three cells into PdfTable
            pdfTab.AddCell(pdfCell2);
            pdfTab.TotalWidth = document.PageSize.Width;
            //pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;
            //call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
            //first param is start row. -1 indicates there is no end row and all the rows to be included to write
            //Third and fourth param is x and y position to start writing
            pdfTab.WriteSelectedRows(0, -1, 0, document.PageSize.Height - 10, writer.DirectContent);
        }
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber - 1).ToString());
            headerTemplate.EndText();
            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber - 1).ToString());
            footerTemplate.EndText();
        }
    }
}
