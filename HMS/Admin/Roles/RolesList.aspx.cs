using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Services;
using HMSBL;
using SmartControls;
namespace HMS
{
    public partial class RolesList : SimpleBasePage
    {
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.HideButton(btnNew, SaveAllowed);
            Common.HideButton(btnEdit, EditAllowed);
            Common.HideButton(btnDelete, DeleteAllowed);

            Page.Header.DataBind();
            if (!IsPostBack)
            {
                ReadyDBList();
            }
            AddDBListColumns();
        }
        #endregion

        #region Control Events
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReadyDBList();
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int RoleID = Common.ToInt(hdnRoleID.Value);
            if (RoleID > 0)
            {
                RolesBL objRolesBL = new RolesBL(Common.GetConString());
                //objRolesBL.Delete(Common.ToInt(hdnRoleID.Value));
                hdnRoleID.Value = "0";
                ReadyDBList();
            }
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            RolesDBList.AppKey = "Current";
            RolesDBList.Query = @"
SELECT RoleID,RoleName FROM Roles";
        }
        private void AddDBListColumns()
        {
            RolesDBList.Columns.Add(new Column("RoleID", "RoleID", 100, "RoleID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            RolesDBList.Columns.Add(new Column("RoleName", "RoleName", 100, "RoleName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
        }
        #endregion
    }
}
