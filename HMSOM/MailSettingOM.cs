using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data;
using System.Data.Linq.Mapping;

namespace HMSOM
{

    [Serializable]
    [Table(Name = "dbo.MailSetting")]
    public class MailSetting
    {
        private string _EmailID;
        private System.Nullable<bool> _EnableSSL;
        private System.Nullable<int> _EntryID;
        private string _MailServer;
        private string _Password;
        private string _SMSAgentURL;
        private string _SMSUserName;
        private string _SMSPassword;
        private string _DisplayName;
        private int? _Port;

        [Column(Storage = "_Port")]
        public int? Port
        {
            get
            {
                return _Port;
            }
            set
            {
                _Port = value;
            }
        }
        [Column(Storage = "_DisplayName")]
        public string DisplayName
        {
            get
            {
                return _DisplayName;
            }
            set
            {
                _DisplayName = value;
            }
        }
        [Column(Storage = "_EmailID")]
        public string EmailID
        {
            get
            {
                return _EmailID;
            }
            set
            {
                _EmailID = value;
            }
        }
        [Column(Storage = "_EnableSSL")]
        public System.Nullable<bool> EnableSSL
        {
            get
            {
                return _EnableSSL;
            }
            set
            {
                _EnableSSL = value;
            }
        }
        [Column(Storage = "_EntryID")]
        public System.Nullable<int> EntryID
        {
            get
            {
                return _EntryID;
            }
            set
            {
                _EntryID = value;
            }
        }
        [Column(Storage = "_MailServer")]
        public string MailServer
        {
            get
            {
                return _MailServer;
            }
            set
            {
                _MailServer = value;
            }
        }
        [Column(Storage = "_Password")]
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }
        [Column(Storage = "_SMSAgentURL")]
        public string SMSAgentURL
        {
            get
            {
                return _SMSAgentURL;
            }
            set
            {
                _SMSAgentURL = value;
            }
        }
        [Column(Storage = "_SMSUserName")]
        public string SMSUserName
        {
            get
            {
                return _SMSUserName;
            }
            set
            {
                _SMSUserName = value;
            }
        }
        [Column(Storage = "_SMSPassword")]
        public string SMSPassword
        {
            get
            {
                return _SMSPassword;
            }
            set
            {
                _SMSPassword = value;
            }
        }
    }


}
