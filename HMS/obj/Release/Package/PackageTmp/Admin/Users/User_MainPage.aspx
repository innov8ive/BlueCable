<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_MainPage.aspx.cs"
    EnableEventValidation="false" Inherits="HMS.User_MainPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
    <title></title>
</head>
<body style="margin: 0px; width: 100%; height: 100%;">
    <form id="form1" runat="server" style="width: 100%; height: 100%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr style="height: 1px">
            <td valign="top">
               
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="uplMain" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="4" class="btn2">
                                    <asp:Label ID="lbMode" runat="server" Text="Mode:New" Font-Bold="True"></asp:Label>
                                    <span style="float: right;">
                                        <asp:CheckBox ID="isactiveCheckBox" runat="server" Text="Active" /></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 143px">
                                    <asp:Label ID="loginIDLabel" runat="server" Text="Username" CssClass="CLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="emailIDTextBox" runat="server" CssClass="CTextBox" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnCheckAvailability" runat="server" Text="Check Availability" OnClientClick="return ValidateLoginID()"
                                        OnClick="btnCheckAvailability_Click" />
                                </td>
                                <td>
                                    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="passwordLabel" runat="server" Text="Password" CssClass="CLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="passwordTextBox" runat="server" CssClass="CTextBox" ReadOnly="true"
                                        Text="HMS" Width="200px"></asp:TextBox>
                                    <a id="resetLink" runat="server" class="CLinkButtonBlue" href="#" onclick="changePasswordWin.Open();">
                                        Reset Password</a>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="usertypeLabel" runat="server" Text="User Type" CssClass="CLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="userTypeDropDownList" runat="server" CssClass="CTextBox" Width="205px">
                                        <asp:ListItem Text="Cable Operator" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Administrator" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td style="width: 143px">
                                    <asp:Label ID="lbdoctor" runat="server" CssClass="CLabel" Text="Operator Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="operatorTextBox" runat="server" CssClass="CTextBox" Width="200px"
                                        AutoPostBack="true" OnSelectedIndexChanged="operatorTextBox_TextChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    <asp:Label ID="nameLabel" runat="server" CssClass="CLabel" Text="Netowrk Name"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNetworkName" runat="server" CssClass="CTextBox" Width="200px"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    <asp:Label ID="dobLabel" runat="server" CssClass="CLabel" Text="Address"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="CTextBox" Width="200px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    <asp:Label ID="dpartmentLabel" runat="server" CssClass="CLabel" Text="Contact"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContact" runat="server" CssClass="CTextBox" Width="200px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="CLabel" Text="PAN No."></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPANNo" runat="server" CssClass="CTextBox" Width="200px" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <span style="display: none;">
                                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="btnSave_Click" />
                                        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="btnUpdate_Click" />
                                    </span>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="text-align: center;">
                <asp:UpdatePanel ID="uplButton" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Label ID="lbMessage" runat="server" Text="" CssClass="bigHeading"></asp:Label>
                        <br />
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn1" />
                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" OnClientClick="return confirm('Do you want to close this window?');"
                            CssClass="btn1" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="uplUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <span style="display: none;">
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" /></span>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <sc:SWindow ID="changePasswordWin" runat="server" Height="200px" Width="370px" Title="Reset Password">
        <table>
            <tr>
                <td>
                    <asp:Label ID="NewPassLabel" runat="server" Text="New Password" CssClass="CLabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="newPassTextBox" runat="server" CssClass="CTextBox" Width="200px"
                        TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="confirmPassLabel" runat="server" Text="Confirm Password" CssClass="CLabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="confirmPassTextBox" runat="server" CssClass="CTextBox" Width="200px"
                        TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnChangeClose" runat="server" Text="Change and Close" CssClass="CButton"
                        OnClientClick="return ValidatePassword()" OnClick="btnChangeClose_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="CButton" OnClientClick="changePasswordWin.Close();return false;" />
                </td>
            </tr>
        </table>
    </sc:SWindow>
    <asp:PlaceHolder ID="PleHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            var tabIndex = '0';

            function ValidateLoginID() {
                if (document.getElementById('loginIDTextBox').value == '') {
                    alert('Please enter Login ID');
                    return false;
                }
                return true;
            }
            function ValidatePassword() {
                if (document.getElementById('<%=newPassTextBox.ClientID %>').value == '') {
                    alert('Password can not be blank');
                    return false;
                }
                else if (document.getElementById('<%=newPassTextBox.ClientID %>').value != document.getElementById('<%=confirmPassTextBox.ClientID %>').value) {
                    alert('Confirm password not matched');
                    return false;
                }
                changePasswordWin.Close();
                return true;
            }
            function setTabPage() {
                if (tabIndex != '' && Tab1_Obj != null) {
                    Tab1_Obj.setSelectedTab(tabIndex);
                }
            }

            function SaveChild() {
                var ifr = document.getElementById('DocFrame_Tab1');
                if (ifr != null) {
                    var btnUpdate = ifr.contentWindow.document.getElementById('btnUpdate');
                    if (btnUpdate != null)
                        btnUpdate.click();
                }
            }

            function CloseWindow(pageClosedHdn) {
                document.getElementById('DocFrame_Tab1').src = '';
                var closedHidden = document.getElementById(pageClosedHdn);
                closedHidden.value = 'true';
                window.opener.refreshGrid();
                window.setTimeout(function () { this.close(); }, 200);
                return false;
            }
            function SaveTabPage(Obj) {
                if (Obj.getAttribute('index') == tabIndex) return false;
                tabIndex = Obj.getAttribute('index');
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
                            MessageBox.showDialog('Please update the current tab data.');
                            return false;
                        }
                }
                return true;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
