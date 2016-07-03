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
    [Table(Name = "dbo.TODO")]
    public class TODO
    {
        private System.Nullable<bool> _Completed;
        private System.Nullable<DateTime> _CompletedDate;
        private string _Details;
        private System.Nullable<DateTime> _DueDate;
        private System.Nullable<int> _ID;
        private string _Title;
        private System.Nullable<int> _OperatorID;

        [Column(Storage = "_OperatorID")]
        public System.Nullable<int> OperatorID
        {
            get { return _OperatorID; }
            set { _OperatorID = value; }
        }
        [Column(Storage = "_Completed")]
        public System.Nullable<bool> Completed
        {
            get
            {
                return _Completed;
            }
            set
            {
                _Completed = value;
            }
        }
        [Column(Storage = "_CompletedDate")]
        public System.Nullable<DateTime> CompletedDate
        {
            get
            {
                return _CompletedDate;
            }
            set
            {
                _CompletedDate = value;
            }
        }
        [Column(Storage = "_Details")]
        public string Details
        {
            get
            {
                return _Details;
            }
            set
            {
                _Details = value;
            }
        }
        [Column(Storage = "_DueDate")]
        public System.Nullable<DateTime> DueDate
        {
            get
            {
                return _DueDate;
            }
            set
            {
                _DueDate = value;
            }
        }
        [Column(Storage = "_ID")]
        public System.Nullable<int> ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
        [Column(Storage = "_Title")]
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
    }


}
