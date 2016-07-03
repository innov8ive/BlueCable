using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConSys;
using System.Data;

namespace HMS
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTableToPDF obj=new DataTableToPDF();
            obj.Header = "Employees Details";
            DataTable dt = Common.GetDBResult("SELECT Address1 as Address,DOB,EmailID,FirstName+ ' '+MiddleName as Name,Mobile FROM Employees ");
            obj.DataTableToPdf(dt, new float[] {10f, 10f, 10f, 10f, 10f});
        }
    }
}