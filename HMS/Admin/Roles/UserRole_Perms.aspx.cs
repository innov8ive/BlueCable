using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HMSBL;
using HMSOM;

namespace HMS
{
    public partial class UserRole_Perms : SimpleBasePage 
    {
        #region Private Members
        RolesBL _rolesObj; 
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PageKey = Request.QueryString["pageKey"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            _rolesObj = (RolesBL)PageSession["RolesObj"];
            if (!IsPostBack)
            {
                PageSession["ChildSave"] = "true";
                BindMenu(roleTreeView, 0);
                BindPageControls();
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SaveInSession();
            PageSession["RolesObj"] = _rolesObj;
        }
        #endregion

        #region Control Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveInSession();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saveparent", "SaveParent();", true);
        } 
        #endregion

        #region Page Methods
        /// <summary>
        /// For Binding Main Menus
        /// </summary>
        private void BindMenu(TreeView trview, int parentCategoryID)
        {
            DataTable dtForms = Common.GetDBResult("Select * from Forms");
            trview.Nodes.Clear();
            TreeNode itmMenu;
            foreach (DataRow drMenu in dtForms.Select("ParentFormID=" + parentCategoryID + " AND FormName <> 'Help'"))
            {
                itmMenu = new TreeNode();
                itmMenu.Text = drMenu["FormName"].ToString();
                itmMenu.NavigateUrl = "#";
                itmMenu.Value = drMenu["FormID"].ToString();
                trview.Nodes.Add(itmMenu);
                BindSubMenu(dtForms, itmMenu);
            }
        }
        /// <summary>
        /// For Binding Sub Menus
        /// </summary>
        /// <param name="dtForms"></param>
        /// <param name="parentMenuItem"></param>
        private void BindSubMenu(DataTable dtForms, TreeNode parentMenuItem)
        {
            TreeNode itmSubMenu;
            foreach (DataRow drMenu in dtForms.Select("ParentFormID=" + parentMenuItem.Value))
            {
                if (drMenu["FormUrl"].ToString() == String.Empty || drMenu["Permissions"].ToString() != String.Empty)
                {
                    itmSubMenu = new TreeNode();
                    itmSubMenu.Text = drMenu["FormName"].ToString();
                    itmSubMenu.NavigateUrl = "#";
                    itmSubMenu.Value = drMenu["FormID"].ToString();
                    parentMenuItem.ChildNodes.Add(itmSubMenu);
                    BindRights(drMenu["Permissions"].ToString(), drMenu["ExtendedPerms"].ToString(), itmSubMenu, drMenu["FormID"].ToString());
                    BindSubMenu(dtForms, itmSubMenu);
                    if (itmSubMenu.ChildNodes.Count == 0)
                        parentMenuItem.ChildNodes.Remove(itmSubMenu);
                }
            }
        }
        /// <summary>
        /// For Binding Rights
        /// </summary>
        /// <param name="rights"></param>
        /// <param name="parentNode"></param>
        /// <param name="formID"></param>
        private void BindRights(string rights,string extRights, TreeNode parentNode,string formID)
        {
            TreeNode itmSubMenu;
            if (rights == String.Empty && extRights==String.Empty) {parentNode.ChildNodes.Remove(parentNode); return; }
            foreach (string right in rights.Split(','))
            {
                itmSubMenu = new TreeNode();
                itmSubMenu.Text = right;
                itmSubMenu.NavigateUrl = "#";
                itmSubMenu.Value = formID;
                parentNode.ChildNodes.Add(itmSubMenu);
            }
            foreach (string right in extRights.Split(','))
            {
                if (right.Length <= 0) continue;
                itmSubMenu = new TreeNode();
                itmSubMenu.Text = right.Split(':')[1];
                itmSubMenu.NavigateUrl = "#";
                itmSubMenu.Value = right.Split(':')[0];
                parentNode.ChildNodes.Add(itmSubMenu);
            }
        }
        /// <summary>
        /// For Saving Data in Session
        /// </summary>
        private void SaveInSession()
        {
            List<TreeNode> nodeList = new List<TreeNode>();
            foreach (TreeNode parentNode in roleTreeView.Nodes)
            {
                getChildNodes(nodeList, parentNode);
            }

            List<TreeNode> theCheckedNodes = nodeList.FindAll(Obj => Obj.Checked == true && Obj.ChildNodes.Count==0).OrderBy(Obj=>Obj.Parent.Value).ToList<TreeNode>();
            List<RolePermission> objRolePermissionList = new List<RolePermission>();
            string rights = String.Empty;
            string extRights = String.Empty;
            int i=0;
            while (i < theCheckedNodes.Count)
            {
                rights = String.Empty;
                extRights = String.Empty;
                List<TreeNode> theFormNodes = theCheckedNodes.Where(Obj => Obj.Parent.Value == theCheckedNodes[i].Parent.Value).ToList<TreeNode>();
                foreach (TreeNode tnode in theFormNodes)
                {
                    if (Common.ToInt(tnode.Value) > 0)
                        rights += "," + tnode.Text;
                    else
                        extRights += "," + tnode.Value;
                }
                RolePermission objRolePermission = new RolePermission();
                objRolePermission.FormID = Common.ToInt(theFormNodes[0].Parent.Value);
                objRolePermission.Permissions = rights.Length > 0 ? rights.Substring(1) : String.Empty;
                objRolePermission.ExtPermissions = extRights.Length > 0 ? extRights.Substring(1) : String.Empty;
                objRolePermissionList.Add(objRolePermission);
                i = i + theFormNodes.Count;
            }
           

            //Override the Permission List
            _rolesObj.Data.RolePermissionList = objRolePermissionList;
        }
        /// <summary>
        /// For Getting Child Nodes
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="parentNode"></param>
        private void getChildNodes(List<TreeNode> nodeList, TreeNode parentNode)
        {
            foreach (TreeNode node in parentNode.ChildNodes)
            {
                nodeList.Add(node);
                getChildNodes(nodeList, node);
            }
        }
        private void BindPageControls()
        {
            //Get All Child Tree Nodes in a List Object
            List<TreeNode> nodeList = new List<TreeNode>();
            foreach (TreeNode parentNode in roleTreeView.Nodes)
            {
                getChildNodes(nodeList, parentNode);
            }

            foreach (RolePermission objRolePerms in _rolesObj.Data.RolePermissionList)
            {
                List<TreeNode> theFormNode = nodeList.FindAll(Obj => Obj.Parent.Value == objRolePerms.FormID.ToString());
                if (theFormNode == null || theFormNode.Count <= 0)
                    continue;
                theFormNode.ForEach(Obj =>
                    {
                        if (Common.ToInt(Obj.Value) > 0)
                        {
                            if ((objRolePerms.Permissions).Contains(Obj.Text))
                                Obj.Checked = true;
                            else
                                Obj.Checked = false;
                        }
                        else
                        {
                            if ((objRolePerms.ExtPermissions).Contains(Obj.Value))
                                Obj.Checked = true;
                            else
                                Obj.Checked = false;
                        }
                    }
                );
            }
        }
        #endregion
    }
}