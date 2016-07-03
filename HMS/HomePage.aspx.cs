using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Web.Configuration;
namespace HMS
{
    public partial class HomePage : SimpleBasePage
    {
        private DataTable dtForms;
        private DataTable _dtUserPermissions;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lbUserName.Text = UserSetting.NetworkName;
                if (lbUserName.Text.Length > 0)
                    lbUserName.Text += " (" + UserSetting.FirstName + ")";
                else
                    lbUserName.Text = "Admin";
            }
            if (!IsPostBack)
            {
                TreeView trview = new TreeView();
                BindMenu2(trview);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<ul class=\"drop_menu\">");
                foreach (TreeNode tn in trview.Nodes)
                {
                    sb.AppendLine("<li><a href='#").Append(tn.NavigateUrl).Append("'>").Append(tn.Text).Append("</a>");
                    BindSubMenus(sb, tn, false);
                    sb.Append("</li>");
                }
                sb.AppendLine("</ul>");
                Literal1.Text = sb.ToString();
                lbSupport.Text = "<b>Support:</b>" + WebConfigurationManager.AppSettings["Support"];
                lbExpiryDate.Text = "<b>Membership Expiry Date:</b>" +
                                    UserSetting.RegDate.AddYears(3).ToString("dd-MMM-yyyy");
                if (UserSetting.UserType == 1)
                {
                    ifr.Attributes["src"] = "Reports/Dashboard.aspx";
                    spanTitle.InnerHtml = "Dashboard";
                }
            }
        }
        private void BindSubMenus(StringBuilder sb, TreeNode tn, bool isSubMenu)
        {
            TreeNodeCollection tnList = tn.ChildNodes;
            if (tnList.Count > 0)
                sb.Append("<ul class=\"").Append(isSubMenu ? "submenu" : "menu").Append(" parent\">");
            foreach (TreeNode tnChild in tnList)
            {
                bool hasChild = tnChild.ChildNodes.Count > 0;
                sb.AppendLine("<li>");
                string imageUrl = tnChild.ImageUrl.ToLower().Replace("À₪/images", "/image");
                imageUrl = imageUrl.Length > 0 ? ("<img class=\"imgMenu\" src=\"" + imageUrl + "\" alt=\"\" ></img>") : "";
                sb.Append(imageUrl);
                sb.Append("<a onclick='setURL(this)' URL='#").Append(tnChild.NavigateUrl).Append("?formID=").Append(tnChild.Value)
                    .Append("' ").Append(hasChild ? "class=\"parent\" " : "").Append(">").Append(tnChild.Text).Append("</a>");
                BindSubMenus(sb, tnChild, true);
                sb.Append("</li>");
            }
            if (tnList.Count > 0)
                sb.Append("</ul>");
        }
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            Response.Redirect("~/Login.aspx");
        }
        private void BindMenu2(TreeView trview)
        {
            dtForms = Common.GetDBResult("Select * from Forms");
            _dtUserPermissions =
                Common.GetDBResult("Select FormID,Permissions,ExtPermissions from  UserPermission where UserID=" +
                                   UserSetting.UserID);
            foreach (DataRow dr in dtForms.Select("ParentFormID =0", "SrNo"))
            {
                TreeNode tn = new TreeNode();
                tn.Text = dr["FormName"].ToString();
                tn.NavigateUrl = dr["FormUrl"].ToString();
                tn.Value = dr["FormID"].ToString();
                tn.ImageUrl = dr["ImageUrl"].ToString();
                BindSubMenu2(tn, dtForms);
                trview.Nodes.Add(tn);

            }
            ValidateParentNodes(trview);

        }
        private void ValidateParentNodes(TreeView trv)
        {
            //collect valid treenodes
            List<TreeNode> nodeList = new List<TreeNode>();
            foreach (TreeNode tn in trv.Nodes)
            {
                DataRow[] drChild = dtForms.Select("ParentFormID=" + tn.Value);

                if ((tn.ToolTip.Length <= 0 && drChild.Length <= 0))
                {
                    //permission not defined
                    tn.ToolTip = String.Empty;
                    nodeList.Add(tn);
                }
                else if (tn.ToolTip.Length > 0)
                {
                    //permission defined
                    DataRow[] drPerms = _dtUserPermissions.Select("FormID=" + tn.Value);
                    if (drPerms.Length > 0)
                    {
                        nodeList.Add(tn);
                    }
                }
                else if (!(tn.ChildNodes.Count == 0 && drChild.Length > 0))
                {
                    nodeList.Add(tn);
                }
            }

            //after collecting Valid treeNodes, replace 
            trv.Nodes.Clear();
            foreach (TreeNode tn in nodeList)
            {
                trv.ToolTip = String.Empty;
                trv.Nodes.Add(tn);
            }
        }

        private void BindSubMenu2(TreeNode tn, DataTable dt)
        {
            DataRow[] drArray = dt.Select("parentFormID =" + tn.Value, "SrNo");
            foreach (DataRow dr in drArray)
            {
                //check permission defined or not
                if (dr["Permissions"].ToString().Length <= 0)
                {
                    //Permission not defined or user is admin
                    TreeNode tnChild = new TreeNode();
                    tnChild.Text = dr["FormName"].ToString();
                    tnChild.Value = dr["FormID"].ToString();
                    tnChild.NavigateUrl = dr["FormUrl"].ToString();
                    tnChild.ImageUrl = dr["ImageUrl"].ToString();
                    tn.ChildNodes.Add(tnChild);
                    BindSubMenu2(tnChild, dt);
                }
                else
                {
                    //Permission defined and or user is normal
                    DataRow[] drPerms = _dtUserPermissions.Select("FormID=" + dr["FormID"].ToString());
                    if (drPerms.Length > 0)
                    {
                        TreeNode tnChild = new TreeNode();
                        tnChild.Text = dr["FormName"].ToString();
                        tnChild.Value = dr["FormID"].ToString();
                        tnChild.NavigateUrl = dr["FormUrl"].ToString();
                        tnChild.ImageUrl = dr["ImageUrl"].ToString();
                        tn.ChildNodes.Add(tnChild);
                        BindSubMenu2(tnChild, dt);
                    }
                }
            }
            if (tn.ChildNodes.Count == 0 && drArray.Length > 0)
            {
                //child is present but no permission for user
                //delete this node from its parent
                if (tn.Parent != null)
                    tn.Parent.ChildNodes.Remove(tn);
            }

        }
    }
}
