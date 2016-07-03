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
    [Table(Name = "dbo.Packages")]
    public class Packages
    {
        private System.Nullable<decimal> _AddOnPrice;
        private System.Nullable<decimal> _BasicPrice;
        private System.Nullable<decimal> _Discount;
        private System.Nullable<int> _PackageID;
        private string _PackageName;
        private System.Nullable<decimal> _ServiceTaxPerc;
        private System.Nullable<int> _OperatorID;
        private System.Nullable<decimal> _EntTax;
        private string _Channels;

        [Column(Storage = "_Channels")]
        public string Channels
        {
            get
            {
                return _Channels;
            }
            set
            {
                _Channels = value;
            }
        }
        [Column(Storage = "_EntTax")]
        public System.Nullable<decimal> EntTax
        {
            get
            {
                return _EntTax;
            }
            set
            {
                _EntTax = value;
            }
        }
        [Column(Storage = "_AddOnPrice")]
        public System.Nullable<decimal> AddOnPrice
        {
            get
            {
                return _AddOnPrice;
            }
            set
            {
                _AddOnPrice = value;
            }
        }
        [Column(Storage = "_BasicPrice")]
        public System.Nullable<decimal> BasicPrice
        {
            get
            {
                return _BasicPrice;
            }
            set
            {
                _BasicPrice = value;
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
        [Column(Storage = "_PackageID")]
        public System.Nullable<int> PackageID
        {
            get
            {
                return _PackageID;
            }
            set
            {
                _PackageID = value;
            }
        }
        [Column(Storage = "_PackageName")]
        public string PackageName
        {
            get
            {
                return _PackageName;
            }
            set
            {
                _PackageName = value;
            }
        }
        [Column(Storage = "_ServiceTaxPerc")]
        public System.Nullable<decimal> ServiceTaxPerc
        {
            get
            {
                return _ServiceTaxPerc;
            }
            set
            {
                _ServiceTaxPerc = value;
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
        public System.Collections.Generic.List<PackageChannels> PackageChannelsList { get; set; }
        public Hashtable Types { get; set; }
    }
    [Serializable]
    [Table(Name = "dbo.PackageChannels")]
    public class PackageChannels
    {
        private System.Nullable<int> _ChannelID;
        private System.Nullable<int> _PackageID;
        private string _ChannelName;
        private string _Language;

        [Column(Storage = "_ChannelID")]
        public System.Nullable<int> ChannelID
        {
            get
            {
                return _ChannelID;
            }
            set
            {
                _ChannelID = value;
            }
        }
        [Column(Storage = "_PackageID")]
        public System.Nullable<int> PackageID
        {
            get
            {
                return _PackageID;
            }
            set
            {
                _PackageID = value;
            }
        }
        [Column(Storage = "_ChannelName")]
        public string ChannelName
        {
            get
            {
                return _ChannelName;
            }
            set
            {
                _ChannelName = value;
            }
        }
        [Column(Storage = "_Language")]
        public string Language
        {
            get
            {
                return _Language;
            }
            set
            {
                _Language = value;
            }
        }
    }


}
