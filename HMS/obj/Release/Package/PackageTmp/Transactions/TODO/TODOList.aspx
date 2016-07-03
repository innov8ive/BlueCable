<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.TODOList"
    CodeBehind="TODOList.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TO DO List </title>
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
                                <td>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="CTextBox">
                                        <asp:ListItem Value="-1" Text="Show All Task(s)"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Pending Task(s)"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Completed Task(s)"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 200px;">
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="New" OnClientClick="return NewTODO();"
                                        CssClass="btn1" />
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClientClick="return EditTODO();"
                                        CssClass="btn1" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return DeleteTODO();"
                                        OnClick="btnDelete_Click" CssClass="btn1" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <sc:DBList ID="TODODBList" runat="server" ItemCSS="SGridItem" SelectedItemCSS="SGridSelectedItem"
                    HeaderItemCSS="SGrid_Header" HoverItemCSS="SGridHoverItem" OnClientRowDblClick="openWin(null,1);"
                    FooterItemCSS="SGridFooter" ValueField="ID" ShowGridLines="false" PageSize="20"
                    DisplayMode="ByPage" AutoGenerateColumns="false" ContextMenuID="CM1" ToolTipCSS="toolTip">
                    <columns>
                    </columns>
                </sc:DBList>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnID" runat="server" />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            window.onresize = function () { setWidth(); };
            function refreshGrid() {
                var gridObj = eval('<%= TODODBList.ClientObjectID %>');
                gridObj.refresh();
            }
            function setWidth() {
                var gridObj = eval('<%= TODODBList.ClientObjectID %>');
                if (gridObj)
                    gridObj.setContainerWH();
            }
            function EditTODO() {
                var gridObj = eval('<%= TODODBList.ClientObjectID %>');
                document.getElementById('<%=hdnID.ClientID %>').value = gridObj.selectedValue;
                var ID = document.getElementById('<%=hdnID.ClientID %>').value;
                if (ID == '' || ID == '0' || parseInt(ID) <= 0) {
                    alert('Please select a row to edit.');
                    return false;
                }
                openWindow('TODOMainPage.aspx?ID=' + ID, { width: 440, height: 280 }, null, '');
                return false;
            }
            function NewTODO() {
                openWindow('TODOMainPage.aspx?ID=0', { width: 440, height: 280 }, null, '');
                return false;
            }

            function DeleteTODO() {
                var gridObj = eval('<%= TODODBList.ClientObjectID %>');
                document.getElementById('<%=hdnID.ClientID %>').value = gridObj.selectedValue;
                var ID = document.getElementById('<%=hdnID.ClientID %>').value;
                if (ID == '' || ID == '0' || parseInt(ID) <= 0) {
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
