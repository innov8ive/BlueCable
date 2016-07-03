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
using System.Text;
namespace HMS
{
    public partial class TODOList : SimpleBasePage
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
            int CustomerID = Common.ToInt(hdnID.Value);
            if (CustomerID > 0)
            {
                TODOBL objCustomersBL = new TODOBL(Common.GetConString());
                objCustomersBL.Delete(Common.ToInt(hdnID.Value));
                hdnID.Value = "0";
                ReadyDBList();
            }
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            TODODBList.AppKey = "Current";
            StringBuilder query = new StringBuilder();
            query.Append(@"select ID,Title,Details,DueDate,CompletedDate from TODO where 
OperatorID=@OperatorID and 
(@Name ='' OR Title like '%'+@Name+'%' OR Details like '%'+@Name+'%')");
            if (ddlType.SelectedValue == "0")
            {
                query.Append(" and CompletedDate is null");
            }
            else if (ddlType.SelectedValue == "1")
            {
                query.Append(" and CompletedDate is not null");
            }
            TODODBList.Query = query.ToString();
            Hashtable ht = new Hashtable();
            ht["@Name"] = txtName.Text;
            ht["@OperatorID"] = objSetting.OperatorID;
            TODODBList.Parameters = ht;
        }
        private void AddDBListColumns()
        {
            TODODBList.Columns.Add(new Column("Title", "Title", 150, "Title", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            TODODBList.Columns.Add(new Column("Details", "Details", 250, "Details", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            TODODBList.Columns.Add(new Column("DueDate", "Due Date", 120, "DueDate", HorizontalAlign.Left, HorizontalAlign.Center, "dd-MMM-yyyy", true));
            TODODBList.Columns.Add(new Column("CompletedDate", "Completed Date", 120, "CompletedDate", HorizontalAlign.Left, HorizontalAlign.Center, "dd-MMM-yyyy", true));
        }
        #endregion
    }
}
