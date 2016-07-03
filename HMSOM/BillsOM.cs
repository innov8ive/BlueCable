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
[Table(Name = "dbo.Bills")]
public class Bills
{ private System.Nullable<decimal> _AddOnPrice;
private string _BankName;
private System.Nullable<decimal> _BasicPrice;
private System.Nullable<DateTime> _BillDate;
private System.Nullable<int> _BillID;
private string _BranchName;
private System.Nullable<DateTime> _ChequeDate;
private string _ChequeNo;
private System.Nullable<decimal> _CollectedAmount;
private System.Nullable<int> _CollectedBy;
private System.Nullable<int> _CustomerID;
private System.Nullable<decimal> _Discount;
private System.Nullable<decimal> _EntTax;
private System.Nullable<int> _GeneratedBy;
private System.Nullable<DateTime> _GeneratedDate;
private System.Nullable<decimal> _NetBillAmount;
private System.Nullable<decimal> _Outstanding;
private System.Nullable<DateTime> _PaymentDate;
private string _PaymentMode;
private string _Remarks;
private System.Nullable<decimal> _ServiceTaxPerc;
 
[Column(Storage = "_AddOnPrice")]
public System.Nullable<decimal> AddOnPrice
{
get 
{
return _AddOnPrice;
}
set 
{
 _AddOnPrice=value;
}
} [Column(Storage = "_BankName")]
public string BankName
{
get 
{
return _BankName;
}
set 
{
 _BankName=value;
}
} [Column(Storage = "_BasicPrice")]
public System.Nullable<decimal> BasicPrice
{
get 
{
return _BasicPrice;
}
set 
{
 _BasicPrice=value;
}
} [Column(Storage = "_BillDate")]
public System.Nullable<DateTime> BillDate
{
get 
{
return _BillDate;
}
set 
{
 _BillDate=value;
}
} [Column(Storage = "_BillID")]
public System.Nullable<int> BillID
{
get 
{
return _BillID;
}
set 
{
 _BillID=value;
}
} [Column(Storage = "_BranchName")]
public string BranchName
{
get 
{
return _BranchName;
}
set 
{
 _BranchName=value;
}
} [Column(Storage = "_ChequeDate")]
public System.Nullable<DateTime> ChequeDate
{
get 
{
return _ChequeDate;
}
set 
{
 _ChequeDate=value;
}
} [Column(Storage = "_ChequeNo")]
public string ChequeNo
{
get 
{
return _ChequeNo;
}
set 
{
 _ChequeNo=value;
}
} [Column(Storage = "_CollectedAmount")]
public System.Nullable<decimal> CollectedAmount
{
get 
{
return _CollectedAmount;
}
set 
{
 _CollectedAmount=value;
}
} [Column(Storage = "_CollectedBy")]
public System.Nullable<int> CollectedBy
{
get 
{
return _CollectedBy;
}
set 
{
 _CollectedBy=value;
}
} [Column(Storage = "_CustomerID")]
public System.Nullable<int> CustomerID
{
get 
{
return _CustomerID;
}
set 
{
 _CustomerID=value;
}
} [Column(Storage = "_Discount")]
public System.Nullable<decimal> Discount
{
get 
{
return _Discount;
}
set 
{
 _Discount=value;
}
} [Column(Storage = "_EntTax")]
public System.Nullable<decimal> EntTax
{
get 
{
return _EntTax;
}
set 
{
 _EntTax=value;
}
} [Column(Storage = "_GeneratedBy")]
public System.Nullable<int> GeneratedBy
{
get 
{
return _GeneratedBy;
}
set 
{
 _GeneratedBy=value;
}
} [Column(Storage = "_GeneratedDate")]
public System.Nullable<DateTime> GeneratedDate
{
get 
{
return _GeneratedDate;
}
set 
{
 _GeneratedDate=value;
}
} [Column(Storage = "_NetBillAmount")]
public System.Nullable<decimal> NetBillAmount
{
get 
{
return _NetBillAmount;
}
set 
{
 _NetBillAmount=value;
}
} [Column(Storage = "_Outstanding")]
public System.Nullable<decimal> Outstanding
{
get 
{
return _Outstanding;
}
set 
{
 _Outstanding=value;
}
} [Column(Storage = "_PaymentDate")]
public System.Nullable<DateTime> PaymentDate
{
get 
{
return _PaymentDate;
}
set 
{
 _PaymentDate=value;
}
} [Column(Storage = "_PaymentMode")]
public string PaymentMode
{
get 
{
return _PaymentMode;
}
set 
{
 _PaymentMode=value;
}
} [Column(Storage = "_Remarks")]
public string Remarks
{
get 
{
return _Remarks;
}
set 
{
 _Remarks=value;
}
} [Column(Storage = "_ServiceTaxPerc")]
public System.Nullable<decimal> ServiceTaxPerc
{
get 
{
return _ServiceTaxPerc;
}
set 
{
 _ServiceTaxPerc=value;
}
} }


}
