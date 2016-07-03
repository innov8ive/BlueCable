<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.PendingBillList"
    CodeBehind="PendingBillList.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CustomersList </title>
</head>
<body style="margin: 0px; padding: 0px; width: 100%; height: 100%;">
    <form id="form1" runat="server" style="width: 100%; height: 100%; margin: 0px;">
    <table style="width: 100%; height: 100%;" cellpadding="0" cellspacing="0" border="0">
        <tr style="height: 1px">
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr style="height: 1px">
            <td>
                <asp:UpdatePanel ID="filterUpdatePanel" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table>
                            <tr>
                                <td style="width: 70px;">
                                    Unique ID
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtUID" CssClass="CTextBox" Width="150px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnView" runat="server" Text="View" OnClientClick="return ViewBill();"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Print" OnClientClick="return PrintBill();"
                                        CssClass="btn1" />
                                </td>
                                 <td>
                                    <asp:Button ID="Button2" runat="server" Text="Customer" OnClientClick="return ViewCustomer();"
                                        CssClass="btn1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <sc:DBList ID="CustomersDBList" runat="server" ItemCSS="SGridItem" SelectedItemCSS="SGridSelectedItem"
                    HeaderItemCSS="SGrid_Header" HoverItemCSS="SGridHoverItem" OnClientRowDblClick=""
                    FooterItemCSS="SGridFooter" ValueField="LB.BillID" TextField="LB.CustomerID" ShowGridLines="false"
                    PageSize="20" DisplayMode="ByPage" AutoGenerateColumns="false" ContextMenuID="CM1"
                    ToolTipCSS="toolTip">
                    <Columns>
                    </Columns>
                </sc:DBList>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnCustomerID" runat="server" />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            window.onresize = function () { setWidth(); };
            function refreshGrid() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                gridObj.refresh();
            }
            function setWidth() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                if (gridObj)
                    gridObj.setContainerWH();
            }
            function ViewBill() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                document.getElementById('<%=hdnCustomerID.ClientID %>').value = gridObj.selectedValue;
                var CustomerID = document.getElementById('<%=hdnCustomerID.ClientID %>').value;
                if (CustomerID == '' || CustomerID == '0' || parseInt(CustomerID) <= 0) {
                    alert('Please select a row to view bill.');
                    return false;
                }
                
                openWindow('ViewBill.aspx?BillID=' + CustomerID, { width: 680, height: 430 }, null, '');
                return false;
            }
            function PrintBill() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                document.getElementById('<%=hdnCustomerID.ClientID %>').value = gridObj.selectedValue;
                var CustomerID = document.getElementById('<%=hdnCustomerID.ClientID %>').value;
                if (CustomerID == '' || CustomerID == '0' || parseInt(CustomerID) <= 0) {
                    alert('Please select a row to print bill.');
                    return false;
                }
                //var url = 'http://docs.google.com/gview?embedded=true&url=';
                //url+=window.location.href.replace('PendingBillList.aspx','').replace('?formID=11','')+'PrintBill.aspx?BillID=' + CustomerID;
                //openWindow(url, { width: 820, height: 580 }, null, '');
                openWindow('PrintBill.aspx?BillID=' + CustomerID, { width: 820, height: 580 }, null, '');
                return false;
            }
            
            function ViewCustomer() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                document.getElementById('<%=hdnCustomerID.ClientID %>').value = gridObj.selectedText;
                var CustomerID = document.getElementById('<%=hdnCustomerID.ClientID %>').value;
                if (CustomerID == '' || CustomerID == '0' || parseInt(CustomerID) <= 0) {
                    alert('Please select a row to view customer details.');
                    return false;
                }
                var viewUrl = '<%=Page.ResolveClientUrl("~/Masters/Customers/CustomerView.aspx") %>';
                openWindow(viewUrl + '?CustomerID=' + CustomerID, { width: 820, height: 600 }, null, '');
                return false;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
