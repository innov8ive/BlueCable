<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PrintBillForMail.aspx.cs"
    Inherits="HMS.PrintBillForMail" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Print Bill</title>
</head>
<body style="margin: 0px; padding: 0px; width: 100%; height: 100%; background-color: White;overflow:auto;">
    <form id="form1" runat="server" style="width: 100%; height: 100%; margin: 0px;">
    <table style="width: 100%; height: 100%;" cellpadding="0" cellspacing="0" border="0">
        <tr style="height: 1px">
            <td>
                
            </td>
        </tr>
        <tr>
            <td valign="top" style="position:relative">
                <div style="vertical-align: top;position:absolute;top:0px;right:0px;left:0px;bottom:0px;overflow:auto;">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                        Width="100%" Height="100%">
                    </rsweb:ReportViewer>
                </div>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            var tabIndex = '0';
            function CloseWindow(pageClosedHdn) {
                var closedHidden = document.getElementById(pageClosedHdn);
                closedHidden.value = 'true';
                window.returnValue = 1;
                this.close();
                var theparent;
                theparent = window.opener;
                if (theparent == null)
                    theparent = window.parent;
                theparent.refreshGrid();
                return false;
            }
          
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
