<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerView.aspx.cs" Inherits="HMS.Masters.Customers.CustomerView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hdnCustomerID" runat="server" />
        <div style="width: 700px; text-align: center; padding: 5px; font-size: 14pt; font-weight: bold;">
            Customer Details
        </div>
        <table style="width: 700px; border-collapse: collapse" border="1">
            <tr>
                <td style="width: 100px;">
                    <b>Unique No.</b>
                </td>
                <td style="width: 100px;">
                    <asp:Label ID="lbCustUNo" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <b>Customer Address</b>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Customer Name</b>
                </td>
                <td>
                    <asp:Label ID="lbCustName" runat="server" Text=""></asp:Label>
                </td>
                <td rowspan="3">
                    <asp:Label ID="lbAddress" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Mobile No.</b>
                </td>
                <td>
                    <asp:Label ID="lbMobileNo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Email ID</b>
                </td>
                <td>
                    <asp:Label ID="lbEmailID" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Status</b>
                </td>
                <td>
                    <asp:Label ID="lbActive" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <div class="bigHeading">
                        <asp:Label ID="lbOutstanding" runat="server" Text="Label"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <div style="width: 700px; text-align: center; padding: 5px; font-size: 14pt; font-weight: bold;">
            Device Details
        </div>
        <div style="height: 100px; overflow: auto;">
            <asp:DataList runat="server" ID="deviceDataList">
                <ItemTemplate>
                    <table style="width: 700px; border-collapse: collapse" border="1">
                        <tr>
                            <td style="width: 100px;">
                                <b>CAN No.</b>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("CANNo")%>
                            </td>
                            <td>
                                <b>STB No.</b>
                            </td>
                            <td>
                                <%#Eval("STBNo")%>
                            </td>
                            <td>
                                <b>Smart Card No.</b>
                            </td>
                            <td>
                                <asp:Label ID="lbSMCNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Service Provider</b>
                            </td>
                            <td>
                                <%#Eval("SPName")%>
                            </td>
                            <td>
                                <b>Type</b>
                            </td>
                            <td>
                                <%#Eval("ConnectionType")%>
                            </td>
                            <td>
                                <b>Package</b>
                            </td>
                            <td>
                                <%#Eval("PackageName")%>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <hr />
                    <table style="width: 700px; border-collapse: collapse" border="1">
                        <tr>
                            <td style="width: 100px;">
                                <b>CAN No.</b>
                            </td>
                            <td style="width: 100px;">
                                <%#Eval("CANNo")%>
                            </td>
                            <td>
                                <b>STB No.</b>
                            </td>
                            <td>
                                <%#Eval("STBNo")%>
                            </td>
                            <td>
                                <b>Smart Card No.</b>
                            </td>
                            <td>
                                <asp:Label ID="lbSMCNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Service Provider</b>
                            </td>
                            <td>
                                <%#Eval("SPName")%>
                            </td>
                            <td>
                                <b>Type</b>
                            </td>
                            <td>
                                <%#Eval("ConnectionType")%>
                            </td>
                            <td>
                                <b>Package</b>
                            </td>
                            <td>
                                <%#Eval("PackageName")%>
                            </td>
                        </tr>
                    </table>
                </AlternatingItemTemplate>
            </asp:DataList>
        </div>
        <div style="width: 700px; text-align: center; padding: 5px; font-size: 14pt; font-weight: bold;">
            Account History
        </div>
        <div style="height: 200px; overflow: auto;">
            <asp:GridView ID="GridView1" Width="700px" runat="server" AutoGenerateColumns="False"
                DataKeyNames="BillID">
                <HeaderStyle CssClass="SGrid_Header"></HeaderStyle>
                <Columns>
                    <asp:BoundField DataField="BillDate" HeaderText="Bill Date" SortExpression="BillDate"
                        DataFormatString="{0:dd-MM-yyyy}" />
                    <asp:BoundField DataField="NetBillAmount" HeaderText="Amount" SortExpression="NetBillAmount" />
                    <asp:BoundField DataField="CollectedAmount" HeaderText="Collection" SortExpression="CollectedAmount" />
                    <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" SortExpression="PaymentDate" DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ReadOnly="True"
                        SortExpression="PaymentMode" />
                    <asp:BoundField DataField="CollectedBy" HeaderText="Collected By" ReadOnly="True"
                        SortExpression="CollectedBy" />
                    <asp:TemplateField HeaderText="View Bill">
                        <ItemTemplate>
                            <a href="#" onclick='ViewBill(<%#Eval("BillID") %>);'>View</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div style="width: 700px; text-align: center; padding: 5px; font-size: 14pt; font-weight: bold;">
            <input type="button" value="Close" class="btn1" onclick="if(confirm('Are you sure want to close?'))window.close();" /><br />
        </div>
    </div>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function ViewBill(billID) {
                var custID = document.getElementById('<%=hdnCustomerID.ClientID %>').value;
                var viewBillUrl = '<%=Page.ResolveClientUrl("~/Transactions/Bill/PrintBill.aspx") %>';
                viewBillUrl = viewBillUrl + '?BillID=' + billID;
                openWindow(viewBillUrl, { width: 669, height: 580 }, null, '');
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
