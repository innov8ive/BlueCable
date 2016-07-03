using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HMSOM;
using System.Data.SqlClient;
using System.Data;
using System.Data.Linq;


namespace HMSDL
{
    public class UsersDL
    {
        #region Private Members

        private string connectionString;

        #endregion

        #region Constructor

        public UsersDL(string conString)
        {
            connectionString = conString;
        }

        #endregion

        #region Main Methods

        public Users Load(int UserID)
        {
            SqlConnection SqlCon = new SqlConnection(connectionString);
            Users objUsers = new Users();
            var dc = new DataContext(SqlCon);
            try
            {
                //Get Users
                var resultUsers = dc.ExecuteQuery<Users>("exec Get_Users {0}", UserID).ToList();
                if (resultUsers.Count > 0)
                {
                    objUsers = resultUsers[0];
                }
                //Get UserPermission
                var resultUserPermission =
                    dc.ExecuteQuery<UserPermission>("exec Get_UserPermission {0}", UserID).ToList();
                objUsers.UserPermissionList = resultUserPermission;
                //Get CBArea
                var resultCBArea =
                    dc.ExecuteQuery<CBArea>("exec Get_CBArea {0}", UserID).ToList();
                objUsers.CBAreaList = resultCBArea;
                objUsers.Types = new System.Collections.Hashtable();
                objUsers.Types["UserPermissionList"] = new UserPermission();
                dc.Dispose();
                return objUsers;
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

        public DataTable LoadAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("exec Get_Users", con);

            con.Open();
            dt.Load(cmd.ExecuteReader());
            con.Close();

            return dt;
        }

        public bool Update(Users objUsers)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //update Users
                UpdateUsers(objUsers, trn);
                if (objUsers.UserID > 0)
                {
                    //Update DeleteCBArea
                    DeleteCBArea(Convert.ToInt32(objUsers.UserID), trn);
                    foreach (CBArea objCBArea in objUsers.CBAreaList)
                    {
                        objCBArea.UserID = objUsers.UserID;
                        InsertIntoCBArea(objCBArea, trn);
                    }

                    //Update UserPermission
                    DeleteUserPermission(Convert.ToInt32(objUsers.UserID), trn);
                    foreach (UserPermission objUserPermission in objUsers.UserPermissionList)
                    {
                        objUserPermission.UserID = objUsers.UserID;
                        InsertIntoUserPermission(objUserPermission, trn);
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

        public bool Delete(int UserID)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                //Delete UserPermission
                DeleteUserPermission(UserID, trn);
                //Delete Users
                DeleteUsers(UserID, trn);
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

        public bool UpdateUsers(Users objUsers, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Update_Users", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Transaction = trn;
                cmd.Parameters.Add("@Address1", SqlDbType.VarChar, 50).Value = objUsers.Address1;
                cmd.Parameters.Add("@Address2", SqlDbType.VarChar, 50).Value = objUsers.Address2;
                cmd.Parameters.Add("@City", SqlDbType.VarChar, 50).Value = objUsers.City;
                cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 50).Value = objUsers.EmailID;
                cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = objUsers.FirstName;
                cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = objUsers.IsActive;
                cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = objUsers.LastName;
                cmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 11).Value = objUsers.Mobile;
                cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = objUsers.Password;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = objUsers.UserID;
                cmd.Parameters.Add("@OperatorID", SqlDbType.Int).Value = objUsers.OperatorID;
                cmd.Parameters["@UserID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@UserType", SqlDbType.Int).Value = objUsers.UserType;

                cmd.ExecuteNonQuery();

                //after updating the Users, update UserID
                objUsers.UserID = Convert.ToInt32(cmd.Parameters["@UserID"].Value);

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

        public bool DeleteUsers(int UserID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from Users where UserID=@UserID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }

        public bool DeleteUserPermission(int UserID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from UserPermission where UserID=@UserID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                cmd.ExecuteNonQuery();


                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }

        public bool InsertIntoUserPermission(UserPermission objUserPermission, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Into_UserPermission", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;
                cmd.Parameters.Add("@ExtPermissions", SqlDbType.VarChar, 2000).Value = objUserPermission.ExtPermissions;
                cmd.Parameters.Add("@FormID", SqlDbType.Int).Value = objUserPermission.FormID;
                cmd.Parameters.Add("@Permissions", SqlDbType.VarChar, 2000).Value = objUserPermission.Permissions;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = objUserPermission.UserID;

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
        /// <summary>
        /// For Checking User Avaialabilty
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        public bool CheckUserAvailability(string loginID)
        {
            bool isAvail = false;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("Select COUNT(*) from Users where EmailID=@LoginID", con);
            cmd.Parameters.Add("@LoginID", SqlDbType.VarChar, 100).Value = loginID;
            try
            {
                con.Open();
                isAvail = Convert.ToInt32(cmd.ExecuteScalar()) <= 0;
                con.Close();
            }
            finally
            {
                con.Dispose();
            }
            return isAvail;
        }

        public bool InsertIntoCBArea(CBArea objCBArea, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Into_CBArea", trn.Connection);
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;
                cmd.Parameters.Add("@Area", SqlDbType.VarChar, 500).Value = objCBArea.Area;
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = objCBArea.UserID;

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

        public bool DeleteCBArea(int UserID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from CBArea where UserID=@UserID", trn.Connection);
                cmd.Transaction = trn;

                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

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

