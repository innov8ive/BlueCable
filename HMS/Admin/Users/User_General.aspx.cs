using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HMSBL;
using HMSOM;
using SmartControls;

namespace HMS
{
    public partial class User_General : SimpleBasePage
    {
        #region Private Members
        UserBL _UserObj;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PageKey = Request.QueryString["pageKey"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager1.Services.Add(new ServiceReference("~/HMSHelper.asmx"));
            _UserObj = (UserBL) PageSession["UserObj"];
            if (!IsPostBack)
            {
                PageSession["ChildSave"] = "true";
                BindPageControls();
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            SaveInSession();
            PageSession["UserObj"] = _UserObj;
        }
        #endregion

        #region Control Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveInSession();
            ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "setParentTab", "window.parent.setTabPage();", true);
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SaveInSession();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "saveparent", "SaveParent();", true);
        }
        protected void operatorTextBox_TextChanged(object sender, EventArgs e)
        {
            if (Common.ToInt(operatorTextBox.GetValue()) > 0)
            {
                DataTable dt =
                    Common.GetDBResult("select NetworkName,Contact,Address,PANCardNo from Operators where OperatorID=" +
                                       Common.ToInt(operatorTextBox.GetValue()).ToString());
                if(dt!=null && dt.Rows.Count>0)
                {
                    txtNetworkName.Text = Common.ToString(dt.Rows[0]["NetworkName"]);
                    txtAddress.Text = Common.ToString(dt.Rows[0]["Address"]);
                    txtContact.Text = Common.ToString(dt.Rows[0]["Contact"]);
                    txtPANNo.Text = Common.ToString(dt.Rows[0]["PANCardNo"]);
                }
            }
        }
        #endregion

        #region Page Methods
        /// <summary>
        /// For Saving Data in Session
        /// </summary>
        private void SaveInSession()
        {
            _UserObj.Data.FirstName = operatorTextBox.Text;
            _UserObj.Data.OperatorID = Common.ToInt(operatorTextBox.GetValue());
            //_UserObj.Data.DOB = dobDP.Text == String.Empty ? (DateTime?)null : Common.ToDate(dobDP.Text);
        }
        private void BindPageControls()
        {
            operatorTextBox.SetValue(Common.ToInt(_UserObj.Data.OperatorID).ToString());
            txtAddress.Text = _UserObj.Data.Address;
            txtContact.Text = _UserObj.Data.Contact;
            txtNetworkName.Text = _UserObj.Data.NetworkName;
            txtPANNo.Text = _UserObj.Data.NetworkName;
            operatorTextBox.Text = _UserObj.Data.OperatorName;
            //doctorTextBox.Text = Common.ToString(_UserObj.Data.DoctorName);
            //dobDP.Text = _UserObj.Data.DOB == null ? String.Empty : _UserObj.Data.DOB.Value.ToString("dd-MMM-yyyy");
        }
        #endregion
    }
}