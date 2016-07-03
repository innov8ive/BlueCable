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
    public class AttendenceDL
    {
        #region Private Members
        private string connectionString;
        #endregion

        #region Constructor
        public AttendenceDL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        public Attendence Load(int DoctorID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            Attendence objAttendence = new Attendence();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get Attendence
                var resultAttendence = dc.ExecuteQuery<Attendence>("exec Get_Attendence {0}", DoctorID).ToList();
                if (resultAttendence.Count > 0)
                {
                    objAttendence = resultAttendence[0];
                }
                dc.Dispose();
                return objAttendence;
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
        public DataTable LoadAllAttendence()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_Attendence", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }
        public bool Update(Attendence objAttendence)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update Attendence
                UpdateAttendence(objAttendence, trn);
                if (objAttendence.DoctorID > 0)
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
        public bool Delete(int DoctorID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete Attendence
                DeleteAttendence(DoctorID, trn);
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

        public bool UpdateAttendence(Attendence objAttendence, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_Attendence", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@AttendDate", SqlDbType.DateTime).Value = objAttendence.AttendDate;
                cmd.Parameters.Add("@AttendTime", SqlDbType.DateTime).Value = objAttendence.AttendTime;
                cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = objAttendence.DoctorID;
                cmd.Parameters["@DoctorID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@ParientID", SqlDbType.Int).Value = objAttendence.ParientID;
                cmd.Parameters.Add("@PatientName", SqlDbType.VarChar, 100).Value = objAttendence.PatientName;
                cmd.Parameters.Add("@Remark", SqlDbType.VarChar, 100).Value = objAttendence.Remark;

                cmd.ExecuteNonQuery();

                //after updating the Attendence, update DoctorID
                objAttendence.DoctorID = Convert.ToInt32(cmd.Parameters["@DoctorID"].Value);

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
        public bool DeleteAttendence(int DoctorID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from Attendence where DoctorID=@DoctorID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@DoctorID", SqlDbType.Int).Value = DoctorID;

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
