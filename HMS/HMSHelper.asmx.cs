using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;

namespace HMS
{
    [Serializable]
    public class AutoSugggestData
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Summary description for HMSHelper
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HMSHelper : System.Web.Services.WebService
    {
        [WebMethod]
        public List<AutoSugggestData> GetOperators(string prefix)
        {
            DataTable dt = new DataTable();
            List<AutoSugggestData> result = new List<AutoSugggestData>();

            SqlConnection con = new SqlConnection(Common.GetConString());
            SqlCommand cmd =
                new SqlCommand(
                    "select OperatorID,OperatorName from Operators where  OperatorName like '%'+@Name+'%'",
                    con);
            cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = prefix;

            con.Open();
            using(con)
            {
                dt.Load(cmd.ExecuteReader());
            }
            if(dt!=null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    result.Add(new AutoSugggestData()
                                   {
                                       ID = Common.ToString(dr["OperatorID"]),
                                       Name = Common.ToString(dr["OperatorName"])
                                   }
                        );
                }
            }
            return result;
        }
    }
}

