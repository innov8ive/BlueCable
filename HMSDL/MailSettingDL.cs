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
    public class MailSettingDL
    {
        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public MailSettingDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public MailSetting Load(int EntryID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            MailSetting objMailSetting = new MailSetting();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get MailSetting
                var resultMailSetting = dc.ExecuteQuery<MailSetting>("exec Get_MailSetting {0}", EntryID).ToList();
                if (resultMailSetting.Count > 0)
                {
                    objMailSetting = resultMailSetting[0];
                }
                dc.Dispose();
                return objMailSetting;
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
        public DataTable LoadAllMailSetting()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_MailSetting", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }
        public bool Update(MailSetting objMailSetting)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update MailSetting
                UpdateMailSetting(objMailSetting, trn);
                if (objMailSetting.EntryID > 0)
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
        public bool Delete(int EntryID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete MailSetting
                DeleteMailSetting(EntryID, trn);
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

        public bool UpdateMailSetting(MailSetting objMailSetting, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_MailSetting", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 100).Value = objMailSetting.EmailID;
                cmd.Parameters.Add("@EnableSSL", SqlDbType.Bit).Value = objMailSetting.EnableSSL;
                cmd.Parameters.Add("@EntryID", SqlDbType.Int).Value = objMailSetting.EntryID;
                cmd.Parameters["@EntryID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@MailServer", SqlDbType.VarChar, 100).Value = objMailSetting.MailServer;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 4000).Value = objMailSetting.Password;
                cmd.Parameters.Add("@SMSAgentURL", SqlDbType.VarChar, 200).Value = objMailSetting.SMSAgentURL;
                cmd.Parameters.Add("@SMSUserName", SqlDbType.VarChar, 100).Value = objMailSetting.SMSUserName;
                cmd.Parameters.Add("@SMSPassword", SqlDbType.VarChar, 4000).Value = objMailSetting.SMSPassword;
                cmd.Parameters.Add("@DisplayName", SqlDbType.VarChar, 100).Value = objMailSetting.DisplayName;
                cmd.Parameters.Add("@Port", SqlDbType.Int).Value = objMailSetting.Port;

                cmd.ExecuteNonQuery();

                //after updating the MailSetting, update EntryID
                objMailSetting.EntryID = Convert.ToInt32(cmd.Parameters["@EntryID"].Value);

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
        public bool DeleteMailSetting(int EntryID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from MailSetting where EntryID=@EntryID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@EntryID", SqlDbType.Int).Value = EntryID;

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
