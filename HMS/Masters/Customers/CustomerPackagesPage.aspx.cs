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
    public partial class CustomerPackagesPage : SimpleBasePage
    {


        #region Private Members
        CustomersBL _CustomersBLObj;
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
            _CustomersBLObj = (CustomersBL)PageSession["CustomersBLObj"];
            if (!IsPostBack)
            {
                PageSession["ChildSave"] = "true";
                BindCustomerPackagesGrid();
            }
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageSession["CustomersBLObj"] = _CustomersBLObj;
        }
        #endregion

        #region Control Events
        protected void addBtnCustomerPackages_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "New":
                    AddNewCustomerPackages();
                    BindCustomerPackagesGrid();
                    break;
                case "Cancel":
                    //deleting the newly added blank item if have
                    CancelCustomerPackages();
                    BindCustomerPackagesGrid();
                    break;
            }
        }
        protected void editBtnCustomerPackages_Click(object sender, EventArgs e)
        {
            EditCustomerPackages();
            BindCustomerPackagesGrid();
        }
        protected void updateBtnCustomerPackages_Click(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Update":
                    SaveRows(); BindCustomerPackagesGrid();
                    break;
            }
        }
        protected void deleteBtnCustomerPackages_Click(object sender, EventArgs e)
        {
            DeleteCustomerPackages();
            BindCustomerPackagesGrid();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            BindCustomerPackagesGrid();
            ClientScriptProxy.Current.RegisterStartupScript(this, this.GetType(), "saveparent", "SaveParent();", true);
        }
        #endregion

        #region Page Methods
        private void BindCustomerPackagesGrid()
        {
            CustomerPackagesExGrid.DataSource = _CustomersBLObj.Data.CustomerPackagesList;
            CustomerPackagesExGrid.DataBind();
        }
        private void AddNewCustomerPackages()
        {
            CustomerPackages objCustomerPackages = new CustomerPackages();
            objCustomerPackages.CustomerID = -2;
            _CustomersBLObj.Data.CustomerPackagesList.Add(objCustomerPackages);
            CustomerPackagesExGrid.SelectedIndex = (_CustomersBLObj.Data.CustomerPackagesList.Count - 1).ToString();
            editModeHdn.Value = "1";
            editBtnCustomerPackages.Enabled = false;
            deleteBtnCustomerPackages.Enabled = false;
            updateBtnCustomerPackages.Enabled = true;
            addBtnCustomerPackages.Text = "Cancel";
            mainPanel.Enabled = true;
            BindRowControls();
        }
        private void EditCustomerPackages()
        {
            sIndexHdn.Value = CustomerPackagesExGrid.SelectedIndex;
            if (String.IsNullOrEmpty(sIndexHdn.Value)) return;
            if (String.IsNullOrEmpty(sIndexHdn.Value)) return;
            editModeHdn.Value = "1";
            editBtnCustomerPackages.Enabled = false;
            deleteBtnCustomerPackages.Enabled = false;
            updateBtnCustomerPackages.Enabled = true;
            addBtnCustomerPackages.Text = "Cancel";
            mainPanel.Enabled = true;
            BindRowControls();
        }
        private void CancelCustomerPackages()
        {
            if (_CustomersBLObj.Data.CustomerPackagesList.Last().CustomerID == -2)
            {
                sIndexHdn.Value = (_CustomersBLObj.Data.CustomerPackagesList.Count - 1).ToString();
                DeleteCustomerPackages();
            }
            editModeHdn.Value = "0";
            editBtnCustomerPackages.Enabled = true;
            deleteBtnCustomerPackages.Enabled = true;
            updateBtnCustomerPackages.Enabled = false;
            addBtnCustomerPackages.Text = "New";
            mainPanel.Enabled = false;
            ClearControls();
        }
        private void DeleteCustomerPackages()
        {
            sIndexHdn.Value = CustomerPackagesExGrid.SelectedIndex;
            if (String.IsNullOrEmpty(sIndexHdn.Value)) return;
            _CustomersBLObj.Data.CustomerPackagesList.RemoveAt(Common.ToInt(sIndexHdn.Value));
            CustomerPackagesExGrid.SelectedIndex = String.Empty;
        }

        private void BindRowControls()
        {
            sIndexHdn.Value = CustomerPackagesExGrid.SelectedIndex;
            CustomerPackages objCustomerPackages = _CustomersBLObj.Data.CustomerPackagesList[Common.ToInt(sIndexHdn.Value)];
            cannoTextBox.Text = Common.ToString(objCustomerPackages.CANNo);
            connectiontypeTextBox.Text = Common.ToString(objCustomerPackages.ConnectionType);
            discountNE.Text = Common.ToString(objCustomerPackages.Discount);
            ddlPackageID.SelectedValue = Common.ToString(objCustomerPackages.PackageID);
            serviceprovideridNE.Text = Common.ToString(objCustomerPackages.ServiceProviderID);
            smartcardnoTextBox.Text = Common.ToString(objCustomerPackages.SmartCardNo);
            stbmakeidNE.Text = Common.ToString(objCustomerPackages.STBMakeID);
            stbnoTextBox.Text = Common.ToString(objCustomerPackages.STBNo);
            totalpriceNE.Text = Common.ToString(objCustomerPackages.TotalPrice);
        }

        private void SaveRows()
        {
            sIndexHdn.Value = CustomerPackagesExGrid.SelectedIndex;
            GridViewRow rw = CustomerPackagesExGrid.Rows[Common.ToInt(sIndexHdn.Value)];
            CustomerPackages objCustomerPackages = _CustomersBLObj.Data.CustomerPackagesList[Common.ToInt(sIndexHdn.Value)];
            objCustomerPackages.CustomerID = _CustomersBLObj.Data.CustomerID;
            objCustomerPackages.CANNo = Common.ToString(cannoTextBox.Text);
            objCustomerPackages.ConnectionType = Common.ToString(connectiontypeTextBox.Text);
            objCustomerPackages.Discount = Common.ToDecimal(discountNE.Text, null);
            objCustomerPackages.PackageID = Common.ToInt(packageidNE.Text, null);
            objCustomerPackages.ServiceProviderID = Common.ToInt(serviceprovideridNE.Text, null);
            objCustomerPackages.SmartCardNo = Common.ToString(smartcardnoTextBox.Text);
            objCustomerPackages.STBMakeID = Common.ToInt(stbmakeidNE.Text, null);
            objCustomerPackages.STBNo = Common.ToString(stbnoTextBox.Text);
            objCustomerPackages.TotalPrice = Common.ToDecimal(totalpriceNE.Text, null);

            editModeHdn.Value = "0";
            editBtnCustomerPackages.Enabled = true;
            deleteBtnCustomerPackages.Enabled = true;
            updateBtnCustomerPackages.Enabled = false;
            addBtnCustomerPackages.Text = "New";
            mainPanel.Enabled = false;
            ClearControls();
        }
        private void ClearControls()
        {
            cannoTextBox.Text = String.Empty;
            connectiontypeTextBox.Text = String.Empty;
            discountNE.Text = String.Empty;
            packageidNE.Text = String.Empty;
            serviceprovideridNE.Text = String.Empty;
            smartcardnoTextBox.Text = String.Empty;
            stbmakeidNE.Text = String.Empty;
            stbnoTextBox.Text = String.Empty;
            totalpriceNE.Text = String.Empty;
        }
        #endregion
    }
}
