<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.SettingMainPage"
    CodeBehind="SettingMainPage.aspx.cs" %>

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
                        <fieldset style="width:50%">
                            <legend>
                                <h3>
                                    Other Settings</h3>
                            </legend>
                            <table cellpadding="3" cellspacing="0">
                                <tr style="display:none;">
                                    <td style="width: 130px;">
                                        <asp:Label ID="Label1" runat="server" Text="Entertainment Tax" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <sc:NumericEntry ID="entTaxNE" CssClass="CTextBox" runat="server" Width="100px" DecimalPlace="0"
                                            MaxLength="3"></sc:NumericEntry>
                                    </td>
                                </tr>
                                <tr style="display:none;">
                                    <td>
                                        <asp:Label ID="emailidLabel" runat="server" Text="Service Tax(%)" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <sc:NumericEntry ID="serviceTaxNE" CssClass="CTextBox" runat="server" Width="100px"
                                            DecimalPlace="2" MaxValue="99"></sc:NumericEntry>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="mailserverLabel" runat="server" Text="Print Bill Url" CssClass="CLabel"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="printBillUrlTextBox" CssClass="CTextBox" runat="server" Width="400px"
                                            MaxLength="100"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
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

            function ValidateSave() {
                if (ValidateBlank(document.getElementById('<%=printBillUrlTextBox.ClientID %>'), 'Print Bill Url') == false)
                    return false;
                return true;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
