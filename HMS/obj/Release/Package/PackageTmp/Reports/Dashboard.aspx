<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="HMS.Reports.Dashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .box p,.box p span
        {
            margin:5px;
            font-size:18px;
            font-weight:bold;
        }
        .box
        {
            float: left;
            width: 30%;
            border: solid 1px black;
            margin: 5px;
            padding: 5px;
            text-align: center;
            box-shadow: 5px 5px 5px black;
            border-radius:5px;
        }
        .dashboardHeading
        {
            font-size: 15px;
            font-weight: bold;
            text-align: center;
            float: left;
            width: 93%;
        }
        .data
        {
            color: red;
        }
    </style>
</head>
<body style="background-color: white;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnOperatorID" runat="server" />
        
    <table style="width: 100%;">
        <tr>
            <td></td>
            <td>
                Select Collection Boy : <asp:DropDownList ID="ddlCB" runat="server" CssClass="CTextBox" onchange="refreshDashBoard(this);"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 30%">
            </td>
            <td>
                <div class="dashboardHeading bigHeading">
                    Point Status
                </div>
                <div>
                    <div class="box">
                        <p>
                            Active</p>
                        <p class="data">
                            <asp:Label ID="lbActive" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Deactive</p>
                        <p class="data">
                            <asp:Label ID="lbDeactive" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Total Points</p>
                        <p class="data">
                            <asp:Label ID="lbTotalPoints" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="dashboardHeading bigHeading">
                        Financial Overview
                    </div>
                </div>
                <div>
                    <div class="box">
                        <p>
                            Annual Turnover</p>
                        <p class="data">
                            <asp:Label ID="lbAnnualTurnOver" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Current Billing</p>
                        <p class="data">
                            <asp:Label ID="lbCurBilling" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Total Entertainment Tax</p>
                        <p class="data">
                            <asp:Label ID="lbTotalEntTax" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Total Collection</p>
                        <p class="data">
                            <asp:Label ID="lbTotalCollection" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Current Collection</p>
                        <p class="data">
                            <asp:Label ID="lbCurrentCollection" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Total Service Tax</p>
                        <p class="data">
                            <asp:Label ID="lbTotalServiceTax" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Total Outstanding</p>
                        <p class="data">
                            <asp:Label ID="lbTotalOutstanding" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            Current Outstanding</p>
                        <p class="data">
                            <asp:Label ID="lbCurrentOutstanding" runat="server" Text="Loading..."></asp:Label>
                        </p>
                    </div>
                    <div class="box">
                        <p>
                            -------------</p>
                        <p class="data">
                            <asp:Label ID="Label1" runat="server" Text="0"></asp:Label>
                        </p>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <script language="javascript" type="text/javascript">
            function refreshDashBoard(Obj) {
                var cb = Obj == null ? '' : Obj.value;
                var operatorID = $('#<%=hdnOperatorID.ClientID  %>').val();

                $('#<%=lbActive.ClientID %>').html('Loading...');
                PageMethods.GetActiveCustomers(operatorID, cb, function (result) {
                    $('#<%=lbActive.ClientID %>').html(result);
                });

                $('#<%=lbDeactive.ClientID %>').html('Loading...');
                PageMethods.GetDeActiveCustomers(operatorID, cb, function (result) {
                    $('#<%=lbDeactive.ClientID %>').html(result);
                });

                $('#<%=lbTotalPoints.ClientID %>').html('Loading...');
                PageMethods.GetTotalPoints(operatorID, cb, function (result) {
                    $('#<%=lbTotalPoints.ClientID %>').html(result);
                });

                $('#<%=lbAnnualTurnOver.ClientID %>').html('Loading...');
                PageMethods.GetTotalTurnOver(operatorID, cb, function (result) {
                    $('#<%=lbAnnualTurnOver.ClientID %>').html(result);
                });

                $('#<%=lbCurBilling.ClientID %>').html('Loading...');
                PageMethods.GetCurrentBilling(operatorID, cb, function (result) {
                    $('#<%=lbCurBilling.ClientID %>').html(result);
                });

                $('#<%=lbTotalCollection.ClientID %>').html('Loading...');
                PageMethods.GetTotalCollection(operatorID, cb, function (result) {
                    $('#<%=lbTotalCollection.ClientID %>').html(result);
                });

                $('#<%=lbCurrentCollection.ClientID %>').html('Loading...');
                PageMethods.GetCurrentCollection(operatorID, cb, function (result) {
                    $('#<%=lbCurrentCollection.ClientID %>').html(result);
                });

                $('#<%=lbTotalServiceTax.ClientID %>').html('Loading...');
                PageMethods.GetTotalServiceTax(operatorID, cb, function (result) {
                    $('#<%=lbTotalServiceTax.ClientID %>').html(result);
                });

                $('#<%=lbTotalEntTax.ClientID %>').html('Loading...');
                PageMethods.GetTotalEntTax(operatorID, cb, function (result) {
                    $('#<%=lbTotalEntTax.ClientID %>').html(result);
                });

                $('#<%=lbTotalOutstanding.ClientID %>').html('Loading...');
                PageMethods.GetTotalOutstanding(operatorID, cb, function (result) {
                    $('#<%=lbTotalOutstanding.ClientID %>').html(result);
                });

                $('#<%=lbCurrentOutstanding.ClientID %>').html('Loading...');
                PageMethods.GetCurrentOutstanding(operatorID, cb, function (result) {
                    $('#<%=lbCurrentOutstanding.ClientID %>').html(result);
                });
            }

            $(document).ready(function () {
                refreshDashBoard(null);
            })
        </script>
    </asp:PlaceHolder>
    </form>
</body>
</html>
