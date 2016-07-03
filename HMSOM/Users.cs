using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data;
using System.Data.Linq.Mapping;

namespace HMSOM
{

    [Serializable]
    [Table(Name = "dbo.Users")]
    public class Users
    {
        private string _Address1;
        private string _Address2;
        private string _City;
        private string _EmailID;
        private string _FirstName;
        private System.Nullable<bool> _IsActive;
        private string _LastName;
        private string _Mobile;
        private string _Password;
        private System.Nullable<int> _UserID;
        private System.Nullable<int> _UserType;
        private Nullable<int> _OperatorID;
        private string _Address;
        private string _OperatorName;
        private string _Contact;
        private string _NetworkName;

        [Column(Storage = "_NetworkName")]
        public string NetworkName
        {
            get
            {
                return _NetworkName;
            }
            set
            {
                _NetworkName = value;
            }
        }
        [Column(Storage = "_Contact")]
        public string Contact
        {
            get
            {
                return _Contact;
            }
            set
            {
                _Contact = value;
            }
        }
        [Column(Storage = "_OperatorName")]
        public string OperatorName
        {
            get
            {
                return _OperatorName;
            }
            set
            {
                _OperatorName = value;
            }
        }
        [Column(Storage = "_Address1")]
        public string Address1
        {
            get
            {
                return _Address1;
            }
            set
            {
                _Address1 = value;
            }
        }
        [Column(Storage = "_Address")]
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }
        [Column(Storage = "_Address2")]
        public string Address2
        {
            get
            {
                return _Address2;
            }
            set
            {
                _Address2 = value;
            }
        }
        [Column(Storage = "_City")]
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
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
        [Column(Storage = "_FirstName")]
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }
        [Column(Storage = "_IsActive")]
        public System.Nullable<bool> IsActive
        {
            get
            {
                return _IsActive;
            }
            set
            {
                _IsActive = value;
            }
        }
        [Column(Storage = "_LastName")]
        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }
        [Column(Storage = "_Mobile")]
        public string Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                _Mobile = value;
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
        [Column(Storage = "_UserID")]
        public System.Nullable<int> UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }
        [Column(Storage = "_UserType")]
        public System.Nullable<int> UserType
        {
            get
            {
                return _UserType;
            }
            set
            {
                _UserType = value;
            }
        }
        [Column(Storage = "_OperatorID")]
        public System.Nullable<int> OperatorID
        {
            get
            {
                return _OperatorID;
            }
            set
            {
                _OperatorID = value;
            }
        }
        public System.Collections.Generic.List<UserPermission> UserPermissionList { get; set; }
        public System.Collections.Generic.List<CBArea> CBAreaList { get; set; }
        public Hashtable Types { get; set; }
    }
    [Serializable]
    [Table(Name = "dbo.UserPermission")]
    public class UserPermission
    {
        private string _ExtPermissions;
        private System.Nullable<int> _FormID;
        private string _Permissions;
        private System.Nullable<int> _UserID;

        [Column(Storage = "_ExtPermissions")]
        public string ExtPermissions
        {
            get
            {
                return _ExtPermissions;
            }
            set
            {
                _ExtPermissions = value;
            }
        }
        [Column(Storage = "_FormID")]
        public System.Nullable<int> FormID
        {
            get
            {
                return _FormID;
            }
            set
            {
                _FormID = value;
            }
        }
        [Column(Storage = "_Permissions")]
        public string Permissions
        {
            get
            {
                return _Permissions;
            }
            set
            {
                _Permissions = value;
            }
        }
        [Column(Storage = "_UserID")]
        public System.Nullable<int> UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }
    }

    [Serializable]
    [Table(Name = "dbo.CBArea")]
    public class CBArea
    {
        private string _Area;
        private System.Nullable<int> _UserID;

        [Column(Storage = "_Area")]
        public string Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
            }
        }
        [Column(Storage = "_UserID")]
        public System.Nullable<int> UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }
    }
}
