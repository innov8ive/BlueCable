using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using HMSOM;
using HMSDL;

namespace HMSBL
{
    public class UserBL
    {
        #region Declaration
        private string connectionString;
        Users _Users;
        public Users Data
        {
            get { return _Users; }
            set { _Users = value; }
        }
        public bool IsNew
        {
            get { return (_Users.UserID <= 0 || _Users.UserID == null); }
        }
        #endregion

        #region Constructor
        public UserBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private UsersDL CreateDL()
        {
            return new UsersDL(connectionString);
        }
        public void New()
        {
            _Users = new Users();
        }
        public void Load(int UserID)
        {
            var UsersObj = this.CreateDL();
            _Users = UserID <= 0 ? UsersObj.Load(-1) : UsersObj.Load(UserID);
        }
        public DataTable LoadAllUsers()
        {
            var UsersDLObj = CreateDL();
            return UsersDLObj.LoadAllUsers();
        }
        public bool Update()
        {
            var UsersDLObj = CreateDL();
            return UsersDLObj.Update(this.Data);
        }
        public bool Delete(int UserID)
        {
            var UsersDLObj = CreateDL();
            return UsersDLObj.Delete(UserID);
        }
        /// <summary>
        /// For Checking User Avaialabilty
        /// </summary>
        /// <param name="loginID"></param>
        /// <returns></returns>
        public bool CheckUserAvailability(string loginID)
        {
            var UsersDLObj = CreateDL();
            return UsersDLObj.CheckUserAvailability(loginID);
        }
        #endregion
    }
}
