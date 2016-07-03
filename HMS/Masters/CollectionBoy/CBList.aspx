<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.CBList"
    CodeBehind="CBList.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Collection Boy List </title>
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
                                    <asp:Button ID="btnSearch" runat="server" Text="Refresh" OnClick="btnSearch_Click"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="New" OnClientClick="return NewOperators();"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClientClick="return EditOperators();"
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
                <sc:DBList ID="OperatorsDBList" runat="server" ItemCSS="SGridItem" SelectedItemCSS="SGridSelectedItem"
                    HeaderItemCSS="SGrid_Header" HoverItemCSS="SGridHoverItem" OnClientRowDblClick="openWin(null,1);"
                    FooterItemCSS="SGridFooter" ValueField="UserID" ShowGridLines="false" PageSize="20"
                    DisplayMode="ByPage" AutoGenerateColumns="false" ContextMenuID="CM1" ToolTipCSS="toolTip">
                    <Columns>
                    </Columns>
                </sc:DBList>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnUserID" runat="server" />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            window.onresize = function () { setWidth(); };
            function refreshGrid() {
                var gridObj = eval('<%= OperatorsDBList.ClientObjectID %>');
                gridObj.refresh();
            }
            function setWidth() {
                var gridObj = eval('<%= OperatorsDBList.ClientObjectID %>');
                if (gridObj)
                    gridObj.setContainerWH();
            }
            function EditOperators() {
                var gridObj = eval('<%= OperatorsDBList.ClientObjectID %>');
                document.getElementById('<%=hdnUserID.ClientID %>').value = gridObj.selectedValue;
                var UserID = document.getElementById('<%=hdnUserID.ClientID %>').value;
                if (UserID == '' || UserID == '0' || parseInt(UserID) <= 0) {
                    alert('Please select a row to edit.');
                    return false;
                }
                openWindow('CBMainPage.aspx?UserID=' + UserID, { width: 500, height: 500 }, null, '');
                return false;
            }
            function NewOperators() {
                openWindow('CBMainPage.aspx?UserID=0', { width: 500, height: 500 }, null, '');
                return false;
            }

            function DeleteOperators() {
                var gridObj = eval('<%= OperatorsDBList.ClientObjectID %>');
                document.getElementById('<%=hdnUserID.ClientID %>').value = gridObj.selectedValue;
                var UserID = document.getElementById('<%=hdnUserID.ClientID %>').value;
                if (UserID == '' || UserID == '0' || parseInt(UserID) <= 0) {
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
