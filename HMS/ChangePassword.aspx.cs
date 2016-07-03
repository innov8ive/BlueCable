using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HMS
{
    public partial class ChangePassword1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptInclude("common", ResolveUrl("~/js/Validation.js"));
        }
        [WebMethod]
        public static string ChangePass(string newPass, string oldPass)
        {
            UserSetting sett = (UserSetting)HttpContext.Current.Session["UserSetting"];

            if (Common.ChangePassword(sett.UserID, oldPass, newPass))
            {
                return "Password changed.";
            }
            else
            {
                return "Wrong old password. Password not changed.";
            }
        }
    }
}