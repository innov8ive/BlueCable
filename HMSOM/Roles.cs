using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace HMSOM
{
    [Serializable]
    [Table(Name = "dbo.Roles")]
    public class Roles
    {
        private int? _RoleID;
        private string _RoleName;

        [Column(Storage = "_RoleID")]
        public int? RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }
        [Column(Storage = "_RoleName")]
        public string RoleName
        {
            get
            {
                return _RoleName;
            }
            set
            {
                _RoleName = value;
            }
        }

        public List<RolePermission> RolePermissionList { get; set; }
    }

    [Serializable]
    [Table(Name = "dbo.RolePermission")]
    public class RolePermission
    {
        private string _ExtPermissions;
        private int? _FormID;
        private string _Permissions;
        private int? _RoleID;

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
        public int? FormID
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
        [Column(Storage = "_RoleID")]
        public int? RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }
    }
}
