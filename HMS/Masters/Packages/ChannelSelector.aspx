<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelSelector.aspx.cs"
    Inherits="HMS.Masters.Packages.ChannelSelector" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr style="display:none;">
                <td>
                    Language:
                    <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLanguage_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td><h2>Select Channel(s)</h2></td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCheckAll" runat="server" OnCheckedChanged="chkCheckAll_CheckedChanged" Text="Check All" AutoPostBack="true"/>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="height: 400px; overflow: auto;">
                        <asp:CheckBoxList ID="chkChannelList" runat="server" Width="300px">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSelect" runat="server" Text="Confirm" CssClass="btn1" OnClick="btnSelect_Click" />
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        function Selected(chList) {
            var parentWin = window.opener;
            var channelListHdn = parentWin.document.getElementById('channelListHdn');
            channelListHdn.value = chList;
            parentWin.Selected();
            window.close();
        }
    </script>
    </form>
</body>
</html>
