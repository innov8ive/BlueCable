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
    public class PackagesBL
    {
        #region Declaration
        private string connectionString;
        Packages _Packages;
        public Packages Data
        {
            get { return _Packages; }
            set { _Packages = value; }
        }
        public bool IsNew
        {
            get { return (_Packages.PackageID <= 0 || _Packages.PackageID == null); }
        }
        #endregion

        #region Constructor
        public PackagesBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private PackagesDL CreateDL()
        {
            return new PackagesDL(connectionString);
        }
        public void New()
        {
            _Packages = new Packages();
        }
        public void Load(int PackageID)
        {
            var PackagesObj = this.CreateDL();
            _Packages = PackageID <= 0 ? PackagesObj.Load(-1) : PackagesObj.Load(PackageID);
        }
        public DataTable LoadAllPackages()
        {
            var PackagesDLObj = CreateDL();
            return PackagesDLObj.LoadAllPackages();
        }
        public bool Update()
        {
            var PackagesDLObj = CreateDL();
            return PackagesDLObj.Update(this.Data);
        }
        public bool Delete(int PackageID)
        {
            var PackagesDLObj = CreateDL();
            return PackagesDLObj.Delete(PackageID);
        }
        #endregion
    }
}
