<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.OperatorsMainPage"
    CodeBehind="OperatorsMainPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Operators</title>
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
                        <table style="width: 100%;" cellpadding="3" cellspacing="0">
                            <tr>
                                <td colspan="2">
                                    <span class="required">Note: start(*) marked field(s) are manedatory.</span>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class="btn2">
                                    <asp:Label ID="lbMode" runat="server" Text="Mode:New" Font-Bold="True"></asp:Label>
                                    <span style="float: right;">
                                        <asp:CheckBox ID="isactiveCheckBox" runat="server" Text="Active" /></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px;">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="operatornameLabel" runat="server" Text="Operator Name" CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="operatornameTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="networknameLabel" runat="server" Text="Network Name" CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="networknameTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Short Name" CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="shortNameTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="3"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="addressLabel" runat="server" Text="Address" CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="addressTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="250"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="contactLabel" runat="server" Text="Contact" CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="contactTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="emailidLabel" runat="server" Text="EmailID" CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="emailidTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="pancardnoLabel" runat="server" Text="PAN Card No." CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="pancardnoTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="15"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="postallicencenoLabel" runat="server" Text="Postal Licence No." CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="postallicencenoTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="servicetaxnoLabel" runat="server" Text="Service Tax No." CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="servicetaxnoTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="interainmenttaxnoLabel" runat="server" Text="Entertainment Tax No."
                                        CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="interainmenttaxnoTextBox" CssClass="CTextBox" runat="server" Width="200px"
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Registration Date" CssClass="CLabel"></asp:Label>
                                    <span class="required">*</span>
                                </td>
                                <td>
                                    <sc:DatePicker ID="txtRegDate" CssClass="CTextBox" runat="server" Width="200px"></sc:DatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lbMessage" runat="server" Text="" CssClass="heading"></asp:Label>
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
                        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="btn1"
                            OnClientClick="return Validate();" />
                        <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" OnClientClick="return confirm('Do you want to close this window?');"
                            CssClass="btn1" />
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

            function Validate() {
                if (ValidateBlank(document.getElementById('<%=operatornameTextBox.ClientID %>'), 'Operator Name') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=shortNameTextBox.ClientID %>'), 'Short Name') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=networknameTextBox.ClientID %>'), 'Network Name') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=addressTextBox.ClientID %>'), 'Address') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=contactTextBox.ClientID %>'), 'Contact No.') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=emailidTextBox.ClientID %>'), 'EmailID') == false)
                    return false;
                if (ValidateEmail(document.getElementById('<%=emailidTextBox.ClientID %>')) == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=pancardnoTextBox.ClientID %>'), 'PAN No.') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=servicetaxnoTextBox.ClientID %>'), 'Service TAX No.') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=postallicencenoTextBox.ClientID %>'), 'Postal Card No.') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=interainmenttaxnoTextBox.ClientID %>'), 'Entertainment TAX No.') == false)
                    return false;
                if (ValidateBlank(document.getElementById('<%=txtRegDate.ClientID %>'), 'Registration Date') == false)
                    return false;
                return true;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
