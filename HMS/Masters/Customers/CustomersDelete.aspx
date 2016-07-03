<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.CustomersDelete"
    CodeBehind="CustomersDelete.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CustomersList </title>
    <script src="../../js/jquery-1.7.2.min.js"></script>
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
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return DeleteCustomers();"
                                            CssClass="btn1" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr style="height: 1px">
                <td>
                    <a href="#" onclick="SelectAll()">Select All </a>
                    <a href="#" onclick="SelectNone()">Select None </a>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <sc:DBList ID="CustomersDBList" runat="server" ItemCSS="SGridItem" SelectedItemCSS="SGridSelectedItem"
                        HeaderItemCSS="SGrid_Header" HoverItemCSS="SGridHoverItem" OnClientRowDblClick="openWin(null,1);"
                        FooterItemCSS="SGridFooter" ValueField="CustomerID" ShowGridLines="false" PageSize="20"
                        DisplayMode="ByPage" AutoGenerateColumns="false" ContextMenuID="CM1" ToolTipCSS="toolTip">
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
                function DeleteCustomers() {
                    var custID = '';
                    $('input:checkbox').each(function (indx, itm) {
                        if (itm.checked == true)
                            custID += ',' + itm.value;
                    });
                    if (custID.length == 0) {
                        alert('Please select customer(s)');
                        return false;
                    }

                    if (confirm('Are you sure want to delete all selected Customers?') == true) {
                        beginReq('Deleting Customers...');
                        PageMethods.DeleteCustomers(custID, function () {
                            endReq();
                            refreshGrid();
                        });
                    }
                    return false;
                }

                function SelectAll() {
                    $('input:checkbox').attr('checked', true);
                }

                function SelectNone() {
                    $('input:checkbox').attr('checked', false);
                }
            </script>
        </asp:PlaceHolder>
    </form>
</body>
</html>
