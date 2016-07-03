using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using SmartControls;
using HMSBL;
namespace HMS
{
    public partial class OperatorsMainPage : SimpleBasePage
    {

        #region Private Members
        //AppSettings objSetting;
        OperatorsBL _OperatorsBLObj;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PageID = "6";
            //objSetting = (AppSettings)Session["AppSettings"];
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Title = "Operators";
            ClientScript.RegisterClientScriptInclude("commonJS", ResolveUrl("~/js/Validation.js"));
            if (!IsPostBack)
            {
                if (Request.QueryString["OperatorID"] != null)
                {
                    int OperatorID = Common.ToInt(Request.QueryString["OperatorID"]);
                    _OperatorsBLObj = new OperatorsBL(Common.GetConString());
                    _OperatorsBLObj.Load(OperatorID);
                    if (_OperatorsBLObj.IsNew)
                        lbMode.Text = "Mode: New";
                    else
                    {
                        lbMode.Text = "Mode: Update";
                    }
                    InitPageWithObject();
                }
                PageSession["OperatorsBLObj"] = _OperatorsBLObj;
            }
            else
            {
                _OperatorsBLObj = (OperatorsBL)PageSession["OperatorsBLObj"];
            }
            Page.Header.DataBind();
            base.OnLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageSession["OperatorsBLObj"] = _OperatorsBLObj;
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
            if (!ValidateShortName())
                return;
            if (SaveData())
            {
                lbMode.Text = "Mode: Update";
                lbMessage.Text = "Changes Saved Successfully!";
                shortNameTextBox.ReadOnly = true;
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
            shortNameTextBox.Text = _OperatorsBLObj.Data.ShortName;
            if (shortNameTextBox.Text.Length > 0)
                shortNameTextBox.ReadOnly = true;
            addressTextBox.Text = Common.ToString(_OperatorsBLObj.Data.Address);
            contactTextBox.Text = Common.ToString(_OperatorsBLObj.Data.Contact);
            emailidTextBox.Text = Common.ToString(_OperatorsBLObj.Data.EmailID);
            interainmenttaxnoTextBox.Text = Common.ToString(_OperatorsBLObj.Data.InterainmentTaxNo);
            isactiveCheckBox.Checked = Common.ToBool(_OperatorsBLObj.Data.IsActive, true).Value;
            networknameTextBox.Text = Common.ToString(_OperatorsBLObj.Data.NetworkName);
            operatornameTextBox.Text = Common.ToString(_OperatorsBLObj.Data.OperatorName);
            pancardnoTextBox.Text = Common.ToString(_OperatorsBLObj.Data.PANCardNo);
            postallicencenoTextBox.Text = Common.ToString(_OperatorsBLObj.Data.PostalLicenceNo);
            servicetaxnoTextBox.Text = Common.ToString(_OperatorsBLObj.Data.ServiceTaxNo);
            txtRegDate.Text = _OperatorsBLObj.Data.RegDate == null ? String.Empty : Common.ToDate(_OperatorsBLObj.Data.RegDate).ToString("dd-MMM-yyyy");
        }

        private bool SaveData()
        {
            _OperatorsBLObj.Data.ShortName = shortNameTextBox.Text;
            _OperatorsBLObj.Data.Address = Common.ToString(addressTextBox.Text);
            _OperatorsBLObj.Data.Contact = Common.ToString(contactTextBox.Text);
            _OperatorsBLObj.Data.EmailID = Common.ToString(emailidTextBox.Text);
            _OperatorsBLObj.Data.InterainmentTaxNo = Common.ToString(interainmenttaxnoTextBox.Text);
            _OperatorsBLObj.Data.IsActive = Common.ToBool(isactiveCheckBox.Checked);
            _OperatorsBLObj.Data.NetworkName = Common.ToString(networknameTextBox.Text);
            _OperatorsBLObj.Data.OperatorName = Common.ToString(operatornameTextBox.Text);
            _OperatorsBLObj.Data.PANCardNo = Common.ToString(pancardnoTextBox.Text);
            _OperatorsBLObj.Data.PostalLicenceNo = Common.ToString(postallicencenoTextBox.Text);
            _OperatorsBLObj.Data.ServiceTaxNo = Common.ToString(servicetaxnoTextBox.Text);
            _OperatorsBLObj.Data.RegDate = Common.ToDate(txtRegDate.Text);
            if (_OperatorsBLObj.Update())
            {
                return true;
            }
            else
                return false;
        }

        private bool ValidateShortName()
        {
            int count = Common.ToInt(
                Common.GetDBScalarValue("Select COUNT(*) from Operators where OperatorID<>" +
                                        Common.ToInt(_OperatorsBLObj.Data.OperatorID)
                                        +" and ShortName='"+shortNameTextBox.Text.Replace("'","''")+"'"));
            if(count>0)
            {
                lbMessage.Text = "<span style='color:red;'>Data not saved! Short Name already exists, please choose another Short Name</span>";
                uplMain.Update();
                return false;
            }
            return true;
        }
        #endregion
    }
}

