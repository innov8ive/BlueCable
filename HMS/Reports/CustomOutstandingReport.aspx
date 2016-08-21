<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.CustomOutstandingReport"
    CodeBehind="CustomOutstandingReport.aspx.cs" %>

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
                                    Collection Boy
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCB" runat="server" CssClass="CTextBox" Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Outstanding
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlOutstanding" runat="server" CssClass="CTextBox" Width="100px">
                                        <asp:ListItem Text="(No Range)" Value="0,100000"></asp:ListItem>
                                        <asp:ListItem Text="0-500" Value="0,500"></asp:ListItem>
                                        <asp:ListItem Text="500-1000" Value="500,1000"></asp:ListItem>
                                        <asp:ListItem Text="1000-1500" Value="1000,1500"></asp:ListItem>
                                        <asp:ListItem Text="1500-2000" Value="1500,2000"></asp:ListItem>
                                         <asp:ListItem Text="0-1000" Value="0,1000"></asp:ListItem>
                                        <asp:ListItem Text="0-1500" Value="0,1500"></asp:ListItem>
                                        <asp:ListItem Text="0-2000" Value="0,2000"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    MSO
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlMSO" runat="server" CssClass="CTextBox" Width="100px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="paidRadioButtonList" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Text="Paid" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Non-Paid" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Both" Value="-1" Selected="true"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                        CssClass="btn1" />
                                </td>
                                <td>
                                    <asp:Label ID="lbTotalCustomer" runat="server" Text="" CssClass="btn2"></asp:Label>&nbsp;
                                    <asp:Label ID="lbTotalCollection" runat="server" Text="" CssClass="btn2"></asp:Label>
                                </td>
                                <td>
                                    <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click"
                                        CssClass="btn1" />
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
                    FooterItemCSS="SGridFooter" ValueField="C.CustomerID" ShowGridLines="false" PageSize="20" ErrorThresold="5000"
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
           
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
