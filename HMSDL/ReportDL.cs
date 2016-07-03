using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace HMSDL
{
    public class ReportDL
    {
        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public ReportDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        public int CollectionReport_GetCustomers(int operatorID, int serviceproviderID, int collectedBy, DateTime date1, DateTime date2)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd =
                new SqlCommand(
                    @"SELECT COUNT(C.CustomerID) FROM GetCollection(@StartDate,@EndDate) as COL
inner join VW_Customers C ON COL.CustomerID=C.CustomerID
inner join Users U ON COL.CollectedBy=U.UserID 
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)
and (@CollectedBy =-1 OR COL.CollectedBy=@CollectedBy)",
                    con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.Add("@CollectedBy", SqlDbType.Int).Value = collectedBy;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 20).Value = date1;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 20).Value = date2;
            int total = 0;
            con.Open();
            using (con)
            {
                total = ToInt(cmd.ExecuteScalar());
            }
            return total;
        }

        public double CollectionReport_GetCollection(int operatorID, int serviceproviderID, int collectedBy, DateTime date1, DateTime date2)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd =
                new SqlCommand(
                    @"SELECT SUM(COL.CollectedAmount) FROM GetCollection(@StartDate,@EndDate) as COL
inner join VW_Customers C ON COL.CustomerID=C.CustomerID
left join Users U ON COL.CollectedBy=U.UserID 
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)
and (@CollectedBy =-1 OR COL.CollectedBy=@CollectedBy)",
                    con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.Add("@CollectedBy", SqlDbType.Int).Value = collectedBy;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 20).Value = date1;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 20).Value = date2;
            double total = 0;
            con.Open();
            using (con)
            {
                total = ToDouble(cmd.ExecuteScalar());
            }
            return total;
        }

        public int DateReport_GetCustomers(int operatorID, int serviceproviderID, int active, int colBoy)
        {
            string query = @"SELECT COUNT(C.CustomerID) FROM VW_Customers C
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)
and (@Active =-1 OR (C.IsActive=1 and @Active=1) OR (C.IsActive=0 and @Active=0))";

            if (colBoy > 0)//Collection Boy
            {
                query += " and exists(select 1 from CBArea where UserID=" + colBoy + " and (CBArea.Area=C.Area OR C.Area=''))";
            }

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.Add("@Active", SqlDbType.Int).Value = active;
            int total = 0;
            con.Open();
            using (con)
            {
                total = ToInt(cmd.ExecuteScalar());
            }
            return total;
        }

        public int OutstandingReport_GetCustomers(int operatorID, int serviceproviderID, double min, double max, int colBoy)
        {
            string query = @"SELECT COUNT(C.CustomerID) FROM VW_Customers C 
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID) and C.Outstanding between @Min and @Max";

            if (colBoy > 0)//Collection Boy
            {
                query += " and exists(select 1 from CBArea where UserID=" + colBoy + " and (CBArea.Area=C.Area OR C.Area=''))";
            }
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.AddWithValue("@Min", min == 0 ? 1 : min);
            cmd.Parameters.AddWithValue("@Max", max == 0 ? 2000000 : max);
            int total = 0;
            con.Open();
            using (con)
            {
                total = ToInt(cmd.ExecuteScalar());
            }
            return total;
        }
        public double OutstandingReport_GetCollection(int operatorID, int serviceproviderID, double min, double max, int colBoy)
        {
            string query = @"SELECT SUM(C.Outstanding) FROM VW_Customers C 
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)  and C.Outstanding between @Min and @Max";
            if (colBoy > 0)//Collection Boy
            {
                query += " and exists(select 1 from CBArea where UserID=" + colBoy + " and (CBArea.Area=C.Area OR C.Area=''))";
            }
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.AddWithValue("@Min", min == 0 ? 1 : min);
            cmd.Parameters.AddWithValue("@Max", max == 0 ? 2000000 : max);
            double total = 0;
            con.Open();
            using (con)
            {
                total = ToDouble(cmd.ExecuteScalar());
            }
            return total;
        }
        public int CustomOutstandingReport_GetCustomers(int operatorID, int serviceproviderID, int paid, double min, double max, int userType, int userID)
        {
            string query = @"SELECT COUNT(C.CustomerID) FROM VW_Customers C
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID  
left join VW_LatestBill VLB ON C.CustomerID=VLB.CustomerID
left join Bills B ON VLB.BillID=B.BillID
where C.OperatorID=@OperatorID and C.IsActive=1 and (@Paid = -1 OR @Paid = 1 and B.PaymentDate IS NOT NULL OR @Paid = 0 and B.PaymentDate IS NULL)
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID) and C.Outstanding>0 and C.Outstanding between @Min AND @Max ";
            if (userID > 0)//Collection Boy
            {
                query += " and exists(select 1 from CBArea where UserID=" + userID + " and (CBArea.Area=C.Area OR C.Area=''))";
            }
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.Add("@Paid", SqlDbType.Int).Value = paid;
            cmd.Parameters.AddWithValue("@Min", min == 0 ? 1 : min);
            cmd.Parameters.AddWithValue("@Max", max == 0 ? 2000000 : max);
            int total = 0;
            con.Open();
            using (con)
            {
                total = ToInt(cmd.ExecuteScalar());
            }
            return total;
        }
        public double CustomOutstandingReport_GetCollection(int operatorID, int serviceproviderID, int paid, double min, double max, int userType, int userID)
        {
            string query = @"SELECT SUM(C.Outstanding) from VW_Customers C
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID  
left join VW_LatestBill VLB ON C.CustomerID=VLB.CustomerID
left join Bills B ON VLB.BillID=B.BillID
where C.OperatorID=@OperatorID and C.IsActive=1 and (@Paid = -1 OR @Paid = 1 and B.PaymentDate IS NOT NULL OR @Paid = 0 and B.PaymentDate IS NULL)
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID) and C.Outstanding>0 and C.Outstanding between @Min AND @Max ";
            if (userID > 0)//Collection Boy
            {
                query += " and exists(select 1 from CBArea where UserID=" + userID + " and (CBArea.Area=C.Area OR C.Area=''))";
            }

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.Add("@Paid", SqlDbType.Int).Value = paid;
            cmd.Parameters.AddWithValue("@Min", min == 0 ? 1 : min);
            cmd.Parameters.AddWithValue("@Max", max == 0 ? 2000000 : max);
            double total = 0;
            con.Open();
            using (con)
            {
                total = ToDouble(cmd.ExecuteScalar());
            }
            return total;
        }
        public int NewCustomerReport_GetCustomers(int operatorID, int serviceproviderID, int active, int userType, int userID)
        {
            string query = @"SELECT COUNT(C.CustomerID) FROM VW_Customers C
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and C.CreatedDate between @StartDate and @EndDate
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)
and (@Active =-1 OR (C.IsActive=1 and @Active=1) OR (C.IsActive=0 and @Active=0))";
            if (userID > 0)//Collection Boy
            {
                query += " and exists(select 1 from CBArea where UserID=" + userID + " and (CBArea.Area=C.Area OR C.Area=''))";
            }

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime, 20).Value = DateTime.Today.AddMonths(-1);
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime, 20).Value = DateTime.Today;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.Add("@Active", SqlDbType.Int).Value = active;
            int total = 0;
            con.Open();
            using (con)
            {
                total = ToInt(cmd.ExecuteScalar());
            }
            return total;
        }
        public int PackageReport_GetCustomers(int operatorID, int serviceproviderID, int packageID, int userType, int userID)
        {
            string query = @"SELECT COUNT(C.CustomerID) FROM VW_Customers C
left join ServiceProviders ON C.ServiceProviderID=ServiceProviders.ServiceProviderID 
where C.OperatorID=@OperatorID 
and (@ServiceProviderID =-1 OR C.ServiceProviderID=@ServiceProviderID)
and (@PackageID=0 OR exists(select 1 from CustomerPackages CP where C.CustomerID=CP.CustomerID and CP.PackageID=@PackageID))";
            if (userID > 0)//Collection Boy
            {
                query += " and exists(select 1 from CBArea where UserID=" + userID + " and (CBArea.Area=C.Area OR C.Area=''))";
            }

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = operatorID;
            cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = serviceproviderID;
            cmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = packageID;
            int total = 0;
            con.Open();
            using (con)
            {
                total = ToInt(cmd.ExecuteScalar());
            }
            return total;
        }
        public int ToInt(object theValue)
        {
            int theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !int.TryParse(theValue.ToString(), out theResult))
                theResult = 0;
            return theResult;
        }
        public double ToDouble(object theValue)
        {
            double theResult = 0;
            if (theValue == null || Convert.IsDBNull(theValue) || !double.TryParse(theValue.ToString(), out theResult))
                theResult = 0;
            return theResult;
        }
    }
}
