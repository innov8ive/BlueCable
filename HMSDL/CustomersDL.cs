using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data;
using System.Data.Linq.Mapping;
using HMSOM;

namespace HMSDL
{
    public class CustomersDL
    {
        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public CustomersDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public Customers Load(int CustomerID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            Customers objCustomers = new Customers();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get Customers
                var resultCustomers = dc.ExecuteQuery<Customers>("exec Get_Customers {0}", CustomerID).ToList();
                if (resultCustomers.Count > 0)
                {
                    objCustomers = resultCustomers[0];
                }
                //Get CustomerPackages
                var resultCustomerPackages = dc.ExecuteQuery<CustomerPackages>("exec Get_CustomerPackages {0}", CustomerID).ToList();
                objCustomers.CustomerPackagesList = resultCustomerPackages;
                dc.Dispose();
                return objCustomers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                {
                    SqlCon.Close();
                }
                SqlCon.Dispose();
            }
        }
        public DataTable LoadAllCustomers()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_Customers", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }
        public bool Update(Customers objCustomers)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update Customers
                UpdateCustomers(objCustomers, trn);
                if (objCustomers.CustomerID > 0)
                {
                    int counter = 1;
                    //Update CustomerPackages
                    DeleteCustomerPackages(Convert.ToInt32(objCustomers.CustomerID), trn);
                    foreach (CustomerPackages objCustomerPackages in objCustomers.CustomerPackagesList)
                    {
                        objCustomerPackages.CustomerID = objCustomers.CustomerID;
                        objCustomerPackages.SrNo = counter;
                        InsertIntoCustomerPackages(objCustomerPackages, trn);
                        counter++;
                    }

                    trn.Commit();
                }
                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                con.Dispose();
            }
        }
        public bool Delete(int CustomerID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete CustomerPackages
                DeleteCustomerPackages(CustomerID, trn);
                //Delete Customers
                DeleteCustomers(CustomerID, trn);
                //Delete Bills
                DeleteBills(CustomerID, trn);
                trn.Commit();
                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                con.Dispose();
            }
        }
        public bool DeleteCustomerPackages(int CustomerID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from CustomerPackages where CustomerID=@CustomerID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }
        public bool DeleteBills(int CustomerID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from Bills where CustomerID=@CustomerID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }
        public bool InsertIntoCustomerPackages(CustomerPackages objCustomerPackages, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Into_CustomerPackages", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;
                cmd.Parameters.Add("@CANNo", SqlDbType.VarChar, 25).Value = objCustomerPackages.CANNo;
                cmd.Parameters.Add("@ConnectionType", SqlDbType.VarChar, 10).Value = objCustomerPackages.ConnectionType;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = objCustomerPackages.CustomerID;
                cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = objCustomerPackages.Discount;
                cmd.Parameters.Add("@PackageID", SqlDbType.Int).Value = objCustomerPackages.PackageID;
                cmd.Parameters.Add("@ServiceProviderID", SqlDbType.Int).Value = objCustomerPackages.ServiceProviderID;
                cmd.Parameters.Add("@SmartCardNo", SqlDbType.VarChar, 25).Value = objCustomerPackages.SmartCardNo;
                cmd.Parameters.Add("@STBMakeID", SqlDbType.Int).Value = objCustomerPackages.STBMakeID;
                cmd.Parameters.Add("@STBNo", SqlDbType.VarChar, 50).Value = objCustomerPackages.STBNo;
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Decimal).Value = objCustomerPackages.TotalPrice;
                cmd.Parameters.Add("@SrNo", SqlDbType.Int).Value = objCustomerPackages.SrNo;

                cmd.ExecuteNonQuery();

                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public bool UpdateCustomers(Customers objCustomers, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_Customers", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 500).Value = objCustomers.Address1;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 100).Value = objCustomers.Address2;
                cmd.Parameters.Add("@Address3", SqlDbType.VarChar, 100).Value = objCustomers.Address3;
                cmd.Parameters.Add("@Area", SqlDbType.VarChar, 100).Value = objCustomers.Area;
                cmd.Parameters.Add("@City", SqlDbType.VarChar, 100).Value = objCustomers.City;
                cmd.Parameters.Add("@Country", SqlDbType.VarChar, 100).Value = objCustomers.Country;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = objCustomers.CustomerID;
                cmd.Parameters["@CustomerID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@UniqueID", SqlDbType.VarChar, 50).Value = objCustomers.UniqueID;
                cmd.Parameters["@UniqueID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = objCustomers.EmailID;
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 100).Value = objCustomers.FirstName;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = objCustomers.IsActive;
                cmd.Parameters.Add("@LandlineNo", SqlDbType.VarChar, 25).Value = objCustomers.LandlineNo;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 100).Value = objCustomers.LastName;
                cmd.Parameters.Add("@MiddleName", SqlDbType.VarChar, 100).Value = objCustomers.MiddleName;
                cmd.Parameters.Add("@MobileNo", SqlDbType.VarChar, 12).Value = objCustomers.MobileNo;
                cmd.Parameters.Add("@Outstanding", SqlDbType.Decimal).Value = objCustomers.Outstanding;
                cmd.Parameters.Add("@PinCode", SqlDbType.VarChar, 100).Value = objCustomers.PinCode;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = objCustomers.Remarks;
                cmd.Parameters.Add("@State", SqlDbType.VarChar, 100).Value = objCustomers.State;
                cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = objCustomers.OperatorID;
                cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = objCustomers.Discount;
                cmd.Parameters.Add("@TotalPayable", SqlDbType.Decimal).Value = objCustomers.TotalPayable;
                cmd.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = objCustomers.CreatedDate;
                cmd.Parameters.Add("@SMSEnabled", SqlDbType.Bit).Value = objCustomers.SMSEnabled;
                cmd.Parameters.Add("@EmailEnabled", SqlDbType.Bit).Value = objCustomers.EmailEnabled;

                cmd.ExecuteNonQuery();

                //after updating the Customers, update CustomerID
                objCustomers.CustomerID = Convert.ToInt32(cmd.Parameters["@CustomerID"].Value);
                objCustomers.UniqueID = Convert.ToString(cmd.Parameters["@UniqueID"].Value);
                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
            finally
            {
                cmd.Dispose();
            }
        }
        public bool DeleteCustomers(int CustomerID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(@"Delete from CustomerPackages where CustomerID=@CustomerID;
Delete from Bills where CustomerID=@CustomerID;
Delete from Customers where CustomerID=@CustomerID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = CustomerID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }
        #endregion
    }
}
