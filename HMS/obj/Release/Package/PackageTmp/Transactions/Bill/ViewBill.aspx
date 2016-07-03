<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBill.aspx.cs" Inherits="HMS.Transactions.Bill.ViewBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Bill</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-left: 20px;">
        <asp:HiddenField ID="hdnBillID" runat="server" />
        <asp:HiddenField ID="hdnServiceTaxPerc" runat="server" />
        <table style="width: 600px; border-collapse: collapse" border="1">
            <tr>
                <td>
                    <asp:Label ID="lbBillNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbBillMonth" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbBillDate" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbCustName" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lbCustAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lbCANNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbSTBNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbSMCNo" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr class="SGrid_Header">
                <td colspan="2">
                    <asp:Label ID="Label1" runat="server" Text="Discription" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Amount" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label3" runat="server" Text="Basic Price" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbBasicPrice" runat="server" Text="" CssClass="CTextBox" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label4" runat="server" Text="Add On Pack" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <sc:NumericEntry ID="txtAddOnPrice" runat="server" Text="" CssClass="CTextBox" Width="150px" MaxLength="6"  OnClientBlur="CalcTotal()"></sc:NumericEntry>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label5" runat="server" Text="Entertainment Tax" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbEntTax" runat="server" Text="" CssClass="CTextBox" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lbServiceTaxPerc" runat="server" Text="" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbServiceTax" runat="server" Text="" CssClass="CTextBox" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label6" runat="server" Text="Previous Outstanding" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbOutstanding" runat="server" Text="" CssClass="CTextBox" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label9" runat="server" Text="Total" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbTotal" runat="server" Text="" CssClass="CTextBox" Width="150px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" Text="Discount" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <sc:NumericEntry ID="txtDiscount" runat="server" Text="" CssClass="CTextBox" MaxLength="6" Width="150px" OnClientBlur="CalcTotal()"></sc:NumericEntry>
                    <asp:Button ID="btnUpdateDiscount" runat="server" Text="Update Discount/Addon" CssClass="btn1"
                        OnClick="btnUpdateDiscount_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label8" runat="server" Text="Net Bill Amount" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lbNetBillAmount" runat="server" Font-Bold="True" CssClass="CTextBox"
                        Width="150px"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="width: 600px; border-collapse: collapse" border="0">
            <tr>
                <td colspan="6">
                    <asp:Label ID="Label10" runat="server" Text="Payment Details" Font-Bold="True" Font-Size="14px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Payment Mode:
                </td>
                <td>
                    <%--AutoPostBack="true"   OnSelectedIndexChanged="ddlPaymentMode_SelectedIndexChanged"--%>
                    <asp:DropDownList ID="ddlPaymentMode" runat="server" Width="100px" CssClass="CTextBox"
                        onchange="paymentModeChanged(this);">
                        <asp:ListItem Value="ByCash">By Cash</asp:ListItem>
                        <asp:ListItem Value="ByCheque">By Cheque</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    Amount:
                </td>
                <td>
                    <sc:NumericEntry runat="server" ID="amountNE" Width="100px" CssClass="CTextBox"></sc:NumericEntry>
                </td>
                <td>
                    Cheque No.:
                </td>
                <td>
                    <asp:TextBox ID="txtChequeNo" runat="server" Width="100px" Enabled="false" CssClass="CTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Cheque Date:
                </td>
                <td>
                    <sc:DatePicker ID="txtChequeDate" runat="server" Width="100px" Enabled="false" CssClass="CTextBox"></sc:DatePicker>
                </td>
                <td>
                    Bank Name:
                </td>
                <td>
                    <asp:TextBox ID="txtBankName" runat="server" Width="100px" Enabled="false" CssClass="CTextBox"></asp:TextBox>
                </td>
                <td>
                    Branch Name:
                </td>
                <td>
                    <asp:TextBox ID="txtBranchName" runat="server" Width="100px" Enabled="false" CssClass="CTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Remarks:
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="100px" CssClass="CTextBox"></asp:TextBox>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="btnMakePayment" runat="server" Text="Make Payment" CssClass="btn1"
                        OnClick="btnMakePayment_Click" />
                    &nbsp;
                    <input type="button" value="Close" onclick="if(confirm('Are you sure want to close?'))CloseWindow();"
                        class="btn1" />
                </td>
                <td colspan="4">
                    <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function CloseWindow() {
                window.opener.refreshGrid();
                window.setTimeout(function () { this.close(); }, 200);
                return false;
            }

            function CalcTotal() {
                var basic = parseFloatEx(document.getElementById('<%=lbBasicPrice.ClientID %>').innerHTML);
                var entTax = parseFloatEx(document.getElementById('<%=lbEntTax.ClientID %>').innerHTML);
                var serviceTaxPerc = parseFloatEx(document.getElementById('<%=hdnServiceTaxPerc.ClientID %>').value);
                var addon = parseFloatEx(document.getElementById('<%=txtAddOnPrice.ClientID %>').value);
                var outstanding = parseFloatEx(document.getElementById('<%=lbOutstanding.ClientID %>').innerHTML);

                var temp_total = basic + addon;
                temp_total = temp_total + (temp_total * serviceTaxPerc) / 100;
                temp_total = temp_total + outstanding + entTax - parseFloatEx(document.getElementById('<%=txtDiscount.ClientID %>').value);
                document.getElementById('<%=lbNetBillAmount.ClientID %>').innerHTML = Math.ceiling(temp_total);
            }

            function parseFloatEx(val) {
                if (val == null || val == '')
                    return 0;
                return parseFloat(val);
            }
            
            function paymentModeChanged(Obj) {
                var dis = true;
                if (Obj.value == 'ByCash') {
                    dis = true;
                }
                else {
                    dis = false;
                }

                document.getElementById('<%=txtBankName.ClientID %>').disabled =
                document.getElementById('<%=txtBranchName.ClientID %>').disabled =
                    document.getElementById('<%=txtChequeDate.ClientID %>').disabled =
                        document.getElementById('<%=txtChequeNo.ClientID %>').disabled = dis;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
