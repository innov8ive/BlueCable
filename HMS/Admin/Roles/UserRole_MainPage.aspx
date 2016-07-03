<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRole_MainPage.aspx.cs"
    Inherits="HMS.UserRole_MainPage" Theme="Theme1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <asp:UpdatePanel ID="uplButton" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" OnClientClick="return confirm('Do you want to close this window?');"
                                        CssClass="btn1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td style="padding: 10px;">
                <asp:UpdatePanel ID="uplMain" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td colspan="2" class="btn2">
                                    <asp:Label ID="lbMode" runat="server" Text="Mode:New" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Role Name
                                </td>
                                <td>
                                    <asp:TextBox ID="roleNameTextBox" runat="server" CssClass="CTextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <div style="padding-left: 10px;">
                    <sc:SimpleTab ID="Tab1" runat="server" Width="550px" Height="450px" SelectedTabIndex="0">
                        <TabItemCollection>
                            <sc:TabItem NavigateUrl="UserRole_Perms.aspx" OnClientClick="" Text="Permissions">
                            </sc:TabItem>
                        </TabItemCollection>
                    </sc:SimpleTab>
                </div>
                <asp:UpdatePanel ID="uplUpdate" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <span style="display: none;">
                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" /></span>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function SaveChild() {
                var ifr = document.getElementById('DocFrame_Tab1');
                if (ifr != null) {
                    var btnUpdate = ifr.contentWindow.document.getElementById('btnSave');
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
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
