<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.PackagesMainPage"
    CodeBehind="PackagesMainPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Packages</title>
</head>
<body style="width: 100%; height: 100%;">
    <form id="form1" runat="server" style="width: 100%; height: 100%;">
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; table-layout: fixed;
        border-collapse: collapse; position: absolute; left: 0px; top: 0px;">
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
                        <table style="width: 670px;" cellpadding="3" cellspacing="0">
                            <tr>
                                <td colspan="2" class="btn2">
                                    <asp:Label ID="lbMode" runat="server" Text="Mode:New" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width: 100px;">
                                                <asp:Label ID="packagenameLabel" runat="server" Text="Package Name" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="packagenameTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                    MaxLength="50"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="basicpriceLabel" runat="server" Text="Basic Price" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <sc:NumericEntry ID="basicpriceNE" runat="server" CssClass="CTextBox" Width="200px"
                                                    MaxValue="9999999" MinValue="0" DecimalPlace="2" OnClientBlur="CalcTotal()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="addonpriceLabel" runat="server" Text="Add On Pack" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <sc:NumericEntry ID="addonpriceNE" runat="server" CssClass="CTextBox" Width="200px"
                                                    MaxValue="9999999" MinValue="0" DecimalPlace="2" OnClientBlur="CalcTotal()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text="Total" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <sc:NumericEntry ID="totalNE" runat="server" CssClass="CTextBox" Width="200px" MaxValue="9999999"
                                                    MinValue="0" DecimalPlace="2" ReadOnly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="vertical-align: top;">
                                    <table>
                                        <tr>
                                            <td style="width: 100px;">
                                                <asp:Label ID="Label1" runat="server" Text="Ent Tax" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <sc:NumericEntry ID="enttaxNE" runat="server" CssClass="CTextBox" Width="200px" MaxValue="9999999"
                                                    MinValue="0" DecimalPlace="2" OnClientBlur="CalcTotal()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="servicetaxpercLabel" runat="server" Text="Service Tax(%)" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <sc:NumericEntry ID="servicetaxpercNE" runat="server" CssClass="CTextBox" Width="200px"
                                                    MaxValue="9999999" MinValue="0" DecimalPlace="2" OnClientBlur="CalcTotal()" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="discountLabel" runat="server" Text="Discount(Less)" CssClass="CLabel"></asp:Label>
                                            </td>
                                            <td>
                                                <sc:NumericEntry ID="discountNE" runat="server" CssClass="CTextBox" Width="200px"
                                                    MaxValue="9999999" MinValue="0" DecimalPlace="2" OnClientBlur="CalcTotal()" />
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
            <td valign="top" style="padding: 10px;">
                <div class="CLabel" style="padding-left: 10px;">
                    <sc:SimpleTab ID="Tab1" runat="server" Width="640px" Height="350px" SelectedTabIndex="0">
                        <TabItemCollection>
                            <sc:TabItem NavigateUrl="PackageChannelsPage.aspx" OnClientClick="return SaveTabPage(this)"
                                Text="Package Channels"></sc:TabItem>
                        </TabItemCollection>
                    </sc:SimpleTab>
                </div>
            </td>
        </tr>
        <tr style="height: 1px">
            <td valign="top">
                <asp:UpdatePanel ID="uplButton" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lbMessage" runat="server" Text="" CssClass="bigHeading"></asp:Label>
                        <br />
                        <asp:Button ID="btnSave" runat="server" OnClientClick="return Validate();" OnClick="btnSave_Click"
                            Text="Save" CssClass="btn1" />
                        &nbsp;;
                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" OnClientClick="return confirm('Do you want to close this window?');"
                            CssClass="btn1" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr style="height: 1px">
            <td style="display: none;">
                <asp:UpdatePanel ID="uplUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function Validate() {
                if (ValidateBlank(document.getElementById('<%=packagenameTextBox.ClientID %>'), 'Package Name') == false)
                    return false;
                return true;
            }
            function CloseWindow(pageClosedHdn) {
                var closedHidden = document.getElementById(pageClosedHdn);
                closedHidden.value = 'true';
                window.returnValue = 1;
                this.close();
                return false;
            }
            var nextTabIndex = '0';
            function SaveChild() {
                var ifr = document.getElementById('DocFrame_<%=Tab1.ClientID%>');
                if (ifr != null) {
                    var btnUpdate = ifr.contentWindow.document.getElementById('btnUpdate');
                    var editMode = ifr.contentWindow.document.getElementById('editModeHdn');
                    if (editMode != null)
                        if (editMode.value == '1') {
                            alert('Please update the current tab data.');
                            return false;
                        }
                if (btnUpdate != null)
                    btnUpdate.click();
            }
        }
        function SaveTabPage(Obj) {
            if (nextTabIndex == Obj.getAttribute('index')) return false;
            nextTabIndex = Obj.getAttribute('index');
            var ifr = document.getElementById('DocFrame_Tab1');
            if (ifr != null) {
                var btnSave = ifr.contentWindow.document.getElementById('btnSave');
                var editMode = ifr.contentWindow.document.getElementById('editModeHdn');
                if (btnSave != null) {
                    btnSave.click();
                    return false;
                }
                else if (editMode != null)
                    if (editMode.value == '1') {
                        alert('Please update the current tab data.');
                        return false;
                    }
        }
        return true;
    }
    function SetTabUrl() {
        if (nextTabIndex != '') {
            Tab1_Obj.setSelectedTab(nextTabIndex);
        }
    }

    function CalcTotal() {
        var basicPrice = parseFloat(document.getElementById('<%=basicpriceNE.ClientID %>').value);
        var addOnPack = parseFloat(document.getElementById('<%=addonpriceNE.ClientID %>').value);
        var entTax = parseFloat(document.getElementById('<%=enttaxNE.ClientID %>').value);
        var serviceTax = parseFloat(document.getElementById('<%=servicetaxpercNE.ClientID %>').value);
        var discount = parseFloat(document.getElementById('<%=discountNE.ClientID %>').value);
        var total = (basicPrice + addOnPack) + (((basicPrice + addOnPack) * serviceTax) / 100) + entTax - discount;
        document.getElementById('<%=totalNE.ClientID %>').value = Math.ceil(total);
    }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
