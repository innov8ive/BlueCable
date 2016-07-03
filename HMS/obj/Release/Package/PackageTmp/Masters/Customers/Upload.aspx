<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="HMS.Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div>
        <i style="color: red;">Note: FirstName, City, State, PinCode, ConnectionType, CustomerStatus, ServiceProvider, Outstanding, Package,Area
            are mandatory fields.</i>
        <asp:Button ID="btnDownload" runat="server" Text="Download Format" CssClass="btn1"
            OnClick="btnDownload_Click" />
    </div>
    <div>
        <table>
            <tr>
                <td style="width: 100px;">
                    File Name
                </td>
                <td>
                    <asp:FileUpload ID="csvFileUpload" runat="server" CssClass="STextBox" onchange="IsFileSelected(this);" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px;">
                    Status
                </td>
                <td>
                    <asp:Label ID="uxStatuslb" runat="server" CssClass="CaptionLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td colspan="2">
                    <div id="uxMessagetxt" runat="server" style="width: 800px; height: 200px; overflow: scroll;
                        border: solid 1px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnUpdoad" runat="server" Text="Upload" CssClass="btn1" OnClick="btnUpdoad_Click"
                        OnClientClick="return IsFileSelected();" />
                    <asp:HiddenField ID="hdnFileName" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <sc:ExGrid ID="uxProductGrid" runat="server" AllowPaging="false" AutoGenerateColumns="true"
            Height="203px" ShowFixedHeader="true" AddBufferColumn="false" GridLines="Horizontal"
            ShowHeader="true" Width="820px" FixedCols="2" ShowHiddenCellContentAsTips="true">
            <FooterStyle CssClass="SGridFooter" />
            <RowStyle CssClass="SGridItem" />
            <SelectedRowStyle CssClass="SGridSelectedItem" />
            <HeaderStyle CssClass="SGrid_Header" />
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
