using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HMSOM;
using HMSDL;

namespace HMSBL
{
    [Serializable]
    public class RolesBL
    {
        #region Private Members
        private Roles theRolesObj;
        private string conString;
        #endregion

        #region Constructor
        public RolesBL(string connectionString)
        {
            this.conString = connectionString;
        } 
        #endregion

        #region Public Prorperties
        public Roles Data
        {
            get
            {
                return theRolesObj;
            }
            set
            {
                theRolesObj = value;
            }
        }
        public bool IsNew
        {
            get
            {
                if (theRolesObj != null && theRolesObj.RoleID > 0)
                {
                    return false;
                }
                else
                    return true;
            }
        }
        public RolesDL GetDLObj()
        {
            return new RolesDL(conString);
        }
        #endregion

        #region Public Methods
        public void Load(int roleID)
        {
            theRolesObj = GetDLObj().Load(roleID);
        }
        public bool Save()
        {
           return  GetDLObj().UpdateRoles(theRolesObj);
        }
        public DataTable getRoleList()
        {
            return GetDLObj().getRoleList();
        }
        public DataTable getRolePermissionList(int roleID)
        {
            return GetDLObj().getRolePermissionList(roleID);
        }
        #endregion
    }
}
