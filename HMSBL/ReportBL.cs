using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HMSOM;
using HMSDL;

namespace HMSBL
{
    public class ReportBL
    {
        #region Declaration
        private string connectionString;
        #endregion

        #region Constructor
        public ReportBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        private ReportDL CreateDL()
        {
            return new ReportDL(connectionString);
        }

        public int CollectionReport_GetCustomers(int operatorID, int serviceproviderID, int collectedBy, DateTime date1, DateTime date2)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.CollectionReport_GetCustomers(operatorID,serviceproviderID,collectedBy, date1, date2);
        }
        public double CollectionReport_GetCollection(int operatorID, int serviceproviderID, int collectedBy, DateTime date1, DateTime date2)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.CollectionReport_GetCollection(operatorID, serviceproviderID, collectedBy, date1, date2);
        }
        public int DateReport_GetCustomers(int operatorID, int serviceproviderID, int active,int colBoy)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.DateReport_GetCustomers(operatorID, serviceproviderID, active,colBoy);
        }
        public int OutstandingReport_GetCustomers(int operatorID, int serviceproviderID,double min,double max,int colBoy)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.OutstandingReport_GetCustomers(operatorID, serviceproviderID, min, max, colBoy);
        }
        public double OutstandingReport_GetCollection(int operatorID, int serviceproviderID, double min, double max, int colBoy)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.OutstandingReport_GetCollection(operatorID, serviceproviderID, min, max, colBoy);
        }
        public int CustomOutstandingReport_GetCustomers(int operatorID, int serviceproviderID, int paid, double min, double max, int userType, int userID)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.CustomOutstandingReport_GetCustomers(operatorID, serviceproviderID, paid, min, max, userType, userID);
        }
        public double CustomOutstandingReport_GetCollection(int operatorID, int serviceproviderID, int paid, double min, double max, int userType, int userID)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.CustomOutstandingReport_GetCollection(operatorID, serviceproviderID, paid, min, max, userType, userID);
        }
        public int NewCustomerReport_GetCustomers(int operatorID, int serviceproviderID, int active,int userType,int userID)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.NewCustomerReport_GetCustomers(operatorID, serviceproviderID, active, userType, userID);
        }
        public int PackageReport_GetCustomers(int operatorID, int serviceproviderID, int packageID, int userType, int userID)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.PackageReport_GetCustomers(operatorID, serviceproviderID, packageID, userType, userID);
        }
    }
}
