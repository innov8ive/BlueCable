<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.CustomersMainPage"
    CodeBehind="CustomersMainPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customers</title>
    <style type="text/css">
        .heading
        {
            background-color: #1B8383;
            padding: 5px;
        }
        .heading h3
        {
            margin: 0px;
            color: white;
        }
        td
        {
            vertical-align: top;
        }
        table.total td
        {
            vertical-align: middle;
        }
    </style>
</head>
<body style="width: 100%; height: 100%;">
    <form id="form1" runat="server" style="width: 100%; height: 100%;">
    <table border="0" cellpadding="0" cellspacing="0" style="table-layout: fixed; border-collapse: collapse;
        position: absolute; left: 10px; top: 0px;">
        <tr style="height: 1px">
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr style="height: 1px">
            <td valign="top">
                <asp:UpdatePanel ID="uplMain" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 750px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2" class="btn2">
                                    <asp:Label ID="lbMode" runat="server" Text="Mode:New" Font-Bold="True"></asp:Label>
                                    <span style="float: right;">
                                        <asp:CheckBox ID="isactiveCheckBox" runat="server" Text="Active" /></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td colspan="2" class="heading">
                                                <h3>
                                                    Customer Name</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label1" runat="server" Text="UniqueID" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="uniqueIDTextBox" ReadOnly="true" CssClass="CTextBox" runat="server"
                                                    Width="275px" MaxLength="100" BackColor="silver"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="firstnameLabel" runat="server" Text="First Name" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="firstnameTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="middlenameLabel" runat="server" Text="Middle Name" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="middlenameTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lastnameLabel" runat="server" Text="Last Name" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lastnameTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="heading">
                                                <h3>
                                                    Contact Details</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="mobilenoLabel" runat="server" Text="Mobile No." CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="mobilenoTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="12"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="landlinenoLabel" runat="server" Text="Landline No." CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="landlinenoTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="25"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="emailidLabel" runat="server" Text="EmailID" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="emailidTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td>Send</td>
                                        <td>
                                            <asp:CheckBox ID="chkSMSEnabled" runat="server" Text="SMS" />
                                            <asp:CheckBox ID="chkEmailEnabled" runat="server" Text="Email" />
                                        </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top;">
                                    <table>
                                        <tr>
                                            <td colspan="2" class="heading">
                                                <h3>
                                                    Address Details</h3>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="address1Label" runat="server" Text="Address1" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="address1TextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="500" TextMode="MultiLine"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="areaLabel" runat="server" Text="Area Code" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="areaTextBox" CssClass="CTextBox" runat="server" Width="275px" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="cityLabel" runat="server" Text="City" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="cityTextBox" CssClass="CTextBox" runat="server" Width="275px" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="stateLabel" runat="server" Text="State" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="stateTextBox" CssClass="CTextBox" runat="server" Width="275px" MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="pincodeLabel" runat="server" Text="Pin Code" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="pincodeTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="countryLabel" runat="server" Text="Country" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="countryTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="100"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:Label ID="remarksLabel" runat="server" Text="Remarks" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="remarksTextBox" CssClass="CTextBox" runat="server" Width="275px"
                                                    MaxLength="250" TextMode="MultiLine" Height="50px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr style="height: 1px">
            <td style="border: solid 1px black; padding: 10px;">
                <asp:UpdatePanel ID="uplGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <sc:ExGrid ID="CustomerPackagesExGrid" runat="server" Height="120px" Width="730px"
                            AutoGenerateColumns="False" ToolTipCssClass="toolTip" DataValueField="CustomerID"
                            DataTextField="CustomerID" KeyBoardNavigation="true" OnRowSingleClick="SingleClick();"
                            OnRowDataBound="CustomerPackagesExGrid_OnRowDataBound">
                            <FooterStyle CssClass="SGridFooter" />
                            <RowStyle CssClass="SGridItem" />
                            <HeaderStyle CssClass="SGrid_Header" />
                            <SelectedRowStyle CssClass="SGridSelectedItem" />
                            <Columns>
                                <asp:TemplateField HeaderText="CAN No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCANNo" runat="server" CssClass="CTextBox" Text='<%#DataBinder.Eval(Container.DataItem,"CANNo") %>'
                                            Width="90px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STB No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSTBNo" runat="server" CssClass="CTextBox" Text='<%#DataBinder.Eval(Container.DataItem,"STBNo") %>'
                                            Width="90px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SMC No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSmartCardNo" runat="server" CssClass="CTextBox" Text='<%#DataBinder.Eval(Container.DataItem,"SmartCardNo") %>'
                                            Width="90px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Con. Type" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="connectiontypeTextBox" CssClass="CTextBox" runat="server" Width="90px">
                                            <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                            <asp:ListItem Value="HD">HD</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Provider" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlProvider" CssClass="CTextBox" runat="server" Width="90px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Package" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPackageID" runat="server" CssClass="CTextBox" Width="90px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <sc:NumericEntry ID="neDiscount" runat="server" CssClass="CTextBox" DecimalPlace="2"
                                            MaxLength="4" Text='<%#DataBinder.Eval(Container.DataItem,"Discount") %>' Width="50px"></sc:NumericEntry>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </sc:ExGrid>
                        <asp:LinkButton ID="addBtnCustomerPackages" runat="server" ToolTip="New" Text="New" OnClick="addBtnCustomerPackages_Click" />
                        <asp:LinkButton ID="deleteBtnCustomerPackages" runat="server" ToolTip="Delete" Text="Delete"
                            OnClick="deleteBtnCustomerPackages_Click" OnClientClick="return ValidateDelete();" ForeColor="red" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr style="height: 1px">
            <td>
                <asp:UpdatePanel ID="uplTotal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table align="center" class="total">
                            <tr>
                                <td>
                                    <asp:Button ID="btnCalc" runat="server" OnClick="btnCalc_Click" Text="Calculate Total"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Label ID="outstandingLabel" runat="server" Text="Outstanding" CssClass="CLabel"></asp:Label>
                                </td>
                                <td>
                                    <sc:NumericEntry ID="outstandingNE" runat="server" CssClass="CTextBox" Width="50px"
                                        MaxValue="9999999" MinValue="0" DecimalPlace="2" />
                                </td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Total Billing: " CssClass="bigHeading"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="totalBillingNe" runat="server" CssClass="bigHeading" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr style="height: 1px">
            <td valign="top">
                <asp:UpdatePanel ID="uplButton" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lbMessage" runat="server" Text="" CssClass="bigHeading"></asp:Label>
                        <br/>
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" OnClientClick="return Validate();"
                            CssClass="btn1" />
                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" OnClientClick="return confirm('Do you want to close this window?');"
                            CssClass="btn1" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function CloseWindow(pageClosedHdn) {
                var closedHidden = document.getElementById(pageClosedHdn);
                closedHidden.value = 'true';
                window.returnValue = 1;
                this.close();
                return false;
            }
            function SingleClick() {
                return true;
            }
            function Validate() {
                if (ValidateBlank(document.getElementById('<%=firstnameTextBox.ClientID %>'), 'First Name') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=lastnameTextBox.ClientID %>'), 'Last Name') == false)
                    return false;
                if (document.getElementById('<%=chkEmailEnabled.ClientID %>').checked == true 
                    && ValidateBlank(document.getElementById('<%=emailidTextBox.ClientID %>'), 'Email') == false)
                    return false;
                if (document.getElementById('<%=chkSMSEnabled.ClientID %>').checked == true
                    && ValidateBlank(document.getElementById('<%=mobilenoTextBox.ClientID %>'), 'Mobile') == false)
                    return false;
                if (ValidateEmail(document.getElementById('<%=emailidTextBox.ClientID %>'), 'Email') == false)
                    return false;
                return true;
            }
            function ValidateDelete() {
                var theGrid = eval('_' + '<%=CustomerPackagesExGrid.ClientID %>');
                if (theGrid.selectedRow == null) {
                    alert('Please select a record to delete.');
                    return false;
                }
                if (document.getElementById('<%=CustomerPackagesExGrid.ClientID %>').tBodies[0].rows.length <= 1) {
                    alert('You can not delete first record.');
                    return false;
                }
                return confirm('Are you sure to delete?');
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
