<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.PackagesList"
    CodeBehind="PackagesList.aspx.cs" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PackagesList </title>
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
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Refresh" OnClick="btnSearch_Click"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="New" OnClientClick="return NewPackages();"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClientClick="return EditPackages();"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClientClick="return DeletePackages();"
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
                <sc:DBList ID="PackagesDBList" runat="server" ItemCSS="SGridItem" SelectedItemCSS="SGridSelectedItem"
                    HeaderItemCSS="SGrid_Header" HoverItemCSS="SGridHoverItem" OnClientRowDblClick="openWin(null,1);"
                    FooterItemCSS="SGridFooter" ValueField="PackageID" ShowGridLines="false" PageSize="20"
                    DisplayMode="ByPage" AutoGenerateColumns="false" ContextMenuID="CM1" ToolTipCSS="toolTip">
                    <Columns>
                    </Columns>
                </sc:DBList>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnPackageID" runat="server" />
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            window.onresize = function () { setWidth(); };
            function refreshGrid() {
                var gridObj = eval('<%= PackagesDBList.ClientObjectID %>');
                gridObj.refresh();
            }
            function setWidth() {
                var gridObj = eval('<%= PackagesDBList.ClientObjectID %>');
                if (gridObj)
                    gridObj.setContainerWH();
            }
            function EditPackages() {
                var gridObj = eval('<%= PackagesDBList.ClientObjectID %>');
                document.getElementById('<%=hdnPackageID.ClientID %>').value = gridObj.selectedValue;
                var PackageID = document.getElementById('<%=hdnPackageID.ClientID %>').value;
                if (PackageID == '' || PackageID == '0' || parseInt(PackageID) <= 0) {
                    alert('Please select a row to edit.');
                    return false;
                }
                openWindow('PackagesMainPage.aspx?PackageID=' + PackageID, { width: 669, height: 600 }, null, '');
                return false;
            }
            function NewPackages() {
                openWindow('PackagesMainPage.aspx?PackageID=0', { width: 669, height: 600 }, null, '');
                return false;
            }

            function DeletePackages() {
                var gridObj = eval('<%= PackagesDBList.ClientObjectID %>');
                document.getElementById('<%=hdnPackageID.ClientID %>').value = gridObj.selectedValue;
                var PackageID = document.getElementById('<%=hdnPackageID.ClientID %>').value;
                if (PackageID == '' || PackageID == '0' || parseInt(PackageID) <= 0) {
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
