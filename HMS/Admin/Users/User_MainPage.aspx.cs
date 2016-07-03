using System;
using System.Collections.Generic;
using System.Data;
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
namespace HMS
{
    public partial class User_MainPage : SimpleBasePage
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
            PageID = "17";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.HideButton(btnSave, (SaveAllowed || EditAllowed));
            if (!IsPostBack)
            {
                BindOperator(operatorTextBox);
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
            ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "c", "window.close();", true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                lbMode.Text = "Mode: Update";
                uplMain.Update();
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                lbMode.Text = "Mode: Update";
                lbMessage.ForeColor = Color.Green;
                lbMessage.Text = "Data Saved Successfully";
            }
            else
            {
                lbMessage.ForeColor = Color.Red;
                lbMessage.Text = "Data Not Saved Successfully";
            }
            uplMain.Update();
        }
        protected void btnCheckAvailability_Click(object sender, EventArgs e)
        {
            CheckUserAvailability();
        }
        protected void btnChangeClose_Click(object sender, EventArgs e)
        {
            _userObj.Data.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassTextBox.Text, FormsAuthPasswordFormat.MD5.ToString());
        }
        protected void operatorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Common.ToInt(operatorTextBox.SelectedValue) > 0)
            {
                DataTable dt =
                    Common.GetDBResult("select NetworkName,Contact,Address,PANCardNo from Operators where OperatorID=" +
                                       Common.ToInt(operatorTextBox.SelectedValue).ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    txtNetworkName.Text = Common.ToString(dt.Rows[0]["NetworkName"]);
                    txtAddress.Text = Common.ToString(dt.Rows[0]["Address"]);
                    txtContact.Text = Common.ToString(dt.Rows[0]["Contact"]);
                    txtPANNo.Text = Common.ToString(dt.Rows[0]["PANCardNo"]);
                }
                else
                {
                    txtNetworkName.Text =
                        txtAddress.Text =
                        txtContact.Text =
                        txtPANNo.Text = String.Empty;
                }
            }
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
                AddPermission();
                bool updated = _userObj.Update();
                btnCheckAvailability.Visible = _userObj.IsNew;
                passwordTextBox.Visible = _userObj.IsNew;
                resetLink.Visible = !_userObj.IsNew;
                emailIDTextBox.Enabled = _userObj.IsNew;
                return updated;
            }
            return false;
        }
        private void AddPermission()
        {
            List<UserPermission> permList = new List<UserPermission>();
            if (_userObj.Data.UserType == 1)
            {

                permList.Add(new UserPermission()
                {
                    FormID = 7,
                    Permissions = "Add,Update,Delete",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 8,
                    Permissions = "Generate Bill",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 9,
                    Permissions = "Add,Update,Delete",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 10,
                    Permissions = "Add,Update,Delete",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 11,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 14,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 15,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 18,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 19,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 27,
                    Permissions = "Add,Update,Delete",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 30,
                    Permissions = "Add,Update,Delete",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 31,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 32,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 33,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 34,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 35,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 36,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 37,
                    Permissions = "View",
                    ExtPermissions = String.Empty
                });
            }
            else if (_userObj.Data.UserType == 2)
            {
                permList.Add(new UserPermission()
                {
                    FormID = 6,
                    Permissions = "Add,Update,Delete",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 17,
                    Permissions = "Add,Update,Delete",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 26,
                    Permissions = "Update",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 28,
                    Permissions = "Update",
                    ExtPermissions = String.Empty
                });
                permList.Add(new UserPermission()
                {
                    FormID = 29,
                    Permissions = "Update",
                    ExtPermissions = String.Empty
                });
               
            }
            _userObj.Data.UserPermissionList = permList;
        }
        private bool ValidateSave()
        {
            if (emailIDTextBox.Text.Trim().Contains(" "))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "m", "<script>alert('LoginID can not be blank space');</script>",false);
                return false;
            }
            if (emailIDTextBox.Text.Trim() == String.Empty)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "m", "<script>alert('LoginID can not be blank');</script>",false);
                return false;
            }
            if (operatorTextBox.SelectedValue == "0" && userTypeDropDownList.SelectedValue == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "m", "<script>alert('Please select cable operator.');</script>",false);
                return false;
            }
            if (userTypeDropDownList.SelectedValue == "1" && CheckUserAvailabilityOperator(Common.ToInt(operatorTextBox.SelectedValue)) == false)
            {
                ScriptManager.RegisterStartupScript(this,this.GetType(), "m", "<script>alert('" + operatorTextBox.SelectedItem.Text + " login aleardy created, Please choose another Operator.');</script>",false);
                return false;
            }
            if (_userObj.IsNew == true && !_userObj.CheckUserAvailability(emailIDTextBox.Text))
            {
                ScriptManager.RegisterStartupScript(this,this.GetType(), "m", "<script>alert('LoginID already exists');</script>",false);
                return false;
            }
            return true;
        }
        private void SaveInSession()
        {
            _userObj.Data.EmailID = emailIDTextBox.Text;
            _userObj.Data.IsActive = isactiveCheckBox.Checked;
            if (_userObj.Data.Password == null)
            {
                //set default password
                _userObj.Data.Password = FormsAuthentication.HashPasswordForStoringInConfigFile("HMS", FormsAuthPasswordFormat.MD5.ToString());
            }
            _userObj.Data.UserType = Common.ToByte(userTypeDropDownList.SelectedValue);
           
            if (_userObj.Data.UserType == 1)
            {
                _userObj.Data.FirstName = operatorTextBox.SelectedItem.Text;
                _userObj.Data.OperatorID = Common.ToInt(operatorTextBox.SelectedValue);
            }
            else
            {
                _userObj.Data.FirstName = emailIDTextBox.Text;
                _userObj.Data.OperatorID = 0;
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
            userTypeDropDownList.SelectedValue = Common.ToInt(_userObj.Data.UserType, 1).ToString();

            Common.SetDDL(operatorTextBox, Common.ToInt(_userObj.Data.OperatorID).ToString());
            txtAddress.Text = _userObj.Data.Address;
            txtContact.Text = _userObj.Data.Contact;
            txtNetworkName.Text = _userObj.Data.NetworkName;
            txtPANNo.Text = _userObj.Data.NetworkName;
            operatorTextBox.Text = _userObj.Data.OperatorName;
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
        private bool CheckUserAvailabilityOperator(int operatorID)
        {
            int count =
                Common.ToInt(Common.GetDBScalarValue("Select COUNT(*) from Users where UserType=1 and UserID<>" +Common.ToInt( _userObj.Data.UserID)+ " and OperatorID=" + operatorID));
            if (count > 0 && operatorID > 0)
            {
                messageLabel.Text = "Operator login aleardy created, Please choose another Operator";
                messageLabel.ForeColor = Color.Red;
                return false;
            }
            return true;
        }
        private void BindOperator(DropDownList ddl)
        {
            DataTable dt = Common.GetDBResult("select operatorID,OperatorName from operators Order By OperatorName");
            ddl.DataSource = dt;
            ddl.DataTextField = "OperatorName";
            ddl.DataValueField = "operatorID";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("(Select)", "0"));
        }
        #endregion
    }
}