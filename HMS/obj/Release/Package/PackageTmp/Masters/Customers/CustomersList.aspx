<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.CustomersList"
    CodeBehind="CustomersList.aspx.cs" %>

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
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="CTextBox" Width="150px"></asp:TextBox>
                                </td>
                                <td style="width: 200px;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="New" OnClientClick="return NewCustomers();"
                                        CssClass="btn1" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClientClick="return EditCustomers();"
                                        CssClass="btn1" />
                                    <asp:Button ID="Button1" runat="server" Text="View" OnClientClick="return ViewCustomers();"
                                        CssClass="btn1" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return DeleteCustomers();"
                                        OnClick="btnDelete_Click" CssClass="btn1" />
                                     <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" CssClass="btn1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnExport" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <sc:DBList ID="CustomersDBList" runat="server" ItemCSS="SGridItem" SelectedItemCSS="SGridSelectedItem"
                    HeaderItemCSS="SGrid_Header" HoverItemCSS="SGridHoverItem" OnClientRowDblClick="openWin(null,1);"
                    FooterItemCSS="SGridFooter" ValueField="CustomerID" ShowGridLines="false" PageSize="20"
                    DisplayMode="ByPage" AutoGenerateColumns="false" ContextMenuID="CM1" ToolTipCSS="toolTip">
                    <columns>
                    </columns>
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
            function EditCustomers() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                document.getElementById('<%=hdnCustomerID.ClientID %>').value = gridObj.selectedValue;
                var CustomerID = document.getElementById('<%=hdnCustomerID.ClientID %>').value;
                if (CustomerID == '' || CustomerID == '0' || parseInt(CustomerID) <= 0) {
                    alert('Please select a row to edit.');
                    return false;
                }
                openWindow('CustomersMainPage.aspx?CustomerID=' + CustomerID, { width: 820, height: 580 }, null, '');
                return false;
            }
            function ViewCustomers() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                document.getElementById('<%=hdnCustomerID.ClientID %>').value = gridObj.selectedValue;
                var CustomerID = document.getElementById('<%=hdnCustomerID.ClientID %>').value;
                if (CustomerID == '' || CustomerID == '0' || parseInt(CustomerID) <= 0) {
                    alert('Please select a row to view.');
                    return false;
                }
                openWindow('CustomerView.aspx?CustomerID=' + CustomerID, { width: 820, height: 600 }, null, '');
                return false;
            }
            function NewCustomers() {
                openWindow('CustomersMainPage.aspx?CustomerID=0', { width: 820, height: 580 }, null, '');
                return false;
            }

            function DeleteCustomers() {
                var gridObj = eval('<%= CustomersDBList.ClientObjectID %>');
                document.getElementById('<%=hdnCustomerID.ClientID %>').value = gridObj.selectedValue;
                var CustomerID = document.getElementById('<%=hdnCustomerID.ClientID %>').value;
                if (CustomerID == '' || CustomerID == '0' || parseInt(CustomerID) <= 0) {
                    alert('Please select a row to delete.');
                    return false;
                }
                if (confirm('Do you want to delete this row?')) {
                    Obj.selectedValue = '0';
                    return true;
                }
                else
                    return false;
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
