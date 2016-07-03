using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace HMS.Masters.Packages
{
    public partial class ChannelSelector : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindLanguageDDL(ddlLanguage);
                BindChannelList(chkChannelList, 0);
            }
        }
        protected void ddlLanguage_SelectedIndexChanged(object sender,EventArgs e)
        {
            BindChannelList(chkChannelList, Common.ToInt(ddlLanguage.SelectedValue));
        }
        protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach(ListItem itm in chkChannelList.Items)
            {
                itm.Selected = chkCheckAll.Checked;
            }
        }
      
        protected void btnSelect_Click(object sender,EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem itm in chkChannelList.Items)
            {
                if (itm.Selected)
                    sb.Append(",").Append(itm.Value);
            }
            string chList = sb.Length > 0 ? sb.ToString().Substring(1) : String.Empty;
         ScriptManager.RegisterStartupScript(this,this.GetType(), "seleced", "Selected('" + chList + "');", true);
        }
        private void BindLanguageDDL(DropDownList ddl)
        {
            DataTable dt = Common.GetDBResult("Select *from Languages");
            ddl.DataSource = dt;
            ddl.DataTextField = "LanguageName";
            ddl.DataValueField = "LanguageID";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("(All)", "0"));
        }
        private void BindChannelList(CheckBoxList ddl, int languageID)
        {
            string query = @"select C.ChannelID,C.ChannelName+' - '+L.LanguageName as ChannelName from Channels C
inner join Languages L on L.LanguageID=C.LanguageID where C.IsActive=1";
            if (languageID > 0)
                query = query + " and C.LanguageID=" + languageID;
            DataTable dt = Common.GetDBResult(query);
            ddl.DataSource = dt;
            ddl.DataTextField = "ChannelName";
            ddl.DataValueField = "ChannelID";
            ddl.DataBind();
        }
    }
}