<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CBMainPage.aspx.cs" EnableEventValidation="false"
    Inherits="HMS.CBMainPage" %>

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
                                <td>
                                    <asp:Label ID="loginIDLabel" runat="server" Text="Username" CssClass="CLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="emailIDTextBox" runat="server" CssClass="CTextBox" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnCheckAvailability" runat="server" Text="Check Availability" OnClientClick="return ValidateLoginID()"
                                        OnClick="btnCheckAvailability_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
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
                                    <asp:Label ID="usertypeLabel" runat="server" Text="First Name" CssClass="CLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="CTextBox" Width="200px">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Last Name
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="CTextBox" Width="200px">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Mobile" CssClass="CLabel"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="CTextBox" Width="200px">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top;">
                                   <span class="CLabel">Given Area</span>
                                </td>
                                <td>
                                    <sc:ExCheckedListBox ID="chkAreaList" runat="server" Height="200px" >
                                    </sc:ExCheckedListBox>
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
                        <br />
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn1" />
                        &nbsp;;
                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" OnClientClick="return confirm('Do you want to close this window?');"
                            CssClass="btn1" />
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
                if (document.getElementById('<%=emailIDTextBox.ClientID %>').value == '') {
                    alert('Please enter user name');
                    return false;
                }
                if (document.getElementById('<%=emailIDTextBox.ClientID %>').value.indexOf(' ') > -1) {
                    alert('Blank space not allowed in user name');
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
                var closedHidden = document.getElementById(pageClosedHdn);
                closedHidden.value = 'true';
                window.returnValue = 1;
                this.close();
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
