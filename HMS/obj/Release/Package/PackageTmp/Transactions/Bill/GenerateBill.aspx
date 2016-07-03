<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GenerateBill.aspx.cs" Inherits="HMS.Transactions.Bill.GenerateBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    Bill Date
                </td>
                <td>
                    <sc:DatePicker runat="server" ID="billdateDatePicker" CssClass="CTextBox" Width="200px" />
                </td>
                <td>
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate Bill" CssClass="btn1"
                        OnClick="btnGenerate_Click" />
                </td>
            </tr>
            <tr><td colspan="3">
                <asp:Label ID="lbMessage" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td></tr>
        </table>
    </div>
    </form>
</body>
</html>
