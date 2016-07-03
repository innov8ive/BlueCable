<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="STBNumberVerify.aspx.cs" Inherits="HMS.STBNumberVerify" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>

        <div id="uploadDiv" runat="server">
            <div>
                <i style="color: red;">Note: STBNo is the mandatory fields.</i>
                <asp:Button ID="btnDownload" runat="server" Text="Download Format" CssClass="btn1"
                    OnClick="btnDownload_Click" />
            </div>
            <table>
                <tr>
                    <td style="width: 100px;">File Name
                    </td>
                    <td>
                        <asp:FileUpload ID="csvFileUpload" runat="server" CssClass="STextBox" onchange="IsFileSelected(this);" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px;">Status
                    </td>
                    <td>
                        <asp:Label ID="uxStatuslb" runat="server" CssClass="CaptionLabel"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan="2">
                        <div id="uxMessagetxt" runat="server" style="width: 800px; height: 200px; overflow: scroll; border: solid 1px;">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btnUpdoad" runat="server" Text="Upload" CssClass="btn1" OnClick="btnUpdoad_Click"
                            OnClientClick="return IsFileSelected();" />
                        <asp:HiddenField ID="hdnFileName" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="resultDiv" runat="server" visible="false">
            <table>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="validRadioButtonList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Valid" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Invalid" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Both (Valid+Invalid)" Value="-1" Selected="true"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="activeRadioButtonList" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Deactive" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Both (Active+Deactive)" Value="-1" Selected="true"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>Outstanding
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOutstanding" runat="server" CssClass="CTextBox" Width="100px">
                            <asp:ListItem Text="0-500" Value="0,500"></asp:ListItem>
                            <asp:ListItem Text="0-1000" Value="0,1000"></asp:ListItem>
                            <asp:ListItem Text="0-1500" Value="0,1500"></asp:ListItem>
                            <asp:ListItem Text="0-2000" Value="0,2000"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                            CssClass="btn1" />
                    </td>
                    <td>
                        <asp:Label ID="lbTotalCustomer" runat="server" Text=""></asp:Label>
                    </td>
                    <td>
                        <asp:Button ID="btnExport" runat="server" Text="Export" 
                            CssClass="btn1" OnClick="btnExport_Click"/>
                    </td>
                </tr>
            </table>
            <sc:ExGrid ID="uxProductGrid" runat="server" AllowPaging="false" AutoGenerateColumns="false"
                Height="203px" ShowFixedHeader="true" AddBufferColumn="false" GridLines="Horizontal"
                ShowHeader="true" Width="820px" FixedCols="2" ShowHiddenCellContentAsTips="true">
                <FooterStyle CssClass="SGridFooter" />
                <RowStyle CssClass="SGridItem" />
                <SelectedRowStyle CssClass="SGridSelectedItem" />
                <HeaderStyle CssClass="SGrid_Header" />
                <Columns>
                    <asp:BoundField HeaderText="Customer Name" DataField="Name" SortExpression="Name" />
                    <asp:BoundField HeaderText="STB No" DataField="STBNo" SortExpression="STBNo" />
                    <asp:TemplateField HeaderText="Valid">
                        <ItemTemplate>
                            <asp:Label ID="lbValid" runat="server" Text='<%#HMS.Common.ToBool(Eval("IsValid"))==true?"Yes":"<span style=\"color:red;\">No</span>" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active">
                        <ItemTemplate>
                            <asp:Label ID="lbAcive" runat="server" Text='<%#HMS.Common.ToBool(Eval("IsActive"))==true?"Yes":"<span style=\"color:red;\">No</span>" %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Outstanding" DataField="Outstanding" SortExpression="Outstanding" DataFormatString="{0:0.00}" />
                </Columns>
            </sc:ExGrid>
        </div>
        <asp:PlaceHolder ID="PleHolder1" runat="server">
            <script language="javascript" type="text/javascript">
                String.prototype.endsWith = function (suffix) {
                    return this.indexOf(suffix, this.length - suffix.length) !== -1;
                }
                function IsFileSelected() {
                    var fileName = document.getElementById('<%=csvFileUpload.ClientID %>').value;
                    if (fileName == '') {
                        alert('Please select data file.');
                        return false;
                    }
                    if (fileName.endsWith('.csv') == false) {
                        alert('Please select .csv file only.');
                        document.getElementById('<%=csvFileUpload.ClientID %>').value = '';
                        return false;
                    }
                    return true;
                }

                function ValidatingData() {
                    document.getElementById('uxMessagetxt').innerHTML = '';
                    document.getElementById('<%=btnUpdoad.ClientID %>').disabled = true;
                    document.getElementById('<%=uxStatuslb.ClientID %>').innerHTML = 'Validating data...';
                    PageMethods.ValidatingData(0,
                function (message) {
                    //                if (message.length > 0) {
                    //                    //Error found after validation
                    //                    document.getElementById('<%=uxStatuslb.ClientID %>').innerHTML = '<span style="color:red">Some error(s) are detected in data file.</span>';
                    //                    document.getElementById('<%=btnUpdoad.ClientID %>').disabled = false;
                    document.getElementById('uxMessagetxt').innerHTML = message;
                    //                    DeleteFile();
                    //                }
                    //                else
                    SavingInDatabase();
                }, onError);
                    return false;
                }
                function SavingInDatabase() {
                    document.getElementById('<%=uxStatuslb.ClientID %>').innerHTML = 'Saving data in database...';
                PageMethods.SavingInDatabase(companyID, function (Obj) { DataSaved(Obj) }, onError);
            }
            function DataSaved(Obj) {
                document.getElementById('<%=uxStatuslb.ClientID %>').innerHTML = Obj;
                document.getElementById('<%=btnUpdoad.ClientID %>').disabled = false;
                DeleteFile();
            }
            function onError(obj) {
                alert(obj);
                document.getElementById('<%=btnUpdoad.ClientID %>').disabled = false;
                DeleteFile();
            }
            function DeleteFile() {
                PageMethods.DeleteFile(function () { });
            }
            </script>
        </asp:PlaceHolder>
    </form>
</body>
</html>
