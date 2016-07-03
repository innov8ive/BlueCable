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
    public class BillsBL
    {
        #region Declaration
        private string connectionString;
        Bills _Bills;
        public Bills Data
        {
            get { return _Bills; }
            set { _Bills = value; }
        }
        public bool IsNew
        {
            get { return (_Bills.BillID <= 0 || _Bills.BillID == null); }
        }
        #endregion

        #region Constructor
        public BillsBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private BillsDL CreateDL()
        {
            return new BillsDL(connectionString);
        }
        public void New()
        {
            _Bills = new Bills();
        }
        public void Load(int BillID)
        {
            var BillsObj = this.CreateDL();
            _Bills = BillID <= 0 ? BillsObj.Load(-1) : BillsObj.Load(BillID);
        }
        public DataTable LoadAllBills()
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.LoadAllBills();
        }
        public bool Update()
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.Update(this.Data);
        }
        public bool Delete(int BillID)
        {
            var BillsDLObj = CreateDL();
            return BillsDLObj.Delete(BillID);
        }
        #endregion
    }
}
