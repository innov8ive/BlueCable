using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

namespace HMS
{
    public partial class Login : System.Web.UI.Page
    {
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                Session.Clear();
                Session.Abandon();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            dateLabel.Text = "Date: " + Common.GetCurDateTime().ToString("dddd dd-MMM-yyyy");
            if (!IsPostBack)
            {
                loginidTextBox.Focus();
            }
        }
        #endregion

        #region Control Events
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bool isValid = Common.ValidateLoginPassword(loginidTextBox.Text, FormsAuthentication.HashPasswordForStoringInConfigFile(paswordTextBox.Text, FormsAuthPasswordFormat.MD5.ToString()), 0);
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            if (isValid)
            {
                if (objSetting.IsActive == false)
                {
                    messageLabel.Text = "Your login currently not active. Please contact your administrator.";
                    isValid = false;
                }
                if ((objSetting.UserType == 1 || objSetting.UserType == 3) && objSetting.RegDate.AddYears(3) < Common.GetCurDate())
                {
                    messageLabel.Text = "Your membership is expired. Please contact your administrator.";
                    isValid = false;
                }

                //yes succussfuly logged-in
                if (isValid)
                {
                    messageLabel.Text = String.Empty;
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "openHomePage", "openWin()", true);
                    Response.Redirect("~/HomePage.aspx");
                }
            }
            else
            {
                messageLabel.Text = "Login failed! Invalid LoginID or Password.";
            }

            //Not a Valid User
            if (isValid == false)
            {
                //remove the AppSetting Entry from session
                if (Session["UserSetting"] != null)
                    Session.Remove("UserSetting");
            }
        }
        #endregion
    }
}