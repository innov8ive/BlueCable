using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Services;
using SmartControls;
namespace HMS
{
    public partial class UsersList : SimpleBasePage
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
            int UserID = Common.ToInt(hdnUserID.Value);
            if (UserID > 0)
            {
                //UsersBL objUsersBL = new UsersBL(Common.GetConString());
                //objUsersBL.Delete(Common.ToInt(hdnUserID.Value));
                hdnUserID.Value = "0";
                ReadyDBList();
            }
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            UsersDBList.AppKey = "Current";
            UsersDBList.Query = @"
SELECT Address1,Address2,City,EmailID,FirstName+ISNULL(' '+LastName,'') as Name,IsActive,Mobile,Password,UserID,case UserType when 1 then 'Operator' when 2 then 'Administrator' else '' end as UserType FROM Users where UserType!=3 
and ISNULL(FirstName,'')+ISNULL(LastName,'') like '%'+@Name+'%'";
            Hashtable ht=new Hashtable();
            ht["@Name"] = txtName.Text;
            UsersDBList.Parameters = ht;
        }
        private void AddDBListColumns()
        {
            UsersDBList.Columns.Add(new Column("Name", "Name", 200, "FirstName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            UsersDBList.Columns.Add(new Column("EmailID", "User Name", 100, "EmailID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            UsersDBList.Columns.Add(new Column("UserType", "User Type", 100, "UserType", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            UsersDBList.Columns.Add(new Column("Mobile", "Mobile", 100, "Mobile", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            UsersDBList.Columns.Add(new Column("IsActive", "Active", 100, "IsActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
        }
        #endregion
    }
}
