<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HMS.Login"
    Theme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="margin: 0px; width: 100%; height: 100%; background-color: White;">
    <form id="form1" runat="server" style="margin: 0px; width: 100%; height: 100%;">
    <asp:ScriptManager ID="ScriptManager1" EnableCdn="true" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div style="height: 100px; float: left; width: 100px; padding: 10px;">
        <%--<img src="image/logo.png" alt="logo" style="width:90px;" />--%>
    </div>
    <div style="height: 100px; font-size: 50px; vertical-align: middle; line-height: 100px;">
        Blue Tech Solutions
    </div>
    <div style="background-color: #1B8383; line-height: 30px; color: White; vertical-align: middle;
        padding-left: 30px;">
        <asp:Label ID="dateLabel" runat="server" Text="Label"></asp:Label>
    </div>
    <table>
        <tr>
            <td style="width: 50%;">
                <%--<img src="image/logo_400_300.png" alt="logo" style="width:70%;"/>--%>
            </td>
            <td style="padding-top:20px;padding-right:20px;">
                <div>
                    <table width="470px" cellpadding="1" cellspacing="0" style="border: solid 2px #1B8383;
                        box-shadow: 5px 5px 5px #1B8383;">
                        <tr>
                            <td colspan="3" style="line-height: 30px; color: White; background-color: #1B8383;">
                                <asp:Label ID="loginLabel" runat="server" Text="Login" Font-Bold="True" Font-Size="15pt"></asp:Label>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">
                                <asp:Label ID="loginidLabel" CssClass="CLabel" runat="server" Text="Login ID"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="loginidTextBox" runat="server" CssClass="CTextBox" Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="loginidTextBox"
                                    CssClass="CLabel" ErrorMessage="Please Enter LoginID" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">
                                <asp:Label ID="passwordLabel" CssClass="CLabel" runat="server" Text="Password"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="paswordTextBox" runat="server" CssClass="CTextBox" TextMode="Password"
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="paswordTextBox"
                                    CssClass="CLabel" ErrorMessage="Please Enter Password" ForeColor="Red"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td width="120px">
                            </td>
                            <td width="200px" align="center" style="height: 30px;">
                                <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="CButton" OnClick="btnLogin_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="CButton" CausesValidation="False"
                                    OnClientClick="window.close();" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td width="120px" colspan="3">
                                <asp:Label ID="messageLabel" runat="server" CssClass="CLabel" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr style="background-image: url('images/bgtoolBar.PNG'); background-repeat: repeat-x;
                            line-height: 15px; color: White;">
                            <td width="120px">
                                &nbsp;
                            </td>
                            <td width="200px" align="center">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function openWin() {
            var retval = openWindow('HomePage.aspx', { width: 1024, height: 768 }, 'resizable=yes');
            this.close();
        }
    </script>
    </form>
</body>
</html>
