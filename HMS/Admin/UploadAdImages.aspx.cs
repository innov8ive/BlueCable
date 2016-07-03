using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace HMS
{
    public partial class UploadAdImages : SimpleBasePage
    {
        #region Page Events
        const string IMAGEPATH = "~/Ad/images";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                foreach (FileInfo file in new DirectoryInfo(Server.MapPath(IMAGEPATH)).GetFiles())
                {
                    if (file.Name.ToLower().Contains("image1"))
                        Image1.ImageUrl = IMAGEPATH + "/" + file.Name;
                    else if (file.Name.ToLower().Contains("image2"))
                        Image2.ImageUrl = IMAGEPATH + "/" + file.Name;
                    else if (file.Name.ToLower().Contains("image3"))
                        Image3.ImageUrl = IMAGEPATH + "/" + file.Name;
                }
            }
        }
        #endregion
        #region Control Events
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                UploadFile("Image1", FileUpload1, Image1, 100, 250);
            }
            catch (Exception Ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = Ex.Message;
            }
        }
        protected void btnUpload2_Click(object sender, EventArgs e)
        {
            try
            {
                UploadFile("Image2", FileUpload2, Image2, 200, 250);
            }
            catch (Exception Ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = Ex.Message;
            }
        }
        protected void btnUpload3_Click(object sender, EventArgs e)
        {
            try
            {
                UploadFile("Image3", FileUpload3, Image3, 150, 600);
            }
            catch (Exception Ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = Ex.Message;
            }
        }
        #endregion
        #region Methods
        public bool ValidateFileDimensions(FileUpload objFileUpload, int height, int width)
        {
            using (System.Drawing.Image myImage =
            System.Drawing.Image.FromStream(objFileUpload.PostedFile.InputStream))
            {
                return (myImage.Height == height && myImage.Width == width);
            }
        }
        private void UploadFile(string imageName, FileUpload objFileUpload, Image img, int height, int width)
        {
            string ext = Path.GetExtension(objFileUpload.PostedFile.FileName).ToLower();
            if (ext == ".jpg" || ext == ".png" || ext == ".gif")
            {
                if (!ValidateFileDimensions(objFileUpload, height, width))
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Please upload image in dimension " + width + "X" + height;
                    return;
                }
                List<FileInfo> files = new DirectoryInfo(Server.MapPath(IMAGEPATH)).GetFiles().ToList();
                foreach (FileInfo file in files)
                {
                    if (file.Name.ToLower().Contains(imageName))
                    {
                        file.Delete();
                    }
                }
                UploadImage(imageName + ext, objFileUpload, img);
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "File format not recognised.  Upload only jpg Image formats";
            }
        }
        private void UploadImage(string imageName, FileUpload objFileUpload, Image img)
        {
            //Stream fs = objFileUpload.PostedFile.InputStream;
            //BinaryReader br = new BinaryReader(fs);
            //Byte[] bytes = br.ReadBytes((Int32)fs.Length);
            ////insert the file into database
            //string strQuery = "update MailSetting set " + imageName + "=@Data";
            //SqlConnection con=new SqlConnection(Common.GetConString());
            //SqlCommand cmd = new SqlCommand(strQuery, con);
            //cmd.Parameters.Add("@Data", SqlDbType.Image).Value = bytes;
            //con.Open();
            //using(con)
            //{
            //    cmd.ExecuteNonQuery();
            //}
            string image1Path = Server.MapPath("~/Ad/images/" + imageName);
            objFileUpload.PostedFile.SaveAs(image1Path);
            img.ImageUrl = "~/Ad/images/" + imageName;
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = imageName + " Uploaded Successfully";
        }
        #endregion
    }
}
