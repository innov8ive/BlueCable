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
    public class OperatorsBL
    {
        #region Declaration
        private string connectionString;
        Operators _Operators;
        public Operators Data
        {
            get { return _Operators; }
            set { _Operators = value; }
        }
        public bool IsNew
        {
            get { return (_Operators.OperatorID <= 0 || _Operators.OperatorID == null); }
        }
        #endregion

        #region Constructor
        public OperatorsBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private OperatorsDL CreateDL()
        {
            return new OperatorsDL(connectionString);
        }
        public void New()
        {
            _Operators = new Operators();
        }
        public void Load(int OperatorID)
        {
            var OperatorsObj = this.CreateDL();
            _Operators = OperatorID <= 0 ? OperatorsObj.Load(-1) : OperatorsObj.Load(OperatorID);
        }
        public DataTable LoadAllOperators()
        {
            var OperatorsDLObj = CreateDL();
            return OperatorsDLObj.LoadAllOperators();
        }
        public bool Update()
        {
            var OperatorsDLObj = CreateDL();
            return OperatorsDLObj.Update(this.Data);
        }
        public bool Delete(int OperatorID)
        {
            var OperatorsDLObj = CreateDL();
            return OperatorsDLObj.Delete(OperatorID);
        }
        #endregion
    }
}
