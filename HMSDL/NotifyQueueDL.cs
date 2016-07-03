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
    public class NotifyQueueDL
    {

        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public NotifyQueueDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public NotifyQueue Load(int Id)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            NotifyQueue objNotifyQueue = new NotifyQueue();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get NotifyQueue
                var resultNotifyQueue = dc.ExecuteQuery<NotifyQueue>("exec Get_NotifyQueue {0}", Id).ToList();
                if (resultNotifyQueue.Count > 0)
                {
                    objNotifyQueue = resultNotifyQueue[0];
                }
                dc.Dispose();
                return objNotifyQueue;
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
        public DataTable LoadAllNotifyQueue()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_NotifyQueue", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }
        public bool Update(NotifyQueue objNotifyQueue)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update NotifyQueue
                UpdateNotifyQueue(objNotifyQueue, trn);
                if (objNotifyQueue.Id > 0)
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
        public bool Delete(int Id)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete NotifyQueue
                DeleteNotifyQueue(Id, trn);
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

        public bool UpdateNotifyQueue(NotifyQueue objNotifyQueue, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_NotifyQueue", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@BillID", SqlDbType.Int).Value = objNotifyQueue.BillID;
                cmd.Parameters.Add("@CustomerID", SqlDbType.Int).Value = objNotifyQueue.CustomerID;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = objNotifyQueue.Id;
                cmd.Parameters["@Id"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@NotifyBy", SqlDbType.VarChar, 20).Value = objNotifyQueue.NotifyBy;
                cmd.Parameters.Add("@NotifyType", SqlDbType.VarChar, 20).Value = objNotifyQueue.NotifyType;

                cmd.ExecuteNonQuery();

                //after updating the NotifyQueue, update Id
                objNotifyQueue.Id = Convert.ToInt32(cmd.Parameters["@Id"].Value);

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
        public bool DeleteNotifyQueue(int Id, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from NotifyQueue where Id=@Id", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

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
