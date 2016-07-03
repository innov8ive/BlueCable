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
    public class OperatorsDL
    {
        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public OperatorsDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public Operators Load(int OperatorID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            Operators objOperators = new Operators();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get Operators
                var resultOperators = dc.ExecuteQuery<Operators>("exec Get_Operators {0}", OperatorID).ToList();
                if (resultOperators.Count > 0)
                {
                    objOperators = resultOperators[0];
                }
                dc.Dispose();
                return objOperators;
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
        public DataTable LoadAllOperators()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_Operators", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }
        public bool Update(Operators objOperators)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update Operators
                UpdateOperators(objOperators, trn);
                if (objOperators.OperatorID > 0)
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
        public bool Delete(int OperatorID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete Operators
                DeleteOperators(OperatorID, trn);
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

        public bool UpdateOperators(Operators objOperators, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_Operators", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@Address", SqlDbType.VarChar, 250).Value = objOperators.Address;
                cmd.Parameters.Add("@Contact", SqlDbType.VarChar, 50).Value = objOperators.Contact;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = objOperators.EmailID;
                cmd.Parameters.Add("@InterainmentTaxNo", SqlDbType.VarChar, 50).Value = objOperators.InterainmentTaxNo;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = objOperators.IsActive;
                cmd.Parameters.Add("@NetworkName", SqlDbType.VarChar, 100).Value = objOperators.NetworkName;
                cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = objOperators.OperatorID;
                cmd.Parameters["@OperatorID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@OperatorName", SqlDbType.VarChar, 100).Value = objOperators.OperatorName;
                cmd.Parameters.Add("@PANCardNo", SqlDbType.VarChar, 15).Value = objOperators.PANCardNo;
                cmd.Parameters.Add("@PostalLicenceNo", SqlDbType.VarChar, 50).Value = objOperators.PostalLicenceNo;
                cmd.Parameters.Add("@ServiceTaxNo", SqlDbType.VarChar, 50).Value = objOperators.ServiceTaxNo;
                cmd.Parameters.Add("@TANNo", SqlDbType.VarChar, 25).Value = objOperators.TANNo;
                cmd.Parameters.Add("@ShortName", SqlDbType.VarChar, 5).Value = objOperators.ShortName;
                cmd.Parameters.Add("@RegDate", SqlDbType.DateTime).Value = objOperators.RegDate;

                cmd.ExecuteNonQuery();

                //after updating the Operators, update OperatorID
                objOperators.OperatorID = Convert.ToInt32(cmd.Parameters["@OperatorID"].Value);

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
        public bool DeleteOperators(int OperatorID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from Operators where OperatorID=@OperatorID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = OperatorID;

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
