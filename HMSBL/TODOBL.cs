using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using HMSDL;
using HMSOM;

namespace HMSBL
{
    [Serializable]
    public class TODOBL
    {

        #region Declaration
        private string connectionString;
        TODO _TODO;
        public TODO Data
        {
            get { return _TODO; }
            set { _TODO = value; }
        }
        public bool IsNew
        {
            get { return (_TODO.ID <= 0 || _TODO.ID == null); }
        }
        #endregion

        #region Constructor
        public TODOBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private TODODL CreateDL()
        {
            return new TODODL(connectionString);
        }
        public void New()
        {
            _TODO = new TODO();
        }
        public void Load(int ID)
        {
            var TODOObj = this.CreateDL();
            _TODO = ID <= 0 ? TODOObj.Load(-1) : TODOObj.Load(ID);
        }
        public DataTable LoadAllTODO()
        {
            var TODODLObj = CreateDL();
            return TODODLObj.LoadAllTODO();
        }
        public bool Update()
        {
            var TODODLObj = CreateDL();
            return TODODLObj.Update(this.Data);
        }
        public bool Delete(int ID)
        {
            var TODODLObj = CreateDL();
            return TODODLObj.Delete(ID);
        }
        public void MarkCompleted(int id)
        {
            var TODODLObj = CreateDL();
            TODODLObj.MarkCompleted(id);
        }
        #endregion
    }
}
