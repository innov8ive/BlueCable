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
    public partial class CBList : SimpleBasePage
    {
        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
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
            int UserID = Common.ToInt(hdnUserID.Value);
            if (UserID > 0)
            {
                UserBL objOperatorsBL = new UserBL(Common.GetConString());
                objOperatorsBL.Delete(Common.ToInt(hdnUserID.Value));
                hdnUserID.Value = "0";
                ReadyDBList();
            }
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            OperatorsDBList.AppKey = "Current";
            OperatorsDBList.Query = @"select FirstName,LastName,Mobile,EmailID,UserID,
case when IsActive=1 then 'Active' else 'Deactive' end as Active from Users where UserType=3 and OperatorID=" + objSetting.OperatorID;
        }
        private void AddDBListColumns()
        {
            OperatorsDBList.Columns.Add(new Column("RSRNO", "Sr. No.", 50, "RSRNO", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("EmailID", "User ID", 100, "EmailID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("FirstName", "First Name", 100, "FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("LastName", "Last Name", 100, "LastName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("Mobile", "Mobile", 200, "Mobile", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("Active", "Active", 100, "IsActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
        }
        #endregion
    }
}

