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
    public class RolesDL
    {
        #region Private Members
        private string conString = String.Empty; 
        #endregion

        #region Constructor
        public RolesDL(string connectionString)
        {
            this.conString = connectionString;
        } 
        #endregion

        #region Main Methods
        public Roles Load(int roleID)
        {
            Roles objRoles;
            List<Roles> objRolesList = new List<Roles>();
            DataContext objDC = new DataContext(conString);
            objRolesList = objDC.ExecuteQuery<Roles>("Select *from Roles where RoleID={0}", roleID).ToList<Roles>();
            if (objRolesList.Count > 0)
                objRoles = objRolesList[0];
            else
                objRoles = new Roles();
            objRoles.RolePermissionList = objDC.ExecuteQuery<RolePermission>("Select *from RolePermission where RoleID={0}", roleID).ToList<RolePermission>();
            return objRoles;
        }
        public bool UpdateRoles(Roles objRoles)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("Insert_Update_Roles", con);
            con.Open();
            SqlTransaction trn = con.BeginTransaction();
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Transaction = trn;
                cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = objRoles.RoleID;
                cmd.Parameters["@RoleID"].Direction = ParameterDirection.InputOutput;
                cmd.Parameters.Add("@RoleName", SqlDbType.VarChar, 50).Value = objRoles.RoleName;

                cmd.ExecuteNonQuery();
                int roleID = Convert.ToInt32(cmd.Parameters["@RoleID"].Value);
                if (roleID > 0)
                {
                    objRoles.RoleID = roleID;
                    //Update Roles Permissions
                    DeleteRolePermission(roleID, trn);
                    foreach (RolePermission objRolePermission in objRoles.RolePermissionList)
                    {
                        objRolePermission.RoleID = roleID;
                        UpdateRolePermission(objRolePermission, trn);
                    }
                }
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
                con.Close();
                con.Dispose();
                cmd.Dispose();
            }
        }
        public bool DeleteRolePermission(int RoleID, SqlTransaction trn)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Delete from RolePermission where RoleID=@RoleID", trn.Connection);
                cmd.Transaction = trn;
                cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = RoleID;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                trn.Rollback();
                return false;
            }
        }
        public bool UpdateRolePermission(RolePermission objRolePermission, SqlTransaction trn)
        {
            SqlCommand cmd = new SqlCommand("Insert_Into_RolePermission", trn.Connection);
            try
            {
                cmd.Transaction = trn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ExtPermissions", SqlDbType.VarChar, 2000).Value = objRolePermission.ExtPermissions;
                cmd.Parameters.Add("@FormID", SqlDbType.Int, 4).Value = objRolePermission.FormID;
                cmd.Parameters.Add("@Permissions", SqlDbType.VarChar, 2000).Value = objRolePermission.Permissions;
                cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = objRolePermission.RoleID;

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
        #endregion

        #region Other Methods
        public DataTable getRoleList()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand("Select RoleID,RoleName from Roles", con);
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();
            }
            finally
            {
                con.Dispose();
            }
            return dt;
        }
        public DataTable getRolePermissionList(int roleID)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd =
                new SqlCommand("select FormID,Permissions,ExtPermissions  from RolePermission  where RoleID=@RoleID",
                               con);
            cmd.Parameters.Add("@RoleID", SqlDbType.Int, 4).Value = roleID;
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();
            }
            finally
            {
                con.Dispose();
            }
            return dt;
        } 
        #endregion
    }
}
