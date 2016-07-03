using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using HMSBL;
using SmartControls;
using HMSOM;
namespace HMS
{
    public partial class CustomersMainPage : SimpleBasePage
    {
        #region Private Members
        private UserSetting objSetting;
        CustomersBL _CustomersBLObj;
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            this.PageID = "7";
            base.OnInit(e);
            //objSetting = (AppSettings)Session["AppSettings"];
        }
        protected override void OnLoad(EventArgs e)
        {
            objSetting = (UserSetting)Session["UserSetting"];
            btnSave.Visible = EditAllowed;
            this.Title = "Customers";
            Page.ClientScript.RegisterClientScriptInclude("commonJS", ResolveUrl("~/js/Validation.js"));
            if (!IsPostBack)
            {
                //BindServiceProvidersDDL(serviceprovideridNE);
                DropDownList ddl=new DropDownList();
                BindPackageDDL(ddl);
                if(ddl.Items.Count<=0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "c",
                                                        "alert('No Package Defined, Please define a Package.');" + "CloseWindow('" + this.PageID + "hdn');",
                                                        true);
                }
                if (Request.QueryString["CustomerID"] != null)
                {
                    int CustomerID = Common.ToInt(Request.QueryString["CustomerID"]);
                    _CustomersBLObj = new CustomersBL(Common.GetConString());
                    _CustomersBLObj.Load(CustomerID);
                    if (_CustomersBLObj.IsNew)
                        lbMode.Text = "Mode: New";
                    else
                    {
                        lbMode.Text = "Mode: Update";
                    }
                    InitPageWithObject();
                }
                PageSession["CustomersBLObj"] = _CustomersBLObj;
            }
            else
            {
                _CustomersBLObj = (CustomersBL)PageSession["CustomersBLObj"];
            }
            Page.Header.DataBind();
            base.OnLoad(e);
        }
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageSession["CustomersBLObj"] = _CustomersBLObj;
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
            else if (SaveData())
            {
                lbMode.Text = "Mode: Update";
                uniqueIDTextBox.Text = _CustomersBLObj.Data.UniqueID;
                lbMessage.Text = "Data saved successfully!";
            }
            else
            {
                lbMessage.Text = "<span style='color:red;'>Data not saved!</span>";
            }
            uplMain.Update();
        }

        protected void discountNe_Changed(object sender, EventArgs e)
        {
            CalculateTotalBilling();
        }
        protected void CustomerPackagesExGrid_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlProvider = (DropDownList)e.Row.Cells[4].FindControl("ddlProvider");
                DropDownList ddlPackage = (DropDownList)e.Row.Cells[5].FindControl("ddlPackageID");

                BindServiceProvidersDDL(ddlProvider);
                BindPackageDDL(ddlPackage);

                CustomerPackages objCustomerPackages = (CustomerPackages)e.Row.DataItem;

                Common.SetDDL(ddlPackage, Common.ToInt(objCustomerPackages.PackageID).ToString());
                Common.SetDDL(ddlProvider, Common.ToInt(objCustomerPackages.ServiceProviderID).ToString());
            }
        }
        protected void addBtnCustomerPackages_Click(object sender, EventArgs e)
        {
            _CustomersBLObj.Data.CustomerPackagesList.Add(new CustomerPackages());
            BindCustomerPackageGrid();
            uplTotal.Update();
        }
        protected void deleteBtnCustomerPackages_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(CustomerPackagesExGrid.SelectedIndex)) return;
            _CustomersBLObj.Data.CustomerPackagesList.RemoveAt(Common.ToInt(CustomerPackagesExGrid.SelectedIndex));
            CustomerPackagesExGrid.SelectedIndex = String.Empty;
            BindCustomerPackageGrid();
            uplTotal.Update();
        }
        protected void btnCalc_Click(object sender,EventArgs e)
        {
            SaveCustomerPackage();
            CalculateTotalBilling();
        }
        #endregion

        #region Page Methods
        private void InitPageWithObject()
        {
            if (_CustomersBLObj.Data.CustomerPackagesList == null || _CustomersBLObj.Data.CustomerPackagesList.Count == 0)
            {
                _CustomersBLObj.Data.CustomerPackagesList = new List<CustomerPackages>();
                _CustomersBLObj.Data.CustomerPackagesList.Add(new CustomerPackages());
            }
            uniqueIDTextBox.Text = Common.ToString(_CustomersBLObj.Data.UniqueID);
            address1TextBox.Text = Common.ToString(_CustomersBLObj.Data.Address1);
            areaTextBox.Text = Common.ToString(_CustomersBLObj.Data.Area);
            cityTextBox.Text = Common.ToString(_CustomersBLObj.Data.City);
            countryTextBox.Text = Common.ToString(_CustomersBLObj.Data.Country);
            emailidTextBox.Text = Common.ToString(_CustomersBLObj.Data.EmailID);
            firstnameTextBox.Text = Common.ToString(_CustomersBLObj.Data.FirstName);
            isactiveCheckBox.Checked = Common.ToBool(_CustomersBLObj.Data.IsActive, true).Value;
            landlinenoTextBox.Text = Common.ToString(_CustomersBLObj.Data.LandlineNo);
            lastnameTextBox.Text = Common.ToString(_CustomersBLObj.Data.LastName);
            middlenameTextBox.Text = Common.ToString(_CustomersBLObj.Data.MiddleName);
            mobilenoTextBox.Text = Common.ToString(_CustomersBLObj.Data.MobileNo);
            pincodeTextBox.Text = Common.ToString(_CustomersBLObj.Data.PinCode);
            remarksTextBox.Text = Common.ToString(_CustomersBLObj.Data.Remarks);
            stateTextBox.Text = Common.ToString(_CustomersBLObj.Data.State);
            chkSMSEnabled.Checked = Common.ToBool(_CustomersBLObj.Data.SMSEnabled, true).Value;
            chkEmailEnabled.Checked = Common.ToBool(_CustomersBLObj.Data.EmailEnabled, true).Value;
            if (_CustomersBLObj.IsNew == false)
            {
                outstandingNE.Text = Common.ToString(_CustomersBLObj.Data.Outstanding);
                outstandingNE.ReadOnly = true;
            }
            BindCustomerPackageGrid();
        }
        private void CalculateTotalBilling()
        {
            decimal total = 0;
            DataTable dt;
            foreach (CustomerPackages objCustomerPackages in _CustomersBLObj.Data.CustomerPackagesList)
            {
                dt =
                Common.GetDBResult("Select Total from VW_Packages where PackageID=" +
                                   Common.ToInt(objCustomerPackages.PackageID));
                if (dt.Rows.Count > 0)
                {
                    decimal packageTotal = Common.ToDecimal(dt.Rows[0]["Total"]);
                    packageTotal = packageTotal - Common.ToDecimal(objCustomerPackages.Discount);
                    total += packageTotal;
                }
            }
            totalBillingNe.Text = total.ToString("0.00");
        }
        private bool SaveData()
        {
            _CustomersBLObj.Data.Address1 = Common.ToString(address1TextBox.Text);
            _CustomersBLObj.Data.Area = Common.ToString(areaTextBox.Text);
            _CustomersBLObj.Data.City = Common.ToString(cityTextBox.Text);
            _CustomersBLObj.Data.Country = Common.ToString(countryTextBox.Text);
            _CustomersBLObj.Data.EmailID = Common.ToString(emailidTextBox.Text);
            _CustomersBLObj.Data.FirstName = Common.ToString(firstnameTextBox.Text);
            _CustomersBLObj.Data.IsActive = Common.ToBool(isactiveCheckBox.Checked);
            _CustomersBLObj.Data.LandlineNo = Common.ToString(landlinenoTextBox.Text);
            _CustomersBLObj.Data.LastName = Common.ToString(lastnameTextBox.Text);
            _CustomersBLObj.Data.MiddleName = Common.ToString(middlenameTextBox.Text);
            _CustomersBLObj.Data.MobileNo = Common.ToString(mobilenoTextBox.Text);
            _CustomersBLObj.Data.Outstanding = Common.ToDecimal(outstandingNE.Text);
            _CustomersBLObj.Data.PinCode = Common.ToString(pincodeTextBox.Text);
            _CustomersBLObj.Data.Remarks = Common.ToString(remarksTextBox.Text);
            _CustomersBLObj.Data.State = Common.ToString(stateTextBox.Text);
            _CustomersBLObj.Data.OperatorID = objSetting.OperatorID;
            _CustomersBLObj.Data.TotalPayable = Common.ToDecimal(totalBillingNe.Text);
            _CustomersBLObj.Data.SMSEnabled = chkSMSEnabled.Checked;
            _CustomersBLObj.Data.EmailEnabled = chkEmailEnabled.Checked;

            if (_CustomersBLObj.IsNew)
            {
                _CustomersBLObj.Data.CreatedDate = Common.GetCurDate();
            }
            SaveCustomerPackage();

            if (_CustomersBLObj.Update())
            {
                return true;
            }
            else
                return false;
        }

        private void SaveCustomerPackage()
        {
            int counter = 0;
            foreach (GridViewRow row in CustomerPackagesExGrid.Rows)
            {
                CustomerPackages objCustomerPackages = _CustomersBLObj.Data.CustomerPackagesList[counter];
                objCustomerPackages.CustomerID = _CustomersBLObj.Data.CustomerID;
                objCustomerPackages.Discount =
                    Common.ToDecimal(((NumericEntry) row.Cells[6].FindControl("neDiscount")).Text);
                objCustomerPackages.PackageID =
                    Common.ToInt(((DropDownList) row.Cells[5].FindControl("ddlPackageID")).SelectedValue);
                objCustomerPackages.ServiceProviderID =
                    Common.ToInt(((DropDownList) row.Cells[4].FindControl("ddlProvider")).SelectedValue);
                objCustomerPackages.STBNo = ((TextBox) row.Cells[1].FindControl("txtSTBNo")).Text;
                objCustomerPackages.SmartCardNo = ((TextBox) row.Cells[2].FindControl("txtSmartCardNo")).Text;
                objCustomerPackages.CANNo = ((TextBox) row.Cells[0].FindControl("txtCANNo")).Text;
                objCustomerPackages.ConnectionType =
                    ((DropDownList) row.Cells[2].FindControl("connectiontypeTextBox")).SelectedValue;
                counter++;
            }
        }

        private void BindCustomerPackageGrid()
        {
            CustomerPackagesExGrid.DataSource = _CustomersBLObj.Data.CustomerPackagesList;
            CustomerPackagesExGrid.DataBind();
            CalculateTotalBilling();
        }
        private void BindServiceProvidersDDL(DropDownList ddl)
        {
            DataTable dt = Common.GetDBResult("Select * from ServiceProviders where IsActive=1 Order By Name");
            ddl.DataSource = dt;
            ddl.DataValueField = "ServiceProviderID";
            ddl.DataTextField = "Name";
            ddl.DataBind();

            ddl.Items.Add(new ListItem("(Others)", "0"));
        }
        private void BindPackageDDL(DropDownList ddl)
        {
            DataTable dt = Common.GetDBResult("select PackageID,PackageName from Packages where OperatorID=" + objSetting.OperatorID);
            ddl.DataSource = dt;
            ddl.DataValueField = "PackageID";
            ddl.DataTextField = "PackageName";
            ddl.DataBind();
        }
        #endregion
    }
}

