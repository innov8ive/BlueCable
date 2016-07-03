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
    public partial class TODOMainPage : SimpleBasePage
    {

        #region Private Members
        TODOBL _TODOBLObj;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PageID = "30";
            //objSetting = (AppSettings)Session["AppSettings"];
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Title = "TODO";
            Page.ClientScript.RegisterClientScriptInclude("commonJS", ResolveUrl("¾‖/js/Common.js"));
            if (!IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    int ID = Common.ToInt(Request.QueryString["ID"]);
                    _TODOBLObj = new TODOBL(Common.GetConString());
                    _TODOBLObj.Load(ID);
                    if (_TODOBLObj.IsNew)
                        lbMode.Text = "Mode: New";
                    else
                    {
                        lbMode.Text = "Mode: Update";
                    }
                    InitPageWithObject();
                }
                PageSession["TODOBLObj"] = _TODOBLObj;
            }
            else
            {
                _TODOBLObj = (TODOBL)PageSession["TODOBLObj"];
            }
            Page.Header.DataBind();
            base.OnLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageSession["TODOBLObj"] = _TODOBLObj;
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
                if (SaveData())
                {
                    lbMode.Text = "Mode: Update";
                    uplMain.Update();
                }
        }
        #endregion

        #region Page Methods


        private void InitPageWithObject()
        {
            detailsTextBox.Text = Common.ToString(_TODOBLObj.Data.Details);
            duedateDatePicker.Text = _TODOBLObj.Data.DueDate == null ? String.Empty : Common.ToDate(_TODOBLObj.Data.DueDate).ToString("dd-MMM-yyyy");
            titleTextBox.Text = Common.ToString(_TODOBLObj.Data.Title);

            if (_TODOBLObj.Data.CompletedDate != null)
                duedateDatePicker.Enabled = false;
        }

        private bool SaveData()
        {
            _TODOBLObj.Data.Details = Common.ToString(detailsTextBox.Text);
            _TODOBLObj.Data.DueDate = duedateDatePicker.Text.Trim() == String.Empty ? (DateTime?)null : Common.ToDate(duedateDatePicker.Text);
            _TODOBLObj.Data.Title = Common.ToString(titleTextBox.Text);
            _TODOBLObj.Data.OperatorID = UserSetting.OperatorID;
            if (_TODOBLObj.Update())
            {
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}

