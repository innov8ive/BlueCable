<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.MailSettingMainPage"
    CodeBehind="MailSettingMainPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MailSetting</title>
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
                        <table>
                            <tr>
                                <td>
                                    <fieldset>
                                        <legend>
                                            <h3>
                                                Email Setting</h3>
                                        </legend>
                                        <table style="width: 100%;" cellpadding="3" cellspacing="0">
                                            <tr>
                                                <td style="width: 100px;">
                                                    <asp:Label ID="Label1" runat="server" Text="Display Name" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="displayNameTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                        MaxLength="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="emailidLabel" runat="server" Text="Email ID" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="emailidTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                        MaxLength="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="enablesslLabel" runat="server" Text="Enable SSL" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="enablesslCheckBox" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="mailserverLabel" runat="server" Text="Mail Server" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="mailserverTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                        MaxLength="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="passwordLabel" runat="server" Text="Password" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="passwordTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                        MaxLength="50" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Port" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <sc:NumericEntry ID="portTextBox" CssClass="CTextBox" DecimalPlace="0" runat="server"
                                                        MaxValue="9999" Width="200px" MaxLength="4"></sc:NumericEntry>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                                <td style="vertical-align: top;">
                                    <fieldset>
                                        <legend>
                                            <h3>
                                                SMS Setting</h3>
                                        </legend>
                                        <table>
                                            <tr>
                                                <td style="width: 100px;">
                                                    <asp:Label ID="lbAgent" runat="server" Text="SMS Agent URL" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="smsagenturlTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                        MaxLength="200"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbsmsUser" runat="server" Text="SMS User Name" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="smsusernameTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                        MaxLength="100"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lbsmspassword" runat="server" Text="SMS Password" CssClass="CLabel"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="smspasswordTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                                        MaxLength="50" TextMode="Password"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
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
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="lbMessage" runat="server" Text="" CssClass="bigHeading"></asp:Label>
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" OnClientClick="return ValidateSave();" OnClick="btnSave_Click"
                                        Text="Save" CssClass="btn1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                Test Email &nbsp;
                <asp:TextBox ID="txtTestEmail" CssClass="CTextBox" runat="server" Width="200px" MaxLength="250"></asp:TextBox>
                <asp:Button ID="btnSend" runat="server" OnClientClick="return ValidateSend();" OnClick="btnSend_Click"
                    Text="Send Test Email" CssClass="btn1" />
                    <br/>
                <asp:Label ID="lbTestMail" runat="server" Text=""></asp:Label>
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

            function ValidateSend() {
                if (ValidateBlank(document.getElementById('<%=txtTestEmail.ClientID %>'), 'Test Email') == false)
                    return false;
                if (ValidateEmail(document.getElementById('<%=txtTestEmail.ClientID %>'), 'Test Email') == false)
                    return false;
                return true;
            }
            function ValidateSave() {
                if (ValidateBlank(document.getElementById('<%=displayNameTextBox.ClientID %>'), 'Display Email') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=emailidTextBox.ClientID %>'), 'Email') == false)
                    return false;
                if (ValidateEmail(document.getElementById('<%=emailidTextBox.ClientID %>'), 'Email') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=displayNameTextBox.ClientID %>'), 'Display Email') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=passwordTextBox.ClientID %>'), 'Password') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=mailserverTextBox.ClientID %>'), 'Mail Server') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=portTextBox.ClientID %>'), 'Port') == false)
                    return false;
                return true;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
