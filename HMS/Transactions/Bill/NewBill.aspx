<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewBill.aspx.cs" Inherits="HMS.Transactions.Bill.NewBill" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="font-size:9px;">
    <form id="form1" runat="server">
        <div>
            <table border="1" style="border-collapse: collapse;">
                <tr>
                    <td colspan="4" style="text-align:center">
                        <h3>
                            <asp:Label ID="lbOperatorName" runat="server"></asp:Label>
                        </h3>
                    </td>
                </tr>
                <tr>
                    <td><b>Office Address</b></td>
                    <td colspan="3">
                        <asp:Label ID="lbOfficeAddress" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Contact No.</b></td>
                    <td colspan="3">
                        <asp:Label ID="lbContactNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Ext Tax No.</b></td>
                    <td>
                        <asp:Label ID="lbExtTaxNo" runat="server"></asp:Label>
                    </td>
                    <td><b>STC No.</b></td>
                    <td>
                        <asp:Label ID="lbSTCNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="text-align:center;color:white;" bgcolor="gray">
                        <h4>INVOICE</h4>
                    </td>
                </tr>
                <tr>
                    <td><b>Name</b></td>
                    <td>
                        <asp:Label ID="lbName" runat="server"></asp:Label>
                    </td>
                    <td><b>Uniqiue ID</b></td>
                    <td>
                        <asp:Label ID="lbUniqueID" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td rowspan="3"><b>Address</b></td>
                    <td rowspan="3" style="vertical-align:top;">
                        <asp:Label ID="lbAddress" runat="server"></asp:Label>
                    </td>
                    <td><b>Bill No.</b></td>
                    <td>
                        <asp:Label ID="lbBillNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Bill Period</b></td>
                    <td>
                        <asp:Label ID="lbBillPeriod" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Bill Date</b></td>
                    <td>
                        <asp:Label ID="lbBillDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Contact</b></td>
                    <td>
                        <asp:Label ID="lbContact" runat="server"></asp:Label>
                    </td>
                    <td><b>Due Date</b></td>
                    <td>
                        <asp:Label ID="lbDueDate" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="2"></td>
                    <td><b>Current Plan</b></td>
                    <td>
                        <asp:Label ID="lbPlanName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Basic Rate</b></td>
                    <td>
                        <asp:Label ID="lbBasicRate" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="1" style="border-collapse: collapse;">
                <tr>
                    <td style="width:50%;color:white;" bgcolor="gray">Description</td>
                    <td style="width:50%;color:white;" bgcolor="gray">Rate</td>
                </tr>
                <tr>
                    <td><b>Basic Price</b></td>
                    <td>
                        <asp:Label ID="lbBaicPrice" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Add On Pack</b></td>
                    <td>
                        <asp:Label ID="lbAddOnPack" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Entertainment Tax</b></td>
                    <td>
                        <asp:Label ID="lbEnterTax" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Service Tax 
                        (<asp:Label ID="lbServiceTaxPerc" runat="server"></asp:Label>%)</b></td>
                    <td>
                        <asp:Label ID="lbServiceTax" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Previous Outstanding</b></td>
                    <td>
                        <asp:Label ID="lbPreOutstanding" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Total Amount</b></td>
                    <td>
                        <asp:Label ID="lbTotalAmount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Discount</b></td>
                    <td>
                        <asp:Label ID="lbDiscount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="color:white;" bgcolor="gray"><b>Net Bill Amount</b></td>
                    <td style="color:white;" bgcolor="gray">
                        <asp:Label ID="lbNetBillAmount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <table border="1" style="border-collapse: collapse;">
                <tr>
                    <td colspan="4" style="text-align:center">
                       <h3> <asp:Label ID="lbOperator2" runat="server"></asp:Label></h3>
                        <br />
                        <asp:Label ID="lbOfficeAddress2" runat="server" Font-Bold="true"></asp:Label>
                        <br />
                        Contact No.
                        <asp:Label ID="lbContactNo2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Ent Tax No.</b></td>
                    <td>
                        <asp:Label ID="lbEntTaxNo" runat="server"></asp:Label>
                    </td>
                    <td><b>Service Tax No.</b></td>
                    <td>
                        <asp:Label ID="lbServiceTaxNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Unique No.</b></td>
                    <td>
                        <asp:Label ID="lbUniqueNo" runat="server"></asp:Label>
                    </td>
                    <td><b>Bill No.</b></td>
                    <td>
                        <asp:Label ID="lbBillNo2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Billing Period</b></td>
                    <td>
                        <asp:Label ID="lbBillPeriod2" runat="server"></asp:Label>
                    </td>
                    <td><b>Bill Amount</b></td>
                    <td>
                        <asp:Label ID="lbBillingAmount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Customer Name</b></td>
                    <td colspan="3">
                        <asp:Label ID="lbCustomerName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Address</b></td>
                    <td colspan="3">
                        <asp:Label ID="lbAddress2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Contact</b></td>
                    <td colspan="3">
                        <asp:Label ID="lbContact2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td><b>Payment Mode</b></td>
                    <td colspan="3">Cash / Cheque / DD / Credit Card / Internet Banking
                    </td>
                </tr>
                <tr>
                    <td><b>Cheque No/DD</b></td>
                    <td>&nbsp;</td>
                    <td><b>Dated</b></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><b>Bank Name</b></td>
                    <td>&nbsp;</td>
                    <td><b>Branch</b></td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
