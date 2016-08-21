using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using HMSBL;
using HMSOM;
using System.Drawing;
using SmartControls;
using System.Data;
namespace HMS
{
    public partial class CBMainPage : SimpleBasePage
    {

        #region Private Members
        UserBL _userObj;
        private int UserID
        {
            get
            {
                return Common.ToInt(Request.QueryString["UserID"]);
            }
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.Header.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.HideButton(btnSave, (SaveAllowed || EditAllowed));
            if (!IsPostBack)
            {
                BindArea();
                string s = this.PageKey;
                _userObj = new UserBL(Common.GetConString());
                _userObj.Load(UserID);
                if (_userObj.IsNew)
                    lbMode.Text = "Mode: New";
                else
                {
                    lbMode.Text = "Mode: Update";
                }
                BindPageControls(_userObj.Data);
                PageSession["UserObj"] = _userObj;
            }
            else
            {
                _userObj = (UserBL)PageSession["UserObj"];
            }

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SaveInSession();
            PageSession["UserObj"] = _userObj;
        }
        #endregion

        #region Control Events
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ((HiddenField)Page.Form.FindControl(this.PageID + "hdn")).Value = "true";
            ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "c", "CloseWindow('" + this.PageID + "hdn');", true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (PageSession["ChildSave"] != null && PageSession["ChildSave"].ToString() == "true")
                ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "savechild", "SaveChild();", true);
            else
                if (Save())
                {
                    lbMode.Text = "Mode: Update";
                    lbMessage.Text = "Data Saved Sucessfully";
                }
                else
                {
                    lbMessage.Text = "<span style='color:red;'>Data not saved!</span>";
                }

            uplMain.Update();
            uplButton.Update();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                lbMode.Text = "Mode: Update";
                lbMessage.Text = "Data Saved Sucessfully";
                uplMain.Update();
                uplButton.Update();
            }
        }
        protected void btnCheckAvailability_Click(object sender, EventArgs e)
        {
            CheckUserAvailability();
        }
        protected void btnChangeClose_Click(object sender, EventArgs e)
        {
            _userObj.Data.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassTextBox.Text, FormsAuthPasswordFormat.MD5.ToString());
        }
        #endregion

        #region Page Methods
        private bool Save()
        {
            SaveInSession();
            //_userObj.Data.Password = Admin.EncryptText(_userObj.Data.Password);
            if (ValidateSave())
            {
                _userObj.Data.Password = _userObj.Data.Password;
                _userObj.Data.UserPermissionList = new List<UserPermission>();
                _userObj.Data.UserPermissionList.Add(new UserPermission()
                                                         {
                                                             FormID = 11,
                                                             Permissions = "View",
                                                             ExtPermissions = String.Empty
                                                         });
                _userObj.Data.UserPermissionList.Add(new UserPermission()
                {
                    FormID = 7,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                _userObj.Data.UserPermissionList.Add(new UserPermission()
                {
                    FormID = 33,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                _userObj.Data.UserPermissionList.Add(new UserPermission()
                {
                    FormID = 34,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                bool updated = _userObj.Update();
                btnCheckAvailability.Visible = _userObj.IsNew;
                passwordTextBox.Visible = _userObj.IsNew;
                resetLink.Visible = !_userObj.IsNew;
                emailIDTextBox.Enabled = _userObj.IsNew;
                return updated;
            }
            return false;
        }

        private bool ValidateSave()
        {
            if (emailIDTextBox.Text.Trim().Contains(" "))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "m", "<script>alert('LoginID can not be blank space');</script>", false);
                return false;
            }
            if (emailIDTextBox.Text.Trim() == String.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "m", "<script>alert('LoginID can not be blank');</script>", false);
                return false;
            }
            else if (_userObj.IsNew == true && !_userObj.CheckUserAvailability(emailIDTextBox.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "m", "<script>alert('LoginID already exists');</script>", false);
                return false;
            }
            return true;
        }
        private void SaveInSession()
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            _userObj.Data.EmailID = emailIDTextBox.Text;
            _userObj.Data.FirstName = txtFirstName.Text;
            _userObj.Data.LastName = txtLastName.Text;
            _userObj.Data.Mobile = txtMobile.Text;
            _userObj.Data.IsActive = isactiveCheckBox.Checked;
            _userObj.Data.OperatorID = objSetting.OperatorID;
            if (_userObj.Data.Password == null)
            {
                //set default password
                _userObj.Data.Password = FormsAuthentication.HashPasswordForStoringInConfigFile("HMS", FormsAuthPasswordFormat.MD5.ToString());
            }
            _userObj.Data.UserType = 3;
            _userObj.Data.CBAreaList = new List<CBArea>();
            foreach (ListItem itm in chkAreaList.Items)
            {
                if (itm.Selected)
                    _userObj.Data.CBAreaList.Add(new CBArea() { Area = itm.Value });
            }
        }
        private void BindPageControls(Users objUser)
        {
            emailIDTextBox.Text = objUser.EmailID;
            btnCheckAvailability.Visible = _userObj.IsNew;
            passwordTextBox.Visible = _userObj.IsNew;
            resetLink.Visible = !_userObj.IsNew;
            emailIDTextBox.Enabled = _userObj.IsNew;
            isactiveCheckBox.Checked = Common.ToBool(_userObj.Data.IsActive, true).Value;
            txtFirstName.Text = _userObj.Data.FirstName;
            txtLastName.Text = _userObj.Data.LastName;
            txtMobile.Text = _userObj.Data.Mobile;

            foreach (ListItem itm in chkAreaList.Items)
            {
                if (_userObj.Data.CBAreaList.Find(Obj => Common.ToString(Obj.Area).ToUpper() == itm.Value.ToUpper()) != null)
                    itm.Selected = true;
            }
        }
        /// <summary>
        /// For Checking User Avaialabilty
        /// </summary>
        private void CheckUserAvailability()
        {
            if (_userObj.CheckUserAvailability(emailIDTextBox.Text))
            {
                messageLabel.Text = "Login ID available";
                messageLabel.ForeColor = Color.Green;
            }
            else
            {
                messageLabel.Text = "Login ID not available! Please choose another Login ID";
                messageLabel.ForeColor = Color.Red;
            }
        }
        private void BindArea()
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            DataTable dt = Common.GetDBResult("select distinct Area from Customers where ISNULL(area,'')!='' and OperatorID=" + objSetting .OperatorID+ " Order By Area");
            chkAreaList.DataSource = dt;
            chkAreaList.DataTextField = "Area";
            chkAreaList.DataValueField = "Area";
            chkAreaList.DataBind();
        }
        #endregion
    }
}