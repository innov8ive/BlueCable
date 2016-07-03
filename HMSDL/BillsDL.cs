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
    public class BillsDL
    {
        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public BillsDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public Bills Load(int BillID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            Bills objBills = new Bills();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get Bills
                var resultBills = dc.ExecuteQuery<Bills>("exec Get_Bills {0}", BillID).ToList();
                if (resultBills.Count > 0)
                {
                    objBills = resultBills[0];
                }
                dc.Dispose();
                return objBills;
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
        public DataTable LoadAllBills()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_Bills", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }
        public bool Update(Bills objBills)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update Bills
                UpdateBills(objBills, trn);
                if (objBills.BillID > 0)
                {

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
        public bool Delete(int BillID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete Bills
                DeleteBills(BillID, trn);
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

        public bool UpdateBills(Bills objBills, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_Bills", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@AddOnPrice", SqlDbType.Decimal).Value = objBills.AddOnPrice;
                cmd.Parameters.Add("@BankName", SqlDbType.VarChar, 50).Value = objBills.BankName;
                cmd.Parameters.Add("@BasicPrice", SqlDbType.Decimal).Value = objBills.BasicPrice;
                cmd.Parameters.Add("@BillDate", SqlDbType.DateTime).Value = objBills.BillDate;
                cmd.Parameters.Add("@BillID", SqlDbType.Int).Value = objBills.BillID;
                cmd.Parameters["@BillID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@BranchName", SqlDbType.VarChar, 50).Value = objBills.BranchName;
                cmd.Parameters.Add("@ChequeDate", SqlDbType.DateTime).Value = objBills.ChequeDate;
                cmd.Parameters.Add("@ChequeNo", SqlDbType.VarChar, 8).Value = objBills.ChequeNo;
                cmd.Parameters.Add("@CollectedAmount", SqlDbType.Decimal).Value = objBills.CollectedAmount;
                cmd.Parameters.Add("@CollectedBy", SqlDbType.Int).Value = objBills.CollectedBy;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = objBills.CustomerID;
                cmd.Parameters.Add("@Discount", SqlDbType.Decimal).Value = objBills.Discount;
                cmd.Parameters.Add("@EntTax", SqlDbType.Decimal).Value = objBills.EntTax;
                cmd.Parameters.Add("@GeneratedBy", SqlDbType.Int).Value = objBills.GeneratedBy;
                cmd.Parameters.Add("@GeneratedDate", SqlDbType.DateTime).Value = objBills.GeneratedDate;
                cmd.Parameters.Add("@NetBillAmount", SqlDbType.Decimal).Value = objBills.NetBillAmount;
                cmd.Parameters.Add("@Outstanding", SqlDbType.Decimal).Value = objBills.Outstanding;
                cmd.Parameters.Add("@PaymentDate", SqlDbType.DateTime).Value = objBills.PaymentDate;
                cmd.Parameters.Add("@PaymentMode", SqlDbType.VarChar, 15).Value = objBills.PaymentMode;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar, 250).Value = objBills.Remarks;
                cmd.Parameters.Add("@ServiceTaxPerc", SqlDbType.Decimal).Value = objBills.ServiceTaxPerc;

                cmd.ExecuteNonQuery();

                //after updating the Bills, update BillID
                objBills.BillID = Convert.ToInt32(cmd.Parameters["@BillID"].Value);

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
        public bool DeleteBills(int BillID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from Bills where BillID=@BillID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@BillID", SqlDbType.Int).Value = BillID;

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
