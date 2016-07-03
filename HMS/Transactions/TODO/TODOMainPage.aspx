<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false"
    Inherits="HMS.TODOMainPage" CodeBehind="TODOMainPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TODO</title>
</head>
<body style="width: 100%; height: 100%;">
    <form id="form1" runat="server" style="width: 100%; height: 100%;">
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; table-layout: fixed; border-collapse: collapse; position: absolute; left: 0px; top: 0px;">
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
                            <table style="width: 100%;" cellpadding="3" cellspacing="0">
                                <tr>
                                    <td colspan="2" class="btn2">
                                        <asp:Label ID="lbMode" runat="server" Text="Mode:New" Font-Bold="True"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 100px;"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="titleLabel" runat="server" Text="Title" CssClass="CLabel"></asp:Label>
                                        <span class="required">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="titleTextBox" CssClass="CTextBox" runat="server" Width="200px" MaxLength="250"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>
                                        <asp:Label ID="duedateLabel" runat="server" Text="Due Date" CssClass="CLabel"></asp:Label>
                                        <span class="required">*</span>
                                    </td>
                                    <td>
                                        <sc:DatePicker ID="duedateDatePicker" runat="server" CssClass="CTextBox" Width="100px" DateFormat="dd-M-yy" Effect="Fade" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align:top;">
                                        <asp:Label ID="detailsLabel" runat="server" Text="Details" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="detailsTextBox" CssClass="CTextBox" TextMode="MultiLine" runat="server" Width="200px" MaxLength="2500"></asp:TextBox>
                                    </td>
                                </tr>
                               

                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr style="height:20px;"><td>&nbsp;</td></tr>
            <tr style="height: 1px">
                <td valign="top">
                    <asp:UpdatePanel ID="uplButton" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" OnClientClick="return Validate();" CssClass="btn1" />
                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close"
                                OnClientClick="return confirm('Do you want to close this window?');" CssClass="btn1" />
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
                    if (window.parent.refreshGrid != null)
                        window.parent.refreshGrid();
                    this.close();
                    return false;
                }
                function Validate() {
                    if (ValidateBlank(document.getElementById('<%=titleTextBox.ClientID %>'), 'Task Title') == false)
                        return false;
                    if (ValidateBlank(document.getElementById('<%=duedateDatePicker.ClientID %>'), 'Task Due Date') == false)
                        return false;

                    return true;
                }

            </script>
        </asp:PlaceHolder>
    </form>
</body>
</html>
