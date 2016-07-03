using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using HMSBL;
using SmartControls;
namespace HMS
{
    public partial class MailSettingMainPage : SimpleBasePage
    {
        #region Private Members
        //AppSettings objSetting;
        MailSettingBL _MailSettingBLObj;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            Common.HideButton(btnSave, EditAllowed);
            this.Title = "MailSetting";
            if (!IsPostBack)
            {

                int EntryID = Common.ToInt(Request.QueryString["EntryID"]);
                _MailSettingBLObj = new MailSettingBL(Common.GetConString());
                _MailSettingBLObj.Load(EntryID);
                InitPageWithObject();
                PageSession["MailSettingBLObj"] = _MailSettingBLObj;
            }
            else
            {
                _MailSettingBLObj = (MailSettingBL)PageSession["MailSettingBLObj"];
            }
            Page.Header.DataBind();
            base.OnLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageSession["MailSettingBLObj"] = _MailSettingBLObj;
        }
        #endregion

        #region Control Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                lbMessage.Text = "Data updated successfully";
            }
            else
            {
                lbMessage.Text = "<span style='color:red;'>Data not saved!</span>";
            }
            uplMain.Update();
        }
        protected void btnSend_Click(object sender,EventArgs e)
        {
            if(txtTestEmail.Text.Length<=0)
            {
                Common.ShowMessage("Please enter test emailid.");
                return;
            }
            lbTestMail.Text = Common.SendMail(txtTestEmail.Text, "This is a test email", "This is a test email",
                                              String.Empty, null);
            txtTestEmail.Text = String.Empty;
        }
        #endregion

        #region Page Methods


        private void InitPageWithObject()
        {
            emailidTextBox.Text = Common.ToString(_MailSettingBLObj.Data.EmailID);
            enablesslCheckBox.Checked = Common.ToBool(_MailSettingBLObj.Data.EnableSSL);
            mailserverTextBox.Text = Common.ToString(_MailSettingBLObj.Data.MailServer);
            smsagenturlTextBox.Text = Common.ToString(_MailSettingBLObj.Data.SMSAgentURL);
            smsusernameTextBox.Text = Common.ToString(_MailSettingBLObj.Data.SMSUserName);
            displayNameTextBox.Text = Common.ToString(_MailSettingBLObj.Data.DisplayName);
            portTextBox.Text = Common.ToInt(_MailSettingBLObj.Data.Port).ToString();
            if (Common.ToString(_MailSettingBLObj.Data.Password).Length > 0)
            {
                string pass=Common.DeSerialize(Common.ToString(_MailSettingBLObj.Data.Password)).ToString();
                passwordTextBox.Text = pass;
                passwordTextBox.Attributes["value"] = pass;
                ViewState["pass"] = pass;
            }
            if (Common.ToString(_MailSettingBLObj.Data.SMSPassword).Length > 0)
            {
                string pass = Common.DeSerialize(Common.ToString(_MailSettingBLObj.Data.SMSPassword)).ToString();
                smspasswordTextBox.Text = pass;
                smspasswordTextBox.Attributes["value"] = pass;
                ViewState["smspass"] = pass;
            }
        }

        private bool SaveData()
        {
            _MailSettingBLObj.Data.EmailID = Common.ToString(emailidTextBox.Text);
            _MailSettingBLObj.Data.EnableSSL = Common.ToBool(enablesslCheckBox.Checked);
            _MailSettingBLObj.Data.MailServer = Common.ToString(mailserverTextBox.Text);
            _MailSettingBLObj.Data.Password = Common.Serialize(Common.ToString(passwordTextBox.Text));
            _MailSettingBLObj.Data.SMSAgentURL = Common.ToString(smsagenturlTextBox.Text);
            _MailSettingBLObj.Data.SMSUserName = Common.ToString(smsusernameTextBox.Text);
            _MailSettingBLObj.Data.SMSPassword =  Common.Serialize(Common.ToString(smspasswordTextBox.Text));
            _MailSettingBLObj.Data.DisplayName = Common.ToString(displayNameTextBox.Text);
            _MailSettingBLObj.Data.Port = Common.ToInt(portTextBox.Text);
            if (_MailSettingBLObj.Update())
            {
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}

