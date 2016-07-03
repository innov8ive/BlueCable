<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Inherits="HMS.PackageChannelsPage"
    CodeBehind="PackageChannelsPage.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PackageChannels</title>
</head>
<meta http-equiv="Page-Enter" content="Alpha(opacity=100)" />
<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.5)" />
<body style="background-color: #f5f5f5; width: 100%; height: 100%;">
    <form id="form1" runat="server" style="width: 100%; height: 100%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="mainBarUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="SGrid">
                <tr style="height: 1px">
                    <td>
                        <sc:ExGrid ID="PackageChannelsExGrid" runat="server" Height="280px" Width="600px"
                            AutoGenerateColumns="False" ToolTipCssClass="toolTip" DataValueField="ChannelID"
                            DataTextField="ChannelID" KeyBoardNavigation="true" OnRowSingleClick="SingleClick();">
                            <FooterStyle CssClass="SGridFooter" />
                            <RowStyle CssClass="SGridItem" />
                            <HeaderStyle CssClass="SGrid_Header" />
                            <SelectedRowStyle CssClass="SGridSelectedItem" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sr No." ItemStyle-Width="100px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Channel Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbChannelID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ChannelName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Language">
                                    <ItemTemplate>
                                        <asp:Label ID="lbLanguage" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Language") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </sc:ExGrid>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:LinkButton ID="addBtnPackageChannels" runat="server" ToolTip="New" Text="Add"
                                        OnClientClick="return addBtnPackageChannels_Click();" />
                                </td>
                                <td>
                                </td>
                                <td>
                                    <asp:LinkButton ID="deleteBtnPackageChannels" runat="server" ToolTip="Delete" Text="Delete"
                                       OnClick="deleteBtnPackageChannels_Click" ForeColor="Red" OnClientClick="return ValidateDelete();" />
                                    <span style="display: none;">
                                        <asp:Button ID="btnSelected" runat="server" Text="Button" OnClick="btnSelected_Click" />
                                    </span>
                                </td>
                                <td>
                                </td>
                                <td>
                                    <span style="display: none;">
                                        <asp:HiddenField ID="editModeHdn" runat="server" />
                                        <asp:HiddenField ID="channelListHdn" runat="server" />
                                        <asp:HiddenField ID="sIndexHdn" runat="server" />
                                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" />
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function selectRow(Obj) {
                Obj.offsetParent.parentNode.className = Obj.checked == true ? 'SGridSelectedItem' : 'SGridItem';
            }
            function SaveParent() {
                var btnUpdate = window.parent.document.getElementById('btnUpdate');
                if (btnUpdate != null)
                    btnUpdate.click();
            }
            function SingleClick(Obj) {
                if (document.getElementById('<%=editModeHdn.ClientID %>').value == '1')
                    return false;
                else {
                    return true;
                }
            }
            function ValidateDelete() {
                var theGrid = eval('_' + '<%=PackageChannelsExGrid.ClientID %>');
                if (theGrid.selectedRow == null) {
                    alert('Please select a record to delete.');
                    return false;
                }
                return confirm('Are you sure to delete?');
            }

            function addBtnPackageChannels_Click() {
                openWindow('ChannelSelector.aspx', { width: 669, height: 580 }, null, '');
                return false;
            }
            function Selected() {
                __doPostBack('btnSelected', '');
            }
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
