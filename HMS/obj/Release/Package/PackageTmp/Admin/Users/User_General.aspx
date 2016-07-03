<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="User_General.aspx.cs" Inherits="HMS.User_General" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-color: #f5f5f5">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="lbdoctor" runat="server" CssClass="CLabel" Text="Operator Name"></asp:Label>
                    </td>
                    <td>
                        <sc:ASTextBox ID="operatorTextBox" runat="server" CssClass="CTextBox" Width="200px"
                            DoPostBack="true" OnTextChanged="operatorTextBox_TextChanged" TextField="Name"
                            ValueField="ID" AllowEditing="false" ASMethodName="HMS.HMSHelper.GetOperators"></sc:ASTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                        <asp:Label ID="nameLabel" runat="server" CssClass="CLabel" Text="Netowrk Name"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNetworkName" runat="server" CssClass="CTextBox" Width="200px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="dobLabel" runat="server" CssClass="CLabel" Text="Address"></asp:Label>
                    </td>
                    <td>
                       <asp:TextBox ID="txtAddress" runat="server" CssClass="CTextBox" Width="200px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="dpartmentLabel" runat="server" CssClass="CLabel" Text="Contact"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtContact" runat="server" CssClass="CTextBox" Width="200px" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
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
                            <asp:Button ID="btnSave" runat="server" Text="Button" OnClick="btnSave_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Button" OnClick="btnUpdate_Click" />
                        </span>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function SaveParent() {
                var btnUpdate = window.parent.document.getElementById('btnUpdate');
                if (btnUpdate != null)
                    btnUpdate.click();
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
