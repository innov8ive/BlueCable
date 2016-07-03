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
    public partial class OperatorsList : SimpleBasePage
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
            int OperatorID = Common.ToInt(hdnOperatorID.Value);
            if (OperatorID > 0)
            {
                OperatorsBL objOperatorsBL = new OperatorsBL(Common.GetConString());
                objOperatorsBL.Delete(Common.ToInt(hdnOperatorID.Value));
                hdnOperatorID.Value = "0";
                ReadyDBList();
            }
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            OperatorsDBList.AppKey = "Current";
            OperatorsDBList.Query = @"

SELECT ShortName,Address,Contact,EmailID,InterainmentTaxNo,RegDate
,case IsActive when 1 then 'Active' else 'Deactive' end as IsActive,NetworkName
,Operators.OperatorID,OperatorName,PANCardNo,PostalLicenceNo,ServiceTaxNo,TANNo
,ISNULL(VW.TotalActive,0) as TotalActive,ISNULL(VW.TotalDeActive,0) as TotalDeActive FROM Operators
left join VW_Operator VW ON Operators.OperatorID=VW.OperatorID
where (OperatorName like '%'+@Name+'%'
OR OperatorName like '%'+@Name+'%' OR Address like '%'+@Name+'%' OR EmailID like '%'+@Name+'%'
OR InterainmentTaxNo like '%'+@Name+'%' OR Contact like '%'+@Name+'%')";
            Hashtable ht = new Hashtable();
            ht["@Name"] = txtOpName.Text;
            OperatorsDBList.Parameters = ht;
        }
        private void AddDBListColumns()
        {
            OperatorsDBList.Columns.Add(new Column("RSRNO", "Sr. No.", 50, "RSRNO", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("OperatorName", "Operator Name", 100, "OperatorName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("NetworkName", "Network Name", 100, "NetworkName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("ShortName", "Short Name", 70, "ShortName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("Address", "Address", 200, "Address", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("Contact", "Contact", 150, "Contact", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("EmailID", "EmailID", 150, "EmailID", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            //OperatorsDBList.Columns.Add(new Column("PANCardNo", "PAN Card No", 100, "PANCardNo", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("RegDate", "Registration Date", 100, "RegDate", HorizontalAlign.Left, HorizontalAlign.Center, "dd-MMM-yyyy", true));
            OperatorsDBList.Columns.Add(new Column("IsActive", "Active", 70, "IsActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("TotalActive", "Active Customers", 100, "VW.TotalActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            OperatorsDBList.Columns.Add(new Column("TotalDeActive", "DeActive Customers", 120, "VW.TotalDeActive", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
        }
        #endregion  
    }
}

