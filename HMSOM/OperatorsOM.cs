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
    [Table(Name = "dbo.Operators")]
    public class Operators
    {
        private string _Address;
        private string _Contact;
        private string _EmailID;
        private string _InterainmentTaxNo;
        private System.Nullable<bool> _IsActive;
        private string _NetworkName;
        private System.Nullable<int> _OperatorID;
        private string _OperatorName;
        private string _PANCardNo;
        private string _PostalLicenceNo;
        private string _ServiceTaxNo;
        private string _TANNo;
        private string _ShortName;
        private DateTime? _RegDate;

        [Column(Storage = "_RegDate")]
        public DateTime? RegDate
        {
            get
            {
                return _RegDate;
            }
            set
            {
                _RegDate = value;
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
        [Column(Storage = "_InterainmentTaxNo")]
        public string InterainmentTaxNo
        {
            get
            {
                return _InterainmentTaxNo;
            }
            set
            {
                _InterainmentTaxNo = value;
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
        [Column(Storage = "_PANCardNo")]
        public string PANCardNo
        {
            get
            {
                return _PANCardNo;
            }
            set
            {
                _PANCardNo = value;
            }
        }
        [Column(Storage = "_PostalLicenceNo")]
        public string PostalLicenceNo
        {
            get
            {
                return _PostalLicenceNo;
            }
            set
            {
                _PostalLicenceNo = value;
            }
        }
        [Column(Storage = "_ServiceTaxNo")]
        public string ServiceTaxNo
        {
            get
            {
                return _ServiceTaxNo;
            }
            set
            {
                _ServiceTaxNo = value;
            }
        }
        [Column(Storage = "_TANNo")]
        public string TANNo
        {
            get
            {
                return _TANNo;
            }
            set
            {
                _TANNo = value;
            }
        }
        [Column(Storage = "_ShortName")]
        public string ShortName
        {
            get
            {
                return _ShortName;
            }
            set
            {
                _ShortName = value;
            }
        }
    }


}
