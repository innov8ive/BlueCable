<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HMS.ChangePassword1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Block">
        <ContentTemplate>
          
            <table cellspacing="0" cellpadding="2" style="border: 1px solid black; padding: 20px;">
                <tr style="height: 1px;" id="trOldPass" runat="server">
                    <td valign="top">
                        <asp:Label ID="lboldPass" runat="server" Text="Old Password"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="oldPassTextBox" runat="server" CssClass="CTextBox" Width="200px"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 1px;">
                    <td valign="top" style="width: 140px;">
                        <asp:Label ID="lbnewPassword" runat="server" Text="New Password"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="newPassTextBox" runat="server" CssClass="CTextBox" Width="200px"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 1px;">
                    <td valign="top">
                        <asp:Label ID="lbNewPass2" runat="server" Text="Confirm Password"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="confirmPasswordTextBox" runat="server" CssClass="CTextBox" Width="200px"
                            TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 1px;">
                    <td colspan="2">
                        <asp:Label ID="lbMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Button ID="OKButton" runat="server" Text="Change Password" CssClass="btn1" OnClientClick="return CP();" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
      <script language="javascript" type="text/javascript">
          function CP() {
              if (ValidateCP()) {
                  var oldPass = document.getElementById('<%=oldPassTextBox.ClientID %>').value;
                  PageMethods.ChangePass(document.getElementById('<%=newPassTextBox.ClientID %>').value
                        , oldPass, CPResult);
              }
              return false;
          }
          function CPResult(Obj) {
              document.getElementById('<%=lbMessage.ClientID %>').innerHTML = Obj;
              document.getElementById('<%=oldPassTextBox.ClientID %>').value =
                document.getElementById('<%=newPassTextBox.ClientID %>').value =
                    document.getElementById('<%=confirmPasswordTextBox.ClientID %>').value = '';
          }

          function ValidateCP() {
              if (!ValidateBlank(document.getElementById('<%=oldPassTextBox.ClientID %>'), 'Old Password'))
                  return false;
              if (!ValidateBlank(document.getElementById('<%=newPassTextBox.ClientID %>'), 'New Password'))
                  return false;
              if (!ValidateBlank(document.getElementById('<%=confirmPasswordTextBox.ClientID %>'), 'Confirm Password'))
                  return false;
              if (!ValidatePasswordLength(document.getElementById('<%=newPassTextBox.ClientID %>')))
                  return false;
              if (!ConfirmPass(document.getElementById('<%=newPassTextBox.ClientID %>'), document.getElementById('<%=confirmPasswordTextBox.ClientID %>')))
                  return false;
              return true;
          }
    </script>
    </form>
</body>
</html>
