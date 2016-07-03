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
    [Table(Name = "dbo.Customers")]
    public class Customers
    {
        private string _Address1;
        private string _Address2;
        private string _Address3;
        private string _Area;
        private string _City;
        private string _Country;
        private System.Nullable<int> _CustomerID;
        private string _EmailID;
        private string _FirstName;
        private System.Nullable<bool> _IsActive;
        private string _LandlineNo;
        private string _LastName;
        private string _MiddleName;
        private string _MobileNo;
        private System.Nullable<decimal> _Outstanding;
        private string _PinCode;
        private string _Remarks;
        private string _State;
        private string _UniqueID;
        private System.Nullable<int> _OperatorID;
        private System.Nullable<decimal> _Discount;
        private System.Nullable<decimal> _TotalPayable;
        private DateTime? _CreatedDate;
        private bool? _SMSEnabled;
        private bool? _EmailEnabled;

        [Column(Storage = "_EmailEnabled")]
        public System.Nullable<bool> EmailEnabled
        {
            get
            {
                return _EmailEnabled;
            }
            set
            {
                _EmailEnabled = value;
            }
        }
        [Column(Storage = "_CreatedDate")]
        public System.Nullable<DateTime> CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }
        [Column(Storage = "_SMSEnabled")]
        public System.Nullable<bool> SMSEnabled
        {
            get
            {
                return _SMSEnabled;
            }
            set
            {
                _SMSEnabled = value;
            }
        }
        [Column(Storage = "_Discount")]
        public System.Nullable<decimal> Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
            }
        }
        [Column(Storage = "_TotalPayable")]
        public System.Nullable<decimal> TotalPayable
        {
            get
            {
                return _TotalPayable;
            }
            set
            {
                _TotalPayable = value;
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
        [Column(Storage = "_Address3")]
        public string Address3
        {
            get
            {
                return _Address3;
            }
            set
            {
                _Address3 = value;
            }
        }
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
        [Column(Storage = "_Country")]
        public string Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
            }
        }
        [Column(Storage = "_CustomerID")]
        public System.Nullable<int> CustomerID
        {
            get
            {
                return _CustomerID;
            }
            set
            {
                _CustomerID = value;
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
        [Column(Storage = "_LandlineNo")]
        public string LandlineNo
        {
            get
            {
                return _LandlineNo;
            }
            set
            {
                _LandlineNo = value;
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
        [Column(Storage = "_MiddleName")]
        public string MiddleName
        {
            get
            {
                return _MiddleName;
            }
            set
            {
                _MiddleName = value;
            }
        }
        [Column(Storage = "_MobileNo")]
        public string MobileNo
        {
            get
            {
                return _MobileNo;
            }
            set
            {
                _MobileNo = value;
            }
        }
        [Column(Storage = "_Outstanding")]
        public System.Nullable<decimal> Outstanding
        {
            get
            {
                return _Outstanding;
            }
            set
            {
                _Outstanding = value;
            }
        }
        [Column(Storage = "_PinCode")]
        public string PinCode
        {
            get
            {
                return _PinCode;
            }
            set
            {
                _PinCode = value;
            }
        }
        [Column(Storage = "_Remarks")]
        public string Remarks
        {
            get
            {
                return _Remarks;
            }
            set
            {
                _Remarks = value;
            }
        }
        [Column(Storage = "_State")]
        public string State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }
        [Column(Storage = "_UniqueID")]
        public string UniqueID
        {
            get
            {
                return _UniqueID;
            }
            set
            {
                _UniqueID = value;
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
        public System.Collections.Generic.List<CustomerPackages> CustomerPackagesList { get; set; }
    }

    [Serializable]
    [Table(Name = "dbo.CustomerPackages")]
    public class CustomerPackages
    {
        private string _CANNo;
        private string _ConnectionType;
        private System.Nullable<int> _CustomerID;
        private System.Nullable<decimal> _Discount;
        private System.Nullable<int> _PackageID;
        private System.Nullable<int> _ServiceProviderID;
        private string _SmartCardNo;
        private System.Nullable<int> _STBMakeID;
        private string _STBNo;
        private System.Nullable<decimal> _TotalPrice;
        private Nullable<int> _SrNo;



        [Column(Storage = "_SrNo")]
        public Nullable<int> SrNo
        {
            get { return _SrNo; }
            set { _SrNo = value; }
        }

        [Column(Storage = "_CANNo")]
        public string CANNo
        {
            get { return _CANNo; }
            set { _CANNo = value; }
        }

        [Column(Storage = "_ConnectionType")]
        public string ConnectionType
        {
            get { return _ConnectionType; }
            set { _ConnectionType = value; }
        }

        [Column(Storage = "_CustomerID")]
        public System.Nullable<int> CustomerID
        {
            get { return _CustomerID; }
            set { _CustomerID = value; }
        }

        [Column(Storage = "_Discount")]
        public System.Nullable<decimal> Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        [Column(Storage = "_PackageID")]
        public System.Nullable<int> PackageID
        {
            get { return _PackageID; }
            set { _PackageID = value; }
        }

        [Column(Storage = "_ServiceProviderID")]
        public System.Nullable<int> ServiceProviderID
        {
            get { return _ServiceProviderID; }
            set { _ServiceProviderID = value; }
        }

        [Column(Storage = "_SmartCardNo")]
        public string SmartCardNo
        {
            get { return _SmartCardNo; }
            set { _SmartCardNo = value; }
        }

        [Column(Storage = "_STBMakeID")]
        public System.Nullable<int> STBMakeID
        {
            get { return _STBMakeID; }
            set { _STBMakeID = value; }
        }

        [Column(Storage = "_STBNo")]
        public string STBNo
        {
            get { return _STBNo; }
            set { _STBNo = value; }
        }

        [Column(Storage = "_TotalPrice")]
        public System.Nullable<decimal> TotalPrice
        {
            get { return _TotalPrice; }
            set { _TotalPrice = value; }
        }
    }

}
