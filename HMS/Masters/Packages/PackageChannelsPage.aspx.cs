using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using HMSBL;
using HMSOM;
using SmartControls;
namespace HMS
{
    public partial class PackageChannelsPage : SimpleBasePage
    {
        #region Private Members
        PackagesBL _PackagesBLObj;
        //AppSettings objSetting;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PageID = "2000";
            //objSetting = (AppSettings)Session["AppSettings"];
            this.PageKey = Request.QueryString["pageKey"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _PackagesBLObj = (PackagesBL)PageSession["PackagesBLObj"];
            if (!IsPostBack)
            {
                PageSession["ChildSave"] = "true";
                BindPackageChannelsGrid();
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageSession["PackagesBLObj"] = _PackagesBLObj;
        }
        #endregion

        #region Control Events
        protected void btnSelected_Click(object sender, EventArgs e)
        {
            List<PackageChannels> list = new List<PackageChannels>();
            list = _PackagesBLObj.Data.PackageChannelsList;
            DataTable dt = Common.GetDBResult("Select Channels.*,Languages.LanguageName from Channels inner join Languages ON Channels.LanguageID=Languages.LanguageID where IsActive=1");
           foreach(string channelID in channelListHdn.Value.Split(','))
           {
               if (list.Count(Obj => Obj.ChannelID == Common.ToInt(channelID)) == 0)
               {
                   PackageChannels objPackageChannels = new PackageChannels();
                   objPackageChannels.PackageID = _PackagesBLObj.Data.PackageID;
                   objPackageChannels.ChannelID = Common.ToInt(channelID);
                   DataRow[] drArray = dt.Select("ChannelID=" + Common.ToInt(channelID));
                   if (drArray.Length > 0)
                   {
                       DataRow dr = drArray[0];
                       objPackageChannels.ChannelName =
                           dr["ChannelName"].ToString();
                       objPackageChannels.Language =
                           dr["LanguageName"].ToString();
                       list.Add(objPackageChannels);
                   }
               }
           }
            _PackagesBLObj.Data.PackageChannelsList = list;
            BindPackageChannelsGrid();
        }
        protected void deleteBtnPackageChannels_Click(object sender, EventArgs e)
        {
            DeletePackageChannels();
            BindPackageChannelsGrid();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            BindPackageChannelsGrid();
            ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "saveparent", "SaveParent();", true);
        }
        #endregion

        #region Page Methods
        private void BindPackageChannelsGrid()
        {
            PackageChannelsExGrid.DataSource = _PackagesBLObj.Data.PackageChannelsList;
            PackageChannelsExGrid.DataBind();
        }
        
        private void DeletePackageChannels()
        {
            sIndexHdn.Value = PackageChannelsExGrid.SelectedIndex;
            if (String.IsNullOrEmpty(sIndexHdn.Value)) return;
            _PackagesBLObj.Data.PackageChannelsList.RemoveAt(Common.ToInt(sIndexHdn.Value));
            PackageChannelsExGrid.SelectedIndex = String.Empty;
        }
        #endregion
    }
}
