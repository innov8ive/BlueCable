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
    public class CustomersBL
    {
        #region Declaration
        private string connectionString;
        Customers _Customers;
        public Customers Data
        {
            get { return _Customers; }
            set { _Customers = value; }
        }
        public bool IsNew
        {
            get { return (_Customers.CustomerID <= 0 || _Customers.CustomerID == null); }
        }
        #endregion

        #region Constructor
        public CustomersBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private CustomersDL CreateDL()
        {
            return new CustomersDL(connectionString);
        }
        public void New()
        {
            _Customers = new Customers();
        }
        public void Load(int CustomerID)
        {
            var CustomersObj = this.CreateDL();
            _Customers = CustomerID <= 0 ? CustomersObj.Load(-1) : CustomersObj.Load(CustomerID);
        }
        public DataTable LoadAllCustomers()
        {
            var CustomersDLObj = CreateDL();
            return CustomersDLObj.LoadAllCustomers();
        }
        public bool Update()
        {
            var CustomersDLObj = CreateDL();
            return CustomersDLObj.Update(this.Data);
        }
        public bool Delete(int CustomerID)
        {
            var CustomersDLObj = CreateDL();
            return CustomersDLObj.Delete(CustomerID);
        }
        #endregion
    }
}
