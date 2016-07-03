using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Reflection;

namespace HMS
{
public class DBHelper
{
private DbProviderFactory dbfactory;
public DBHelper()
{
string dbFactoryType = ConfigurationManager.AppSettings["dbFactoryType"];
if (dbFactoryType == null)
dbFactoryType = "System.Data.SqlClient";
dbfactory = DbProviderFactories.GetFactory(dbFactoryType);
}
public DbConnection DbConnection(string conString)
{
DbConnection con = dbfactory.CreateConnection();
con.ConnectionString = conString;
return con;
}
public DbCommand DbCommand(string commandText, DbConnection con)
{
DbCommand cmd = dbfactory.CreateCommand();
cmd.CommandText = commandText;
cmd.Connection = con;
return cmd;
}
public void AddParameter(DbCommand cmd, string paramName, object value)
{
DbParameter param = dbfactory.CreateParameter();
param.ParameterName = paramName;
param.Value = value;
param.Direction = ParameterDirection.Input;
cmd.Parameters.Add(param);
}
public void AddParameter(DbCommand cmd, string paramName, int size, object value)
{
DbParameter param = dbfactory.CreateParameter();
param.Size = size;
param.ParameterName = paramName;
param.Value = value;
param.Direction = ParameterDirection.Input;
cmd.Parameters.Add(param);
}
public void AddParameter(DbCommand cmd, string paramName, int size, object value, ParameterDirection direction)
{
DbParameter param = dbfactory.CreateParameter();
param.Size = size;
param.ParameterName = paramName;
param.Value = value;
param.Direction = direction;
cmd.Parameters.Add(param);
}
public void AddParameter(DbCommand cmd, string paramName, object value, ParameterDirection direction)
{
DbParameter param = dbfactory.CreateParameter();
param.ParameterName = paramName;
param.Value = value;
param.Direction = direction;
cmd.Parameters.Add(param);
}
public List<T> ExecuteQueryToList<T>(DbCommand cmd)
{
DataTable table = new DataTable();

cmd.Connection.Open();
table.Load(cmd.ExecuteReader());
cmd.Connection.Close();

List<T> list = new List<T>();

T item;
Type listItemType = typeof(T);

for (int i = 0; i < table.Rows.Count; i++)
{
item = (T)Activator.CreateInstance(listItemType);
mapRow(item, table, listItemType, i);
list.Add(item);
}


return list;
}
private void mapRow(object vOb, System.Data.DataTable table, Type type, int row)
{
for (int col = 0; col < table.Columns.Count; col++)
{
var columnName = table.Columns[col].ColumnName;

var prop = type.GetProperty(columnName);
object data = getData(prop, table.Rows[row][col]);
prop.SetValue(vOb, data, null);

}
}
private object getData(PropertyInfo prop, object value)
{
if (prop.PropertyType.Name.Equals("Int32"))
return Convert.ToInt32(value);
if (prop.PropertyType.Name.Equals("Byte"))
return Convert.ToByte(value);
if (prop.PropertyType.Name.Equals("Double"))
return Convert.ToDouble(value);
if (prop.PropertyType.Name.Equals("DateTime"))
return Convert.ToDateTime(value);
if (prop.PropertyType.Name.Contains("Nullable"))
{
//added this condition to check "Nullable" Field"s Value
if (value.ToString() != "0" && value.GetType().Name.Contains("DBNull"))
{
return null;
}
else
{
return value;
}
}
return Convert.ToString(value).Trim();
}
}
}
