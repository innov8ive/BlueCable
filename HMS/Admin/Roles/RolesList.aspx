<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.RolesList"
    CodeBehind="RolesList.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RolesList </title>
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
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        CssClass="btn3" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="Add New" OnClientClick="return NewRoles();"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClientClick="return EditRoles();"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return DeleteRoles();"
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
                <sc:DBList ID="RolesDBList" runat="server" ItemCSS="SGridItem" SelectedItemCSS="SGridSelectedItem"
                    HeaderItemCSS="SGrid_Header" HoverItemCSS="SGridHoverItem" OnClientRowDblClick="openWin(null,1);"
                    FooterItemCSS="SGridFooter" ValueField="RoleID" ShowGridLines="false" PageSize="20"
                    DisplayMode="ByPage" AutoGenerateColumns="false" ContextMenuID="CM1" ToolTipCSS="toolTip">
                    <Columns>
                    </Columns>
                </sc:DBList>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnRoleID" runat="server" />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            window.onresize = function () { setWidth(); };
            function refreshGrid() {
                var gridObj = eval('<%= RolesDBList.ClientObjectID %>');
                gridObj.refresh();
            }
            function setWidth() {
                var gridObj = eval('<%= RolesDBList.ClientObjectID %>');
                if (gridObj)
                    gridObj.setContainerWH();
            }
            function EditRoles() {
                var gridObj = eval('<%= RolesDBList.ClientObjectID %>');
                document.getElementById('<%=hdnRoleID.ClientID %>').value = gridObj.selectedValue;
                var RoleID = document.getElementById('<%=hdnRoleID.ClientID %>').value;
                if (RoleID == '' || RoleID == '0' || parseInt(RoleID) <= 0) {
                    alert('Please select a row to edit.');
                    return false;
                }
                openWindow('UserRole_MainPage.aspx?RoleID=' + RoleID, { width: 669, height: 580 }, null, '');
                return false;
            }
            function NewRoles() {
                openWindow('UserRole_MainPage.aspx?RoleID=0', { width: 669, height: 580 }, null, '');
                return false;
            }

            function DeleteRoles() {
                var gridObj = eval('<%= RolesDBList.ClientObjectID %>');
                document.getElementById('<%=hdnRoleID.ClientID %>').value = gridObj.selectedValue;
                var RoleID = document.getElementById('<%=hdnRoleID.ClientID %>').value;
                if (RoleID == '' || RoleID == '0' || parseInt(RoleID) <= 0) {
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
