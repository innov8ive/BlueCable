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
[Table(Name = "dbo.NotifyQueue")]
public class NotifyQueue
{ private System.Nullable<int> _BillID;
private System.Nullable<int> _CustomerID;
private System.Nullable<int> _Id;
private string _NotifyBy;
private string _NotifyType;
 
[Column(Storage = "_BillID")]
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
} [Column(Storage = "_Id")]
public System.Nullable<int> Id
{
get 
{
return _Id;
}
set 
{
 _Id=value;
}
} [Column(Storage = "_NotifyBy")]
public string NotifyBy
{
get 
{
return _NotifyBy;
}
set 
{
 _NotifyBy=value;
}
} [Column(Storage = "_NotifyType")]
public string NotifyType
{
get 
{
return _NotifyType;
}
set 
{
 _NotifyType=value;
}
} }


}
