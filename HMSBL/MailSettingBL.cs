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
    public class MailSettingBL
    {
        #region Declaration
        private string connectionString;
        MailSetting _MailSetting;
        public MailSetting Data
        {
            get { return _MailSetting; }
            set { _MailSetting = value; }
        }
        public bool IsNew
        {
            get { return (_MailSetting.EntryID <= 0 || _MailSetting.EntryID == null); }
        }
        #endregion

        #region Constructor
        public MailSettingBL(string conString)
        {
            connectionString = conString;
        }
        #endregion

        #region Main Methods
        private MailSettingDL CreateDL()
        {
            return new MailSettingDL(connectionString);
        }
        public void New()
        {
            _MailSetting = new MailSetting();
        }
        public void Load(int EntryID)
        {
            var MailSettingObj = this.CreateDL();
            _MailSetting = EntryID <= 0 ? MailSettingObj.Load(-1) : MailSettingObj.Load(EntryID);
        }
        public DataTable LoadAllMailSetting()
        {
            var MailSettingDLObj = CreateDL();
            return MailSettingDLObj.LoadAllMailSetting();
        }
        public bool Update()
        {
            var MailSettingDLObj = CreateDL();
            return MailSettingDLObj.Update(this.Data);
        }
        public bool Delete(int EntryID)
        {
            var MailSettingDLObj = CreateDL();
            return MailSettingDLObj.Delete(EntryID);
        }
        #endregion
    }
}
