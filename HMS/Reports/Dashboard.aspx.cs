using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

namespace HMS.Reports
{
    public partial class Dashboard : SimpleBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindCB(ddlCB);
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            hdnOperatorID.Value = objSetting.OperatorID.ToString();
            Page.ClientScript.RegisterClientScriptInclude("commonJS", ResolveUrl("~/js/jquery-1.7.2.min.js"));
        }
        private void BindCB(DropDownList ddl)
        {
            DataTable dt = Common.GetDBResult("Select UserID,FirstName+ISNULL(' '+LastName,'') as Name from Users where UserType=3");
            ddl.DataSource = dt;
            ddl.DataTextField = "Name";
            ddl.DataValueField = "UserID";
            ddl.DataBind();

            ddl.Items.Insert(0, new ListItem("(All)", "0"));
        }
        private static string GetCB(string cb)
        {
            if (Common.ToInt(cb) > 0)
                return " and exists(select 1 from CBArea where UserID=" + cb + " and (CBArea.Area=C.Area OR C.Area=''))";
            else
                return "";
            //if (cb.Length > 0)
            //    return " and ';'+C.CollectionBoy+';' like '%;" + cb.Replace("'", "''") + ";%'";
            //else
            //    return "";
        }
        [WebMethod]
        public static double GetActiveCustomers(int operatorID, string cb)
        {
            return
                Common.ToDouble(
                    Common.GetDBScalarValue("Select COUNT(*) from VW_Customers C where C.IsActive=1 and C.OperatorID=" +
                                            operatorID + GetCB(cb)));
        }

        [WebMethod]
        public static double GetDeActiveCustomers(int operatorID, string cb)
        {
            return
                Common.ToDouble(
                    Common.GetDBScalarValue("Select COUNT(*) from VW_Customers C where C.IsActive=0 and C.OperatorID=" +
                                            operatorID + GetCB(cb)));
        }

        [WebMethod]
        public static double GetTotalPoints(int operatorID, string cb)
        {
            return
                Common.ToDouble(
                    Common.GetDBScalarValue("Select COUNT(*) from VW_Customers C where C.OperatorID=" +
                                            operatorID + GetCB(cb)));
        }

        [WebMethod]
        public static double GetTotalTurnOver(int operatorID, string cb)
        {
            return
                Common.ToDouble(
                    Common.GetDBScalarValue(@"select ISNULL(SUM(NetBillAmount),0)-ISNULL(SUM(Bills.Outstanding),0) from Bills 
inner join VW_Customers C ON Bills.CustomerID=C.CustomerID
where C.OperatorID=" + operatorID + GetCB(cb)));
        }

        [WebMethod]
        public static double GetCurrentBilling(int operatorID, string cb)
        {
            string query =
                @"select ISNULL(SUM(NetBillAmount),0) from Bills 
inner join VW_Customers C ON Bills.CustomerID=C.CustomerID
where C.OperatorID={0} and MONTH(Bills.BillDate)={1} and YEAR(Bills.BillDate)={2}" + GetCB(cb);
            query = String.Format(query, operatorID, Common.GetCurDate().Month, Common.GetCurDate().Year);
            return Common.ToDouble(Common.GetDBScalarValue(query));
        }

        [WebMethod]
        public static double GetTotalEntTax(int operatorID, string cb)
        {
            return
                Common.ToDouble(
                    Common.GetDBScalarValue(@"select SUM(EntTax) from Bills 
inner join VW_Customers C ON Bills.CustomerID=C.CustomerID
where C.OperatorID=" + operatorID + GetCB(cb)));
        }

        [WebMethod]
        public static double GetTotalCollection(int operatorID, string cb)
        {
            return
                Common.ToDouble(
                    Common.GetDBScalarValue(@"select ISNULL(SUM(CollectedAmount),0) from Bills 
inner join VW_Customers C ON Bills.CustomerID=C.CustomerID
where C.OperatorID=" + operatorID + GetCB(cb)));
        }


        [WebMethod]
        public static double GetCurrentCollection(int operatorID, string cb)
        {
            string query =
                @"select ISNULL(SUM(CollectedAmount),0) from Bills 
inner join VW_Customers C ON Bills.CustomerID=C.CustomerID
where C.OperatorID={0} and MONTH(Bills.BillDate)={1} and YEAR(Bills.BillDate)={2}" + GetCB(cb);
            query = String.Format(query, operatorID, Common.GetCurDate().Month, Common.GetCurDate().Year);
            return Common.ToDouble(Common.GetDBScalarValue(query));
        }

        [WebMethod]
        public static double GetTotalServiceTax(int operatorID, string cb)
        {
            return
                Common.ToDouble(
                    Common.GetDBScalarValue(@"select SUM(((BasicPrice+AddOnPrice)*ServiceTaxPerc)/100) from Bills 
inner join VW_Customers C ON Bills.CustomerID=C.CustomerID
where C.OperatorID=" + operatorID + GetCB(cb)));
        }

        [WebMethod]
        public static double GetTotalOutstanding(int operatorID, string cb)
        {
            //            return
            //                Common.ToDouble(
            //                    Common.GetDBScalarValue(@"select ISNULL(SUM(NetBillAmount),0)-ISNULL(SUM(CollectedAmount),0)
            //-ISNULL(SUM(Bills.Outstanding),0)+ISNULL(SUM(StartOutstanding),0) from Bills 
            //inner join Customers ON Bills.CustomerID=Customers.CustomerID
            //where Customers.OperatorID=" + operatorID));
            return
               Common.ToDouble(
                   Common.GetDBScalarValue(@"select ISNULL(SUM(C.Outstanding),0) from VW_Customers C 
where C.OperatorID=" + operatorID + GetCB(cb)));
        }

        [WebMethod]
        public static double GetCurrentOutstanding(int operatorID, string cb)
        {
            string query =
                @"select ISNULL(SUM(NetBillAmount),0)-ISNULL(SUM(CollectedAmount),0) from Bills 
inner join VW_Customers C ON Bills.CustomerID=C.CustomerID
where C.OperatorID={0} and MONTH(Bills.BillDate)={1} and YEAR(Bills.BillDate)={2}" + GetCB(cb);
            query = String.Format(query, operatorID, Common.GetCurDate().Month, Common.GetCurDate().Year);
            return Common.ToDouble(Common.GetDBScalarValue(query));
        }
    }
}