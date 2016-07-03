<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.CustomerPackagesPage"
    CodeBehind="CustomerPackagesPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CustomerPackages</title>
</head>
<meta http-equiv="Page-Enter" content="Alpha(opacity=100)" />
<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.5)" />
<body style="background-color: #f5f5f5; width: 100%; height: 100%;">
    <form id="form1" runat="server" style="width: 100%; height: 100%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="mainBarUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="SGrid">
                <tr style="height: 1px">
                    <td>
                        <sc:ExGrid ID="CustomerPackagesExGrid" runat="server" Height="80px" Width="610px"
                            AutoGenerateColumns="False" ToolTipCssClass="toolTip" DataValueField="CustomerID"
                            DataTextField="CustomerID" KeyBoardNavigation="true" OnRowSingleClick="SingleClick();">
                            <FooterStyle CssClass="SGridFooter" />
                            <RowStyle CssClass="SGridItem" />
                            <HeaderStyle CssClass="SGrid_Header" />
                            <SelectedRowStyle CssClass="SGridSelectedItem" />
                            <Columns>
                                <asp:TemplateField HeaderText="CANNo" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbCANNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"CANNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ConnectionType" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbConnectionType" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ConnectionType") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbDiscount" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Discount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PackageID" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbPackageID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"PackageID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ServiceProviderID" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbServiceProviderID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ServiceProviderID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SmartCardNo" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSmartCardNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"SmartCardNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STBMakeID" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSTBMakeID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"STBMakeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="STBNo" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbSTBNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"STBNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TotalPrice" ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbTotalPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TotalPrice") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </sc:ExGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="mainPanel" runat="server" Enabled="false" Height="150px" ScrollBars="Auto">
                            <table style="width: 100%;" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td style="width: 100px;">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="cannoLabel" runat="server" Text="CANNo" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="cannoTextBox" CssClass="CTextBox" runat="server" Width="200px" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="connectiontypeLabel" runat="server" Text="ConnectionType" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="connectiontypeTextBox" CssClass="CTextBox" runat="server" Width="200px">
                                            <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                            <asp:ListItem Value="HD">HD</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="discountLabel" runat="server" Text="Discount" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <sc:NumericEntry ID="discountNE" runat="server" CssClass="CTextBox" Width="200px"
                                            MaxValue="9999999" MinValue="0" DecimalPlace="2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="packageidLabel" runat="server" Text="PackageID" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                          <asp:DropDownList ID="ddlPackageID" runat="server" CssClass="CTextBox" Width="200px">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="serviceprovideridLabel" runat="server" Text="ServiceProviderID" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                          <asp:DropDownList ID="serviceprovideridNE" CssClass="CTextBox" runat="server" Width="200px">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="smartcardnoLabel" runat="server" Text="SmartCardNo" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="smartcardnoTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                            MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="stbmakeidLabel" runat="server" Text="STBMakeID" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="stbmakeidNE" CssClass="CTextBox" runat="server" Width="200px">
                                                </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="stbnoLabel" runat="server" Text="STBNo" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="stbnoTextBox" CssClass="CTextBox" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="totalpriceLabel" runat="server" Text="TotalPrice" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <sc:NumericEntry ID="totalpriceNE" runat="server" CssClass="CTextBox" Width="200px"
                                            MaxValue="9999999" MinValue="0" DecimalPlace="2" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="addBtnCustomerPackages" runat="server" ToolTip="New" Text="New" CssClass="btn2"
                                        OnClick="addBtnCustomerPackages_Click" /></div>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="editBtnCustomerPackages" runat="server" ToolTip="Edit" Text="Edit"
                                        CssClass="btn2" OnClick="editBtnCustomerPackages_Click" OnClientClick="return ValidateEdit();" /></div>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="deleteBtnCustomerPackages" runat="server" ToolTip="Delete" Text="Delete"
                                        CssClass="btn2" OnClick="deleteBtnCustomerPackages_Click" OnClientClick="return ValidateDelete();" />
                                </td>
                                <td>
                                    <asp:Button ID="updateBtnCustomerPackages" runat="server" ToolTip="Update" Text="Update"
                                        CssClass="btn2" OnClick="updateBtnCustomerPackages_Click" Enabled="false" OnClientClick="return ValidateSave();" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <span style="display: none;">
                                        <asp:HiddenField ID="editModeHdn" runat="server" />
                                        <asp:HiddenField ID="sIndexHdn" runat="server" />
                                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" />
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function selectRow(Obj) {
                Obj.offsetParent.parentNode.className = Obj.checked == true ? 'SGridSelectedItem' : 'SGridItem';
            }
            function SaveParent() {
                var btnUpdate = window.parent.document.getElementById('btnUpdate');
                if (btnUpdate != null)
                    btnUpdate.click();
            }
            function SingleClick(Obj) {
                if (document.getElementById('<%=editModeHdn.ClientID %>').value == '1')
                    return false;
                else {
                    return true;
                }
            }
            function ValidateDelete() {
                var theGrid = eval('_' + '<%=CustomerPackagesExGrid.ClientID %>');
                if (theGrid.selectedRow == null) {
                    alert('Please select a record to delete.');
                    return false;
                }
                return confirm('Are you sure to delete?');
            }
            function ValidateEdit() {
                var theGrid = eval('_' + '<%=CustomerPackagesExGrid.ClientID %>');
                if (theGrid.selectedRow == null) {
                    alert('Please select a record to edit.');
                    return false;
                }
                return true;
            }
            function ValidateSave() {
                return true;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
