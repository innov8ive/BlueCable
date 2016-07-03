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
    public class TODODL
    {

        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public TODODL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public TODO Load(int ID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            TODO objTODO = new TODO();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get TODO
                var resultTODO = dc.ExecuteQuery<TODO>("exec Get_TODO {0}", ID).ToList();
                if (resultTODO.Count > 0)
                {
                    objTODO = resultTODO[0];
                }
                dc.Dispose();
                return objTODO;
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
        public DataTable LoadAllTODO()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select * from TODO where CompletedDate IS NULL and DueDate<=getDate()", con);
           
            con.Open();
            using (con)
            {
                dt.Load(cmd.ExecuteReader());
                con.Close();
            }
            return dt;
        }
        public bool Update(TODO objTODO)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update TODO
                UpdateTODO(objTODO, trn);
                if (objTODO.ID > 0)
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
        public bool Delete(int ID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete TODO
                DeleteTODO(ID, trn);
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

        public bool UpdateTODO(TODO objTODO, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_TODO", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@Completed", SqlDbType.Bit).Value = objTODO.Completed;
                cmd.Parameters.Add("@CompletedDate", SqlDbType.DateTime).Value = objTODO.CompletedDate;
                cmd.Parameters.Add("@Details", SqlDbType.VarChar, 2500).Value = objTODO.Details;
                cmd.Parameters.Add("@DueDate", SqlDbType.DateTime).Value = objTODO.DueDate;
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = objTODO.ID;
                cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = objTODO.OperatorID;
                cmd.Parameters["@ID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@Title", SqlDbType.VarChar, 250).Value = objTODO.Title;

                cmd.ExecuteNonQuery();

                //after updating the TODO, update ID
                objTODO.ID = Convert.ToInt32(cmd.Parameters["@ID"].Value);

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
        public bool DeleteTODO(int ID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from TODO where ID=@ID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }
        public void MarkCompleted(int id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Update TODO set Completed=1, CompletedDate=@Date where ID=@ID", con);
            cmd.Parameters.Add("@Date", SqlDbType.DateTime, 20).Value = DateTime.Now;
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

            try
            {
                con.Open();
                using (con)
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            finally { }
        }
        #endregion
    }
}
