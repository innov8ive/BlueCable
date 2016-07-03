using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HMSBL;
using SmartControls;

namespace HMS
{
    public partial class UserRole_MainPage : SimpleBasePage
    {
        #region Private Members
        RolesBL _rolesObj;
        private int RoleID
        {
            get
            {
                return Common.ToInt(Request.QueryString["RoleID"]);
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
            foreach (TabItem itm in Tab1.TabItemCollection)
            {
                itm.NavigateUrl += "?pageKey=" + this.PageKey;
            }
            if (!IsPostBack)
            {
                string s = this.PageKey;
                _rolesObj = new RolesBL(Common.GetConString());
                _rolesObj.Load(RoleID);
                if (_rolesObj.IsNew)
                    lbMode.Text = "Mode: New";
                else
                {
                    lbMode.Text = "Mode: Update";
                }
                roleNameTextBox.Text = _rolesObj.Data.RoleName;
                PageSession["RolesObj"] = _rolesObj;
            }
            else
            {
                _rolesObj = (RolesBL)PageSession["RolesObj"];
            }

        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            _rolesObj.Data.RoleName = roleNameTextBox.Text;
            PageSession["RolesObj"] = _rolesObj;
        }
        #endregion

        #region Control Events
        void userRoleToolBar_ToolBarClick(object sender, SmartControls.ToolBarEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Close":
                    //CloseThisPage(e, Tab1);
                    //isClosed = true;
                    ((HiddenField) Page.Form.FindControl(this.PageID + "hdn")).Value = "true";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "c",
                                                        "CloseWindow('" + this.PageID + "hdn');", true);
                    break;
                case "Save":
                    if (PageSession["ChildSave"].ToString() == "true")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "savechild", "SaveChild();", true);
                    else
                        Save();
                    break;
            }
        }
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
                    uplMain.Update();
                }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Save())
            {
                lbMode.Text = "Mode: Update";
                uplMain.Update();
            }
        }
        #endregion

        #region Page Methods
        private bool Save()
        {
            _rolesObj.Data.RoleName = roleNameTextBox.Text;
            return _rolesObj.Save();
        }

        #endregion
    }
}