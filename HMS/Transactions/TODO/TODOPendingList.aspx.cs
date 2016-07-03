using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HMSBL;

namespace HMS.Transactions.TODO
{
    public partial class TODOPendingList : SimpleBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindPendingList();
            }
        }

        int srNo = 1;
        protected void PendingTODOListGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((Label)e.Row.Cells[0].FindControl("lbSrNo")).Text = srNo.ToString();
                srNo++;
            }
        }

        protected void PendingTODOListGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Complete")
            {
                int id = Common.ToInt(e.CommandArgument);
                TODOBL objTODOBL = new TODOBL(Common.GetConString());
                objTODOBL.MarkCompleted(id);
                BindPendingList();
            }
        }

        private void BindPendingList()
        {
            DataTable dt = Common.GetDBResult("Select * from TODO where CompletedDate IS NULL and DueDate<=getDate() and OperatorID=" + UserSetting.OperatorID);
            PendingTODOListGrid.DataSource = dt;
            PendingTODOListGrid.DataBind();
        }
    }
}