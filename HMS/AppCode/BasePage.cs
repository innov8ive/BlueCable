using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using HMSOM;
using Microsoft.Reporting.WebForms;
using SmartControls;
using System.Web.UI.HtmlControls;
using HMSBL;
using System.Text;

namespace HMS
{
    public class SimpleBasePage : Page
    {
        private Hashtable pageSession;
        private HiddenField closedHdn;
        private HiddenField pageKeyHdn;
        public string PageID { get; set; }
        public string PageKey;
        public UserSetting UserSetting;
        public Hashtable PageSession
        {
            get { return pageSession; }
            set { pageSession = value; }
        }
        private void GeneratePageKey()
        {
            string PageKeyCount = this.PageID + "pagecount";
            if (PageKey == null || PageKey.Length <= 0)
            {
                if (!IsPostBack)
                {

                    int pagecount = 0;
                    if (Session[PageKeyCount] == null)
                        pagecount = 0;
                    else
                    {
                        pagecount = Convert.ToInt32(Session[PageKeyCount]);
                        pagecount++;
                    }
                    Session[PageKeyCount] = pagecount;
                    this.PageKey = "Page" + this.PageID + pagecount;
                    this.pageKeyHdn.Value = this.PageKey;
                    //closedHdn.Value = "false";

                }
                else
                    this.PageKey = Common.ToString(Request.Form[this.PageID + "pk"]);
            }
            //ScriptManager.RegisterHiddenField(this.PageID + "pk", this.PageKey);
            //ClientScriptProxy.Current.RegisterHiddenField(this, this.PageID + "pk", this.PageKey);
        }

        private bool _SaveAllowed = false;
        public bool SaveAllowed
        {
            get { return _SaveAllowed; }
        }
        private bool _EditAllowed = false;
        public bool EditAllowed
        {
            get { return _EditAllowed; }
        }
        private bool _DeleteAllowed = false;
        public bool DeleteAllowed
        {
            get { return _DeleteAllowed; }
        }
        private bool _ViewAllowed = false;
        public bool ViewAllowed
        {
            get { return _ViewAllowed; }
        }
        private string _Permission = "";
        public string Permission
        {
            get { return _Permission; }
        }

        protected override void OnInit(EventArgs e)
        {
            Page.Init += delegate(object sender, EventArgs e_Init)
            {
                if (ScriptManager.GetCurrent(Page) == null)
                {
                    ScriptManager sMgr = new ScriptManager();
                    Page.Form.Controls.AddAt(0, sMgr);
                }
                else
                {
                    ScriptManager.GetCurrent(Page).EnableCdn = true;
                }
            };
            base.OnInit(e);
            if (Session["UserSetting"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            UserSetting = (UserSetting)Session["UserSetting"];
            //if (UserSetting.UserType != 1 && UserSetting.UserType != 2)
            //{
            //    Response.Redirect("~/Login.aspx");
            //}
            if (Request.QueryString["formID"] != null)
            {
                this.PageID = Common.ToString(Request.QueryString["formID"]);
                Session["PageID"] = PageID;
            }
            else
            {
                PageID = Common.ToString(Session["PageID"]);
            }
            FetchPermissions();
        }
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            closedHdn = new HiddenField();
            closedHdn.ID = this.PageID + "hdn";
            this.Form.Controls.Add(closedHdn);
            pageKeyHdn = new HiddenField();
            pageKeyHdn.ID = this.PageID + "pk";
            this.Form.Controls.Add(pageKeyHdn);

            GeneratePageKey();
            Page.ClientScript.RegisterHiddenField("messHdn", "");
            pageSession = Session[this.PageKey] as Hashtable;
            if (pageSession == null)
            {
                pageSession = new Hashtable();
                Session[this.PageKey] = pageSession;
            }

            HtmlMeta pageEnter = new HtmlMeta(); //Create a new instance of META tag object
            HtmlMeta pageExit = new HtmlMeta();

            pageEnter.Attributes.Add("http-equiv", "Page-Enter");
            pageEnter.Attributes.Add("content", "blendTrans(Duration=0.5)");

            pageExit.Attributes.Add("http-equiv", "Page-Exit");
            pageExit.Attributes.Add("content", "blendTrans(Duration=0.5)");

            Page.Header.Controls.Add(pageEnter);
            Page.Header.Controls.Add(pageExit);
            string additionalCSS = @"<style type='text/css'>
.selectedRow{ background-color: #CECECE;}
</style>";
            this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), "basicCss", additionalCSS);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Page.Header.DataBind();
            Page.ClientScript.RegisterClientScriptInclude("commonJS", ResolveUrl("~/js/Validation.js"));
        }
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            if (closedHdn == null) return;
            if (closedHdn.Value == "true")
            {
                for (int i = 0; i < Session.Keys.Count; i++)
                {
                    if (Session.Keys[i] == this.PageKey)
                    {
                        Session.Remove(this.PageKey);
                    }
                }
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "close", "<script>this.close();</script>");
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "loadingSC", getLoadingScript());
        }
        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "runLS", runLoadingScript());
        }
        private string getLoadingScript()
        {
            string script = @" <div id='dvLoading' style='position: absolute; display: none;z-index:1001;border:1px solid #D4D4D4;padding:20px;background-color:silver;font-family:arial;font-size:12px;'>
<table cellpadding='0' cellspacing='0'>
<tr>
<td>
<img src='" + Page.ResolveClientUrl("~/image/working.gif") + @"' style='height: 20px;' />
</td>
<td>
&nbsp;Loading...
</td>
</tr>
</table>
</div>
<div id='dvMask' style='background-color:#D4D4D4 ; position:absolute;left:0px;top:0px;z-index:1000;cursor:wait;opacity:0.4;filter:alpha(opacity=40)'></div>
<script language='javascript' type='text/javascript'>
Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(){beginReq()});
var ReqBeginTime = null;
function beginReq(loadingMessage) {
var dvMask = document.getElementById('dvMask');
dvMask.style.display = 'block';
dvMask.style.height = document.body.offsetHeight + 'px';
dvMask.style.width = document.body.offsetWidth + 'px';
var dvLoading = document.getElementById('dvLoading');
dvLoading.style.display = 'block';
dvLoading.style.top = ((document.documentElement.offsetHeight) / 2) + 'px';
dvLoading.style.left = ((document.documentElement.offsetWidth - 70) / 2) + 'px';
if(loadingMessage!=null)
dvLoading.children[0].rows[0].cells[1].innerHTML='&nbsp;'+loadingMessage;
else
{
var messHdn=document.getElementById('messHdn');
if(messHdn!=null && messHdn.value!='')
{
dvLoading.children[0].rows[0].cells[1].innerHTML='&nbsp;'+messHdn.value;
}
else
{
dvLoading.children[0].rows[0].cells[1].innerHTML='&nbsp;Loading...';
}
}
ReqBeginTime = new Date();
}

function endReq() {
var dvMask = document.getElementById('dvMask');
dvMask.style.display = 'none';
var dvLoading = document.getElementById('dvLoading');
dvLoading.style.display = 'none';
if (!ReqBeginTime) return;
var ReqEndTime = new Date();
window.status = 'Last server response took ' + ((ReqEndTime.getTime() - ReqBeginTime.getTime())/1000).toFixed(0) + ' second(s)';
var messHdn=document.getElementById('messHdn');
if(messHdn!=null && messHdn.value!='')
{
messHdn.value='';
}
}
function setCustomLoadingMessage(message){
var messHdn=document.getElementById('messHdn');
if(messHdn!=null)
{
messHdn.value=message;
}
return true;
}

function ConfirmClose(){
if(confirm('Do you want to close this window?'))
{
return setCustomLoadingMessage('Closing...');
}
else
return false;
}
function openWindow(url, WH, args, feature) {
var width = WH.width;
var height = WH.height;
var diaT = (screen.height - height) / 2;
var diaL = (screen.width - width) / 2;
var features = 'width=' + width + ',height=' + height + ',center=yes,' +
'top=' + diaT + ',left=' + diaL + ',' + feature+',status=yes';
return win = window.open(url, '', features, '');
}
function getParent() {
var win = window;
var winPar = win.parent;
while (win.location.href.length > 0) {
if (win.location.href == winPar.location.href) {
return winPar;
}
win = winPar;
winPar = winPar.parent;
}
return winPar;
}
";
            script += " </script>";
            return script;
        }
        private string runLoadingScript()
        {
            string script = @"<script language='javascript' type='text/javascript'> Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
</script>";
            return script;
        }
        private void FetchPermissions()
        {
            if (this.PageID.Length <= 0) return;
            if (UserSetting.UserType == 2) //Admin
            {
                //Admin User, give all access
                _SaveAllowed = _EditAllowed = _DeleteAllowed = _ViewAllowed = true;
                _Permission = "Allowed";
            }
            else  //Normal User
            {
                string permissions =
                    Common.ToString(
                        Common.GetDBScalarValue(
                            "Select ','+ISNULL(Permissions,'')+','+ISNULL(ExtPermissions,'')+',' from UserPermission where UserID=" +
                            UserSetting.UserID + " and FormID=" + this.PageID)).ToLower();
                _Permission = permissions;
                _SaveAllowed = permissions.Contains(",add,");
                _EditAllowed = permissions.Contains(",update,");
                _DeleteAllowed = permissions.Contains(",delete,");
                _ViewAllowed = permissions.Contains(",view,");
                if (permissions.Trim() == String.Empty)
                {
                    Response.Redirect("~/AccessDenied.aspx");
                }
            }
        }
    }
    public class Common
    {
        #region Common Utilities
        public static DateTime GetCurDateTime()
        {
            DateTime date = DateTime.UtcNow;
            ReadOnlyCollection<TimeZoneInfo> timeZones = TimeZoneInfo.GetSystemTimeZones();
            TimeZoneInfo indianTimeZone = timeZones.FirstOrDefault(Obj => Obj.Id == "India Standard Time");
            if (indianTimeZone != null)
                date = date.AddSeconds(indianTimeZone.BaseUtcOffset.TotalSeconds);
            return date;
        }
        public static DateTime GetCurDate()
        {
            return GetCurDateTime().Date;
        }
        public static string Serialize(object o)
        {
            ObjectStateFormatter objOSF = new ObjectStateFormatter();
            return objOSF.Serialize(o);
        }
        public static object DeSerialize(string s)
        {
            ObjectStateFormatter objOSF = new ObjectStateFormatter();
            return objOSF.Deserialize(s);
        }
        #region Value Conversions with null value check
        /// <summary>
        /// Convert any value to its boolean equivalent
        /// </summary>
        /// <param name="theValue"></param>
        /// <returns>A boolean value</returns>
        public static bool ToBool(object theValue)
        {
            try
            {
                if (Convert.IsDBNull(theValue)) return false;
                if (theValue is string)
                {
                    switch (theValue.ToString().ToLower())
                    {
                        case "yes":
                        case "on":
                            return true;
                    }
                    if (IsNumeric(theValue))
                        return Convert.ToBoolean((Convert.ToDouble(theValue)));
                }
                return Convert.ToBoolean(theValue);
            }
            catch
            {
                return false;
            }
        }
        public static string ToString(object theValue)
        {
            if (Convert.IsDBNull(theValue))
                return "";
            else
                return Convert.ToString(theValue);
        }
        public static string ConvToDateToString(object theValue, string Format)
        {
            if (Convert.IsDBNull(theValue) || theValue == null)
                return "";
            else
                return Common.ToDate(theValue).ToString(Format);
        }
        public static double ToDouble(object theValue)
        {
            double theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !double.TryParse(theValue.ToString(), out theResult))
                theResult = 0;
            return theResult;
        }
        public static double? ToDouble(object theValue, double? defaultValue)
        {
            double theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !double.TryParse(theValue.ToString(), out theResult))
                return defaultValue;
            return theResult;
        }
        public static bool? ToBool(object theValue, bool? defaultValue)
        {
            bool theResult = false;
            if (theValue == null || Convert.IsDBNull(theValue) || !bool.TryParse(theValue.ToString(), out theResult))
                return defaultValue;
            return theResult;
        }
        public static int ToInt(object theValue)
        {
            int theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !int.TryParse(theValue.ToString(), out theResult))
                theResult = 0;
            return theResult;
        }
        public static int? ToInt(object theValue, int? defaultValue)
        {
            int theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !int.TryParse(theValue.ToString(), out theResult))
                return defaultValue;
            return theResult;
        }
        public static float ToFloat(object theValue)
        {
            float theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !float.TryParse(theValue.ToString(), out theResult))
                theResult = 0;
            return theResult;
        }
        public static float? ToFloat(object theValue, float? defaultValue)
        {
            float theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !float.TryParse(theValue.ToString(), out theResult))
                return defaultValue;
            return theResult;
        }
        public static decimal ToDecimal(object theValue)
        {
            decimal theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !decimal.TryParse(theValue.ToString(), out theResult))
                theResult = 0;
            return theResult;
        }
        public static decimal? ToDecimal(object theValue, decimal? defaultValue)
        {
            decimal theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !decimal.TryParse(theValue.ToString(), out theResult))
                return defaultValue;
            return theResult;
        }
        public static DateTime ToDate(object theValue)
        {
            DateTime theResult;
            if (theValue == null || Convert.IsDBNull(theValue) || !DateTime.TryParse(theValue.ToString(), out theResult))
                theResult = DateTime.Now;
            return theResult;
        }
        public static DateTime? ToDate(object theValue, DateTime? defaultval)
        {
            DateTime theResult;
            if (theValue == null || Convert.IsDBNull(theValue) || !DateTime.TryParse(theValue.ToString(), out theResult))
                return defaultval;
            return theResult;
        }
        public static byte ToByte(object theValue)
        {
            byte theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !byte.TryParse(theValue.ToString(), out theResult))
                theResult = 0;
            return theResult;
        }
        #endregion
        public static string GetConString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["Main"].ConnectionString;
            //return "server=182.50.133.111;Database=Cable;uid=cts;pwd=cts@1234;";
        }
        public static bool IsNumeric(object theValue)
        {
            try
            {
                double theNum;
                if (Convert.IsDBNull(theValue))
                    return false;
                return double.TryParse(theValue.ToString(), out theNum);
            }
            catch
            {
                return false;
            }
        }
        public static void ShowMessage(string mess)
        {
            Page page = HttpContext.Current.Handler as Page;
            if (page != null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "shm", "alert('" + mess.Replace("'", "\"") + "');", true);
            }
        }
        public static void SetDDL(DropDownList ddl, string val)
        {
            if (ddl.Items.Count > 0)
            {
                ddl.SelectedIndex = 0;
                ddl.ClearSelection();
                ListItem itm = ddl.Items.FindByValue(val);
                if (itm != null)
                    itm.Selected = true;
            }
        }
        public static DataTable GetDBResult(string query)
        {
            SqlConnection con = new SqlConnection(Common.GetConString());
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            con.Open();
            using (con)
            {
                dt.Load(cmd.ExecuteReader());
            }
            return dt;
        }
        public static int ExecuteNonQuery(string query, params object[] param)
        {
            SqlConnection con = new SqlConnection(Common.GetConString());
            SqlCommand cmd = new SqlCommand(query, con);
            if (param != null && param.Length > 0)
                for (int i = 0; i < param.Length; i = i + 3)
                    cmd.Parameters.Add(Common.ToString(param[i]), (SqlDbType)param[i + 1]).Value = param[i + 2];
            con.Open();
            using (con)
            {
                return cmd.ExecuteNonQuery();
            }
        }
        public static object GetDBScalarValue(string query)
        {
            SqlConnection con = new SqlConnection(Common.GetConString());
            SqlCommand cmd = new SqlCommand(query, con);
            object value = null;
            con.Open();
            using (con)
            {
                value = cmd.ExecuteScalar();
            }
            return value;
        }
        public static string GetPortalLoginUrl()
        {
            return "~/Portal/Login.aspx";
        }
        /// <summary>
        /// For Validating LoginID and Password
        /// </summary>
        /// <param name="loginID"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool ValidateLoginPassword(string loginID, string Password, int userType)
        {
            SqlConnection con = new SqlConnection(GetConString());
            SqlCommand cmd = new SqlCommand(@"Select U.*,O.RegDate,O.NetworkName from Users U left outer join Operators O on U.OperatorID=O.OperatorID where U.EmailID=@LoginID and U.Password=@Password and 
            (@UserType=0 OR UserType=@UserType)", con);
            cmd.Parameters.Add("@LoginID", SqlDbType.NVarChar).Value = loginID;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;// EncryptText(Password);
            cmd.Parameters.Add("@UserType", SqlDbType.Int).Value = userType;
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();

                if (dt.Rows.Count > 0)
                {
                    UserSetting objAppSetting = new UserSetting();

                    //Login Details
                    objAppSetting.FirstName = dt.Rows[0]["FirstName"].ToString();
                    objAppSetting.LastName = dt.Rows[0]["LastName"].ToString();
                    objAppSetting.EmailID = dt.Rows[0]["EmailID"].ToString();
                    objAppSetting.Password = dt.Rows[0]["Password"].ToString();
                    objAppSetting.UserID = ToInt(dt.Rows[0]["UserID"]);
                    objAppSetting.UserType = ToInt(dt.Rows[0]["UserType"]);
                    objAppSetting.IsActive = ToBool(dt.Rows[0]["IsActive"]);
                    objAppSetting.OperatorID = ToInt(dt.Rows[0]["OperatorID"]);
                    objAppSetting.Mobile = dt.Rows[0]["Mobile"].ToString();
                    objAppSetting.NetworkName = dt.Rows[0]["NetworkName"].ToString();
                    objAppSetting.RegDate = Common.ToDate(dt.Rows[0]["RegDate"]);

                    objAppSetting.CBAreaList = new List<CBArea>();
                    DataTable dtArea = Common.GetDBResult("Select Area from CBArea where UserID=" + objAppSetting.UserID);
                    foreach (DataRow dr in dtArea.Rows)
                    {
                        objAppSetting.CBAreaList.Add(new CBArea() { Area = Common.ToString(dr["Area"]) });
                    }

                    HttpContext.Current.Session["UserSetting"] = objAppSetting;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Dispose();
            }
        }
        public static bool IsExists<T>(List<T> list, int notSearchIndex, object matchVal, string propertyName)
          where T : class
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i != notSearchIndex)
                {
                    object obj = list[i];
                    if (matchVal.Equals(obj.GetType().GetProperty(propertyName).GetValue(obj, null)))
                        return true;
                }
            }
            return false;
        }
        public static void HideButton(Button btn, bool show)
        {
            btn.Visible = show;
        }
        #endregion

        #region Appointment helpers
        public static DataTable GetFreeDoctorAppoints(int doctorID, DateTime date)
        {
            //Generate All Possible TimeSlot of Appointment for a Doctor at a given date
            DataTable resultDT = GenerateAppointment(doctorID, date);

            //Find Existing appointment data if have
            DataTable dtExistingAppt =
                Common.GetDBResult(
                    "Select * from  Appointments where DoctorID=" + doctorID +
                    " and Convert(varchar,ApptDateTime,110)='"
                    + date.ToString("MM-dd-yyyy") + "'");

            //Delete the booked appointment slots
            for (int i = 0; i < resultDT.Rows.Count; i++)
            {
                DataRow dr = resultDT.Rows[i];
                DataRow[] drExisting = dtExistingAppt.Select("ApptDateTime='" + Common.ToDate(dr["ApptDateTime"]) + "'");
                if (drExisting.Length > 0)
                {
                    resultDT.Rows.RemoveAt(i);
                    i--;
                }
            }
            return resultDT;
        }
        public static DataTable GenerateAppointment(int doctorID, DateTime date)
        {
            DataTable dtAppointSlots =
                Common.GetDBResult(
                    @"select FTS.SlotHour FromHour,FTS.SlotDesc as FromDesc,
TTS.SlotHour as ToHour,TTS.SlotDesc ToDesc,DT.TimePerPatient from dbo.DoctorTiming DT 
left outer join TimeSlots FTS ON DT.FromSlotID=FTS.SlotID
left outer join TimeSlots TTS ON DT.ToSlotID=TTS.SlotID
where DT.DoctorID=" +
                    doctorID);

            //Find the only table structure of table "Appointments"
            DataTable resultDT =
                Common.GetDBResult(
                    "select DoctorID,PatientID,PatientName,ApptDateTime,Remark,IsDone from Appointments where 1<>1");

            //Additional Columns
            resultDT.Columns.Add(new DataColumn("SrNo", typeof(int)));
            resultDT.Columns.Add(new DataColumn("AppointmentID", typeof(int)));

            int srNo = 1;
            foreach (DataRow dr in dtAppointSlots.Rows)
            {
                int fromTime = Common.ToInt(dr["FromHour"]);
                int toTime = Common.ToInt(dr["ToHour"]);
                int timePerPatient = Common.ToInt(dr["TimePerPatient"]);
                while (fromTime <= toTime)
                {
                    DataRow drAppt = resultDT.NewRow();
                    drAppt["SrNo"] = srNo;
                    drAppt["DoctorID"] = doctorID;
                    drAppt["ApptDateTime"] = date.AddMinutes(fromTime);
                    resultDT.Rows.Add(drAppt);
                    fromTime += timePerPatient;
                    srNo++;
                }
            }
            return resultDT;
        }
        #endregion

        #region Email Sending
        public static bool SendMail(string fromID, string fromPass, string toUserIDs, string smtpServer,
          bool isBodyHTML, bool enableSSL, string subject, string content)
        {
            MailMessage mail = new MailMessage();
            //Add To List
            toUserIDs = toUserIDs.Replace(";", ",");
            foreach (string emailID in toUserIDs.Split(','))
            {
                if (emailID.Length <= 0) continue;
                mail.To.Add(emailID);
            }
            mail.From = new MailAddress(fromID);//new MailAddress("YourGmailID@gmail.com");
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpServer;//"smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential(fromID, fromPass);//("YourUserName@gmail.com", "YourGmailPassword");
            smtp.EnableSsl = enableSSL;//true;
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool SendMail(MailMessage mail, string fromID, string fromPass, string toUserIDs, string smtpServer,
        bool isBodyHTML, bool enableSSL, string subject, string content)
        {
            //Add To List
            toUserIDs = toUserIDs.Replace(";", ",");
            foreach (string emailID in toUserIDs.Split(','))
            {
                if (emailID.Length <= 0) continue;
                mail.To.Add(emailID);
            }
            mail.From = new MailAddress(fromID);//new MailAddress("YourGmailID@gmail.com");
            mail.Subject = subject;
            mail.Body = content;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = smtpServer;//"smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential(fromID, fromPass);//("YourUserName@gmail.com", "YourGmailPassword");
            smtp.EnableSsl = enableSSL;//true;
            try
            {
                smtp.Send(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool SendMail(MailMessage mail, string toUserIDs, string subject, string content)
        {
            DataTable dt = Common.GetDBResult("Select *from MailSetting ");
            if (dt == null || dt.Rows.Count <= 0) return false;

            string fromID = Common.ToString(dt.Rows[0]["EmailID"]);
            string password = Common.ToString(dt.Rows[0]["Password"]);
            if (password.Length > 0)
                password = Common.DeSerialize(password).ToString();
            string mailServer = Common.ToString(dt.Rows[0]["MailServer"]);
            bool enableSSL = Common.ToBool(dt.Rows[0]["EnableSSL"]);

            return SendMail(mail, fromID, password, toUserIDs, subject, true, enableSSL, subject, content);
        }
        #endregion
        private static string PasswordEncrypt(string pass)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pass,
                                                                          FormsAuthPasswordFormat.MD5.ToString());
        }
        public static bool ChangePassword(int userID, string pass, string newPass)
        {
            SqlConnection con = new SqlConnection(Common.GetConString());
            SqlCommand cmd = new SqlCommand("Update Users set Password=@NewPass where UserID=@UserID and Password=@OldPass", con);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
            cmd.Parameters.Add("@OldPass", SqlDbType.NVarChar, 200).Value = PasswordEncrypt(pass);
            cmd.Parameters.Add("@NewPass", SqlDbType.NVarChar, 200).Value = PasswordEncrypt(newPass);

            con.Open();
            try
            {
                int rowsAffetced = 0;
                using (con)
                {
                    rowsAffetced = cmd.ExecuteNonQuery();
                }
                return rowsAffetced > 0;
            }
            catch
            {
                return false;
            }
        }
        public static bool SendSMS(string toNumber, string content)
        {
            if (toNumber.Length != 10)
                return false;
            DataTable dt = Common.GetDBResult("Select *from MailSetting ");
            if (dt == null || dt.Rows.Count <= 0) return false;

            string smsAgentURL = Common.ToString(dt.Rows[0]["SMSAgentURL"]);
            string smsUserName = Common.ToString(dt.Rows[0]["SMSUserName"]);
            string smsPassword = Common.ToString(dt.Rows[0]["SMSPassword"]);
            if (smsPassword.Length > 0)
                smsPassword = Common.DeSerialize(smsPassword).ToString();

            if (smsAgentURL.Length <= 0 || smsUserName.Length <= 0 || smsPassword.Length <= 0)
                return false;
            return SendSms(toNumber, content, smsUserName, smsAgentURL, smsPassword);
        }

        private static bool SendSms(string toNumber, string content, string smsUserName, string smsAgentURL, string smsPassword)
        {
            if (toNumber.Length != 10)
                return false;
            string smsUrl = smsAgentURL + "?user=" + smsUserName + "&password=" +
                            smsPassword + "&message=" + content + "&sender=INFORM&mobile=" + toNumber + "&type=3";
            return true;
            try
            {
                WebRequest req = HttpWebRequest.Create(smsUrl);
                WebResponse response = req.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();

            }
            catch
            {
                //No need to catch
            }
            return true;
        }

        public static bool SendSMSRegistration(string toNumber, string userName, string password)
        {
            string content = @"Dear {0}, you are successfully registered on our system.
You may log-on to our website http://hospitalsoftsolutions.in. Your password is {1}";
            content = String.Format(content, userName, password);
            SendSMS(toNumber, content);
            return true;
        }
        public static bool SendDetailsUpdated(string toNumber, string userName)
        {
            string content = @"Dear {0}, your details have been updated by the hospital.
Please log-in to http://hospitalsoftsolutions.in to note the changes made for your convenience";
            content = String.Format(content, userName);
            SendSMS(toNumber, content);
            return true;
        }
        public static bool SendAppointmentConfirmation(string toNumber, string userName, DateTime apptDate, string doctotName)
        {
            string content = @"Dear {0}, your appointment has been fixed at {1} on {2} with {3}";
            content = String.Format(content, userName, apptDate.ToString("hh:mm tt"), apptDate.ToString("dd-MMM-yyyy"),
                                    doctotName);
            SendSMS(toNumber, content);
            return true;
        }

        public static void UpdateCustomerOutstanding(int customerID)
        {
            SqlConnection con = new SqlConnection(GetConString());
            SqlCommand cmd = new SqlCommand("UpdateCustomerOutstanding", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = customerID;
            con.Open();
            using (con)
            {
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        #region Notify Queue
        public static void InsertNotifyQueue(int billID, int customerID, string notifyBy, string notifyType)
        {
            NotifyQueueBL objNotifyQueueBL = new NotifyQueueBL(Common.GetConString());
            NotifyQueue objNotifyQueue = new NotifyQueue();
            objNotifyQueue.BillID = billID;
            objNotifyQueue.CustomerID = customerID;
            objNotifyQueue.Id = 0;
            objNotifyQueue.NotifyBy = notifyBy;
            objNotifyQueue.NotifyType = notifyType;

            objNotifyQueueBL.Data = objNotifyQueue;
            objNotifyQueueBL.Update();
        }
        public static void SendBillSMS()
        {
            DataTable dtMail = Common.GetDBResult("Select *from MailSetting ");
            if (dtMail == null || dtMail.Rows.Count <= 0) return;

            string smsAgentURL = Common.ToString(dtMail.Rows[0]["SMSAgentURL"]);
            string smsUserName = Common.ToString(dtMail.Rows[0]["SMSUserName"]);
            string smsPassword = Common.ToString(dtMail.Rows[0]["SMSPassword"]);
            if (smsPassword.Length > 0)
                smsPassword = Common.DeSerialize(smsPassword).ToString();

            if (smsAgentURL.Length <= 0 || smsUserName.Length <= 0 || smsPassword.Length <= 0)
                return;
            DataTable dt = Common.GetDBResult("Select * from NotifyQueue where NotifyBy='SMS' and NotifyType='Bill'");
            foreach (DataRow dr in dt.Rows)
            {
                int customerID = Common.ToInt(dr["CustomerID"]);
                string notifyType = Common.ToString(dr["NotifyType"]);
                int billID = Common.ToInt(dr["BillID"]);
                DataTable dt2 = GetBillData(billID);
                if (dt2.Rows.Count == 0) continue;
                DataRow dr2 = dt2.Rows[0];
                string number = "";
                string message = "Dear Customer,Payment Rs {0}/-due for Bill No.{1} {2}-{3}";
                message = String.Format(message, Common.ToDecimal(dr2["NetBillAmount"]).ToString("0.#"), Common.ToString(dr2["BillID"]),
                                        Common.ToString(dr2["BillMonth"]), Common.ToString(dr2["NetworkName"]));

                SendSms(Common.ToString(dr2["MobileNo"]), message, smsUserName, smsAgentURL, smsPassword);

                NotifyQueueBL objNotifyQueueBL = new NotifyQueueBL(Common.GetConString());
                objNotifyQueueBL.Delete(Common.ToInt(dr["Id"]));
            }

        }
        public static void SendPaymentSMS()
        {
            DataTable dtMail = Common.GetDBResult("Select *from MailSetting ");
            if (dtMail == null || dtMail.Rows.Count <= 0) return;

            string smsAgentURL = Common.ToString(dtMail.Rows[0]["SMSAgentURL"]);
            string smsUserName = Common.ToString(dtMail.Rows[0]["SMSUserName"]);
            string smsPassword = Common.ToString(dtMail.Rows[0]["SMSPassword"]);
            if (smsPassword.Length > 0)
                smsPassword = Common.DeSerialize(smsPassword).ToString();

            if (smsAgentURL.Length <= 0 || smsUserName.Length <= 0 || smsPassword.Length <= 0)
                return;
            DataTable dt = Common.GetDBResult("Select * from NotifyQueue where NotifyBy='SMS' and NotifyType='Payment'");
            foreach (DataRow dr in dt.Rows)
            {
                int customerID = Common.ToInt(dr["CustomerID"]);
                string notifyType = Common.ToString(dr["NotifyType"]);
                int billID = Common.ToInt(dr["BillID"]);
                DataTable dt2 = GetBillData(billID);
                if (dt2.Rows.Count == 0) continue;
                DataRow dr2 = dt2.Rows[0];
                string number = "";
                string message = "Dear Customer,Payment Rs {0}/-received againt Bill No.{1} {2} -{3}";
                message = String.Format(message, Common.ToDecimal(dr2["CollectedAmount"]).ToString("0.#"), Common.ToString(dr2["BillID"]),
                                        Common.ToString(dr2["BillMonth"]), Common.ToString(dr2["NetworkName"]));
                SendSms(Common.ToString(dr2["MobileNo"]), message, smsUserName, smsAgentURL, smsPassword);

                NotifyQueueBL objNotifyQueueBL = new NotifyQueueBL(Common.GetConString());
                objNotifyQueueBL.Delete(Common.ToInt(dr["Id"]));
            }

        }
        public static void ProcessEmailQueue()
        {
            DataTable dt = Common.GetDBResult("Select * from NotifyQueue where NotifyBy='Email'");
            foreach (DataRow dr in dt.Rows)
            {
                int customerID = Common.ToInt(dr["CustomerID"]);
                string notifyType = Common.ToString(dr["NotifyType"]);
                int billID = Common.ToInt(dr["BillID"]);
                string message = GetBillMessage(billID);

                if (notifyType == "Bill")
                {
                    string printBillUrl = Common.GetSetting(0, "PrintBillUrl");
                    printBillUrl = printBillUrl + "?BillID=" + billID;
                    var request = WebRequest.Create(printBillUrl);
                    var response = (HttpWebResponse)request.GetResponse();
                    var dataStream = response.GetResponseStream();
                    // convert stream to string
                    byte[] data = ReadFully(dataStream);
                    dataStream.Close();
                    response.Close();

                    string CustomerEmail =
                       Common.ToString(Common.GetDBScalarValue("Select EmailID from Customers where EmailEnabled=1 and CustomerID=" +
                                                customerID));
                    if (CustomerEmail.Length > 0)
                    {
                        SendMail(CustomerEmail, "Cable Bill", message, "Bill.pdf", data);
                    }
                    NotifyQueueBL objNotifyQueueBL = new NotifyQueueBL(Common.GetConString());
                    objNotifyQueueBL.Delete(Common.ToInt(dr["Id"]));
                }
            }
        }
        public static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        public static string SendMail(string toAddress, string subject, string body, string attachmentName, byte[] attachmentBytes)
        {
            //Find default email settings
            DataTable dt = Common.GetDBResult("Select *from MailSetting ");
            if (dt == null || dt.Rows.Count <= 0) return "No Email setting found";

            string userName = Common.ToString(dt.Rows[0]["EmailID"]);
            string password = Common.ToString(dt.Rows[0]["Password"]);
            if (password.Length > 0)
                password = Common.DeSerialize(password).ToString();
            string host = Common.ToString(dt.Rows[0]["MailServer"]);
            bool enableSSL = Common.ToBool(dt.Rows[0]["EnableSSL"]);
            string displayName = Common.ToString(dt.Rows[0]["DisplayName"]);
            int port = Common.ToInt(dt.Rows[0]["Port"]);
            bool UseDefaultCredentials = false;

            var message = new MailMessage();
            //from, to, reply to
            message.From = new MailAddress(userName, displayName);
            toAddress.Replace(",", ";");
            foreach (string to in toAddress.Split(';'))
            {
                message.To.Add(new MailAddress(to));
            }

            //content
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;

            try
            {
                if (attachmentName.Length > 0 && attachmentBytes != null)
                {
                    //attachment
                    var ms = new MemoryStream(attachmentBytes);
                    var attachment = new Attachment(ms, attachmentName, "application/pdf");
                    attachment.ContentDisposition.CreationDate = DateTime.UtcNow;
                    attachment.ContentDisposition.ModificationDate = DateTime.UtcNow;
                    attachment.ContentDisposition.ReadDate = DateTime.UtcNow;
                    message.Attachments.Add(attachment);
                }

                //send email
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.UseDefaultCredentials = UseDefaultCredentials;
                    smtpClient.Host = host;
                    smtpClient.Port = port;
                    smtpClient.EnableSsl = enableSSL;
                    if (UseDefaultCredentials)
                        smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    else
                        smtpClient.Credentials = new NetworkCredential(userName, password);
                    smtpClient.Send(message);
                    return "Mail sent sucessfully";
                }
            }
            catch (Exception Ex)
            {
                return Ex.Message;
                WriteErrorLog(Ex);
            }
        }
        private static void WriteErrorLog(Exception ex)
        {
            string message = ex.Message + Environment.NewLine + ex.StackTrace;
            File.AppendAllText(HostingEnvironment.MapPath("~/ErrorLog.txt"), message);
        }
        private static string GetBillMessage(int billID)
        {
            DataTable dt =
                Common.GetDBResult(
                    @"select C.FirstName+ISNULL(' '+C.MiddleName,'')+ISNULL(' '+C.LastName,'') as Name,
SUBSTRING(convert(varchar,B.BillDate,106),4,8)as BillMonth,B.BillDate,
O.NetworkName from Bills B
inner join Customers C ON B.CustomerID=C.CustomerID
inner join Operators O ON C.OperatorID=O.OperatorID
where B.BillID=" +
                    billID);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string message =
                    @"Dear <b>{0}</b>,<br/>You cable bill is generated on <b>{1}</b> for month <b>{2}</b>. 
Please pay ontime to avoid delay charges.<br/><br/>Regards,<br/><b>{3}</b>";
                message = string.Format(message, Common.ToString(dr["Name"]),
                               Common.ToDate(dr["BillDate"]).ToString("dd-MMM-yyyy"),
                               Common.ToString(dr["BillMonth"]), Common.ToString(dr["NetworkName"]));
                return message;
            }
            return String.Empty;
        }
        private static DataTable GetBillData(int billID)
        {
            DataTable dt =
                Common.GetDBResult(
                    @"select C.FirstName+ISNULL(' '+C.MiddleName,'')+ISNULL(' '+C.LastName,'') as Name,
SUBSTRING(convert(varchar,B.BillDate,106),4,8)as BillMonth,B.BillDate,B.NetBillAmount,B.CollectedAmount,C.MobileNo,
O.NetworkName,BillID from Bills B
inner join Customers C ON B.CustomerID=C.CustomerID
inner join Operators O ON C.OperatorID=O.OperatorID
where B.BillID=" +
                    billID);
            return dt;
        }

        #endregion

        #region UserSetting Methods
        private static DataTable GetUserSetting(int OperatorID, string settID)
        {
            return Common.GetDBResult("SELECT SettID,SettValue FROM Settings WHERE OperatorID=" + OperatorID + " and SettID='" + settID + "'");
        }
        public static bool InsertIntoSetting(int OperatorID, string SettID, string SettVal)
        {
            try
            {
                SqlConnection con = new SqlConnection(Common.GetConString());
                SqlCommand cmd = new SqlCommand("Insert_Into_Settings", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@SettID", SqlDbType.VarChar, 50).Value = SettID;
                cmd.Parameters.Add("@OperatorID", SqlDbType.Int, 4).Value = OperatorID;
                cmd.Parameters.Add("@SettValue", SqlDbType.VarChar, 2000).Value = SettVal;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string GetSetting(int OperatorID, string settID)
        {
            DataTable dtSett = GetUserSetting(OperatorID, settID);
            if (dtSett.Rows.Count > 0)
                return dtSett.Rows[0]["SettValue"].ToString();
            else
                return String.Empty;
        }
        #endregion

        public static void ExportDataTable(DataTable dt)
        {
            string attachment = "attachment; filename=" + DateTime.Now.ToString("dd_MM_yyyy_hh_ss") + ".csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            foreach (DataColumn dc in dt.Columns)
            {
                HttpContext.Current.Response.Write(dc.ColumnName + ",");
            }
            HttpContext.Current.Response.Write("\n");

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    HttpContext.Current.Response.Write('"' + Common.ToString(dr[dc.ColumnName]) + '"' + ",");
                }
                HttpContext.Current.Response.Write("\n");
            }
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", attachment);
            HttpContext.Current.Response.End();
        }
    }
}
