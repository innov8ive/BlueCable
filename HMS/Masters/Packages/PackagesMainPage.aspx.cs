using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using HMSOM;
using SmartControls;
using HMSBL;
namespace HMS
{
    public partial class PackagesMainPage : SimpleBasePage
    {

        #region Private Members
        //AppSettings objSetting;
        PackagesBL _PackagesBLObj;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.PageID = "2000";
            //objSetting = (AppSettings)Session["AppSettings"];
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Title = "Packages";
            string s = this.PageKey;
            foreach (TabItem itm in Tab1.TabItemCollection)
            {
                itm.NavigateUrl += "?pageKey=" + this.PageKey;
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["PackageID"] != null)
                {
                    int PackageID = Common.ToInt(Request.QueryString["PackageID"]);
                    _PackagesBLObj = new PackagesBL(Common.GetConString());
                    _PackagesBLObj.Load(PackageID);
                    if (_PackagesBLObj.IsNew)
                        lbMode.Text = "Mode: New";
                    else
                    {
                        lbMode.Text = "Mode: Update";
                    }
                    InitPageWithObject();
                }
                PageSession["PackagesBLObj"] = _PackagesBLObj;
            }
            else
            {
                _PackagesBLObj = (PackagesBL)PageSession["PackagesBLObj"];
            }
            Page.Header.DataBind();
            base.OnLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageSession["PackagesBLObj"] = _PackagesBLObj;
        }
        #endregion

        #region Control Events
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ((HiddenField)Page.Form.FindControl(this.PageID + "hdn")).Value = "true";
            ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "c", "CloseWindow('" + this.PageID + "hdn');", true);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (PageSession["ChildSave"] != null && PageSession["ChildSave"].ToString() == "true")
                ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "savechild", "SaveChild();", true);
            else
                if (SaveData())
                {
                    lbMode.Text = "Mode: Update";
                    lbMessage.Text = "Data Saved Sucessfully";
                    CalcTotal();
                }
                else
                {
                    lbMessage.Text = "<span style='color:red;'>Data not saved!</span>";
                }
            uplMain.Update();
        }

        private void CalcTotal()
        {
            decimal total = Common.ToDecimal(_PackagesBLObj.Data.BasicPrice) +
                            Common.ToDecimal(_PackagesBLObj.Data.AddOnPrice);
            total = total + ((total * Common.ToDecimal(_PackagesBLObj.Data.ServiceTaxPerc)) / 100);
            total = total + Common.ToDecimal(_PackagesBLObj.Data.EntTax) -
                    Common.ToDecimal(_PackagesBLObj.Data.Discount);
            totalNE.Text = Math.Ceiling(total).ToString("0.00");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (SaveData())
            {
                lbMode.Text = "Mode: Update";
                lbMessage.Text = "Data Saved Sucessfully";
                CalcTotal();
                uplMain.Update();
                uplButton.Update();
            }
        }
        #endregion

        #region Page Methods


        private void InitPageWithObject()
        {
            if (_PackagesBLObj.IsNew == false)
            {
                packagenameTextBox.ReadOnly = true;
            }
            addonpriceNE.Text = Common.ToString(_PackagesBLObj.Data.AddOnPrice);
            basicpriceNE.Text = Common.ToString(_PackagesBLObj.Data.BasicPrice);
            discountNE.Text = Common.ToString(_PackagesBLObj.Data.Discount);
            packagenameTextBox.Text = Common.ToString(_PackagesBLObj.Data.PackageName);
            servicetaxpercNE.Text = Common.ToString(_PackagesBLObj.Data.ServiceTaxPerc);
            enttaxNE.Text = Common.ToString(_PackagesBLObj.Data.EntTax);
            CalcTotal();
        }

        private bool SaveData()
        {
            _PackagesBLObj.Data.AddOnPrice = Common.ToDecimal(addonpriceNE.Text);
            _PackagesBLObj.Data.BasicPrice = Common.ToDecimal(basicpriceNE.Text);
            _PackagesBLObj.Data.Discount = Common.ToDecimal(discountNE.Text);
            _PackagesBLObj.Data.PackageName = Common.ToString(packagenameTextBox.Text);
            _PackagesBLObj.Data.ServiceTaxPerc = Common.ToDecimal(servicetaxpercNE.Text);
            _PackagesBLObj.Data.EntTax = Common.ToDecimal(enttaxNE.Text);
            UserSetting objSetting = (UserSetting)Session["UserSetting"];
            _PackagesBLObj.Data.OperatorID = objSetting.OperatorID;

            string channels = string.Join(",", _PackagesBLObj.Data.PackageChannelsList.Select(Obj => Obj.ChannelID));
            _PackagesBLObj.Data.Channels = channels;

            if (_PackagesBLObj.Update())
            {
                return true;
            }
            else
                return false;
        }
        #endregion
    }
}

