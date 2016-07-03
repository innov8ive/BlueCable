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
    public partial class PackagesList : SimpleBasePage
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
            int PackageID = Common.ToInt(hdnPackageID.Value);
            if (PackageID > 0)
            {
                DataTable dt = Common.GetDBResult("select * from CustomerPackages where PackageID=" + PackageID);
                if(dt.Rows.Count>0)
                {
                    Common.ShowMessage("You can not delete this Package, some transction has already made");
                    return;
                }
                PackagesBL objPackagesBL = new PackagesBL(Common.GetConString());
                objPackagesBL.Delete(Common.ToInt(hdnPackageID.Value));
                hdnPackageID.Value = "0";
                ReadyDBList();
            }
        }
        #endregion

        #region Page Methods

        private void ReadyDBList()
        {
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            PackagesDBList.AppKey = "Current";
            PackagesDBList.Query = @"
SELECT AddOnPrice,EntTax,BasicPrice,Discount,PackageID,PackageName,ServiceTaxPerc,Total FROM VW_Packages where OperatorID=" + objSetting.OperatorID;
        }
        private void AddDBListColumns()
        {
            PackagesDBList.Columns.Add(new Column("RSRNO", "Sr. No.", 50, "RSRNO", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            PackagesDBList.Columns.Add(new Column("PackageName", "Package Name", 200, "PackageName", HorizontalAlign.Left, HorizontalAlign.Center, String.Empty, true));
            PackagesDBList.Columns.Add(new Column("BasicPrice", "Basic Price", 100, "BasicPrice", HorizontalAlign.Right, HorizontalAlign.Center, String.Empty, true));
            PackagesDBList.Columns.Add(new Column("AddOnPrice", "Add On Price", 100, "AddOnPrice", HorizontalAlign.Right, HorizontalAlign.Center, String.Empty, true));
            PackagesDBList.Columns.Add(new Column("EntTax", "Ent Tax", 100, "EntTax", HorizontalAlign.Right, HorizontalAlign.Center, String.Empty, true));
            PackagesDBList.Columns.Add(new Column("ServiceTaxPerc", "Service Tax(%)", 100, "ServiceTaxPerc", HorizontalAlign.Right, HorizontalAlign.Center, String.Empty, true));
            PackagesDBList.Columns.Add(new Column("Discount", "Discount", 100, "Discount", HorizontalAlign.Right, HorizontalAlign.Center, String.Empty, true));
            PackagesDBList.Columns.Add(new Column("Total", "Total", 100, "BasicPrice", HorizontalAlign.Right, HorizontalAlign.Center, "", true));
        }
        #endregion
    }
}
