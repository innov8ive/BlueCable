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
    public partial class SettingMainPage : SimpleBasePage
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
      
        #endregion

        #region Page Methods


        private void InitPageWithObject()
        {
            //entTaxNE.Text = Common.GetSetting(0, "EntTax");
            //serviceTaxNE.Text = Common.GetSetting(0, "ServiceTax");
            printBillUrlTextBox.Text = Common.GetSetting(0, "PrintBillUrl");
        }

        private bool SaveData()
        {
            //Common.InsertIntoSetting(0, "EntTax", entTaxNE.Text);
            //Common.InsertIntoSetting(0, "ServiceTax", serviceTaxNE.Text);
            Common.InsertIntoSetting(0, "PrintBillUrl", printBillUrlTextBox.Text);
            return true;
        }
        #endregion
    }
}

