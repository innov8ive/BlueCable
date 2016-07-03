<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="HomePage.aspx.cs" Inherits="HMS.HomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <style type="text/css">
        /* CSSTerm.com Simple Horizontal DropDown CSS menu */.drop_menu
        {
            background: #1B8383;
            padding: 0;
            margin: 0;
            list-style-type: none;
            height: 33px;
            border-top: solid 1px #1B8383;
        }
        .drop_menu li
        {
            float: left;
        }
        .drop_menu li a
        {
            padding: 9px 20px;
            display: block;
            color: #fff;
            text-decoration: none;
            font: 12px arial, verdana, sans-serif;
        }
        /* Submenu */.drop_menu ul
        {
            position: absolute;
            left: -9999px;
            top: -9999px;
            list-style-type: none;
        }
        .liHover
        {
            position: relative;
            background: #F4F4F4;
        }
        .liHover li a
        {
            color: #000;
        }
        .drop_menu li:hover a
        {
            border-left: 1px solid #1B8383;
            color: #000;
        }
        ul.menu
        {
            display: none;
            left: 0px;
            top: 30px;
            background-color: #F4F4F4;
            padding: 0px;
            border-bottom: solid 1px #1B8383;
            border-right: solid 1px #1B8383;
            margin: 0;
        }
        ul.menu li a
        {
            padding: 5px;
            display: block;
            width: 218px;
            text-indent: 20px;
            background-color: #F4F4F4;
        }
        ul.menu li a:hover
        {
            background-color: orange;
        }
        .imgMenu
        {
            left: 4px;
            position: absolute;
            height: 16px;
            width: 16px;
            margin-top: 4px;
        }
        ul.submenu
        {
            display: none;
            position: absolute;
            left: 229px !important;
            top: 0px !important;
            border-top: solid 1px #1B8383;
            border-right: solid 1px #1B8383;
            border-bottom: solid 1px #1B8383;
            padding: 0;
            margin: 0;
        }
        .parent
        {
            background-image: url('images/Move_Next.gif');
            background-repeat: no-repeat;
            background-position: right center;
        }
        body, html, form
        {
            padding: 0;
            margin: 0;
            width: 100%;
            height: 100%;
            border: none;
        }
        .mainTable
        {
            border-collapse: collapse;
            height: 100%;
            width: 100%;
        }
        .titleBar
        {
            background-color: #1B8383;
            padding: 5px 0px 5px 15px;
            font-family: Arial;
        }
        .companyName
        {
            color: #1b8383;
            float: left;
            font-family: "Play";
            font-size: 40px;
            font-weight: bold;
            text-align: left;
            width: 500px;
            padding-top:10px;
        }
    </style>
</head>
<body style="background-color: white;">
    <form id="form1" runat="server">
    <table class="mainTable" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="height: 80px;">
                <div style="width: 80px; float: left;">
                    <%--<img alt="" src="image/logo.png" style="width:75px;"/>--%>
                </div>
                <div class="companyName">
                    Blue Tech Solutions
                </div>
                <div style="float: right; text-align: right; padding-right: 10px;">
                    Welcome
                    <asp:Label ID="lbUserName" runat="server" Font-Bold="true"></asp:Label>
                    <br />
                    <asp:LinkButton ID="lnkChngPassword" runat="server" Text="Change Password" OnClientClick="return setURL2('Change Password','ChangePassword.aspx');"></asp:LinkButton>&nbsp;|&nbsp;
                    <asp:LinkButton ID="lnkLogout" runat="server" Text="Logout" ForeColor="Red" OnClick="lnkLogout_Click"></asp:LinkButton>
                    <br/>
                    <asp:Label ID="lbExpiryDate" runat="server" Text=""></asp:Label>
                    <br/>
                    <asp:Label ID="lbSupport" runat="server" Text=""></asp:Label>
                </div>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 1px;">
            <td>
                &nbsp;
            </td>
            <td style="width: 96%;" class="SGrid">
                <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 40px;">
            <td>
                &nbsp;
            </td>
            <td class="titleBar SGrid">
                <span id="spanTitle" runat="server" style="color: White; font-size: 15px; font-weight: bold;">
                </span>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="vertical-align: top; background-color: White;" class="SGrid">
                <iframe frameborder="0" id="ifr" style="height: 100%; width: 100%;" src="" runat="server">
                </iframe>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 30px;" colspan="3">
            </td>
        </tr>
    </table>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('ifr').onload = document.getElementById('ifr').onreadystatechange = MyIframeReadyStateChanged;
        $(document).ready(function () {
            $('ul.drop_menu li').on('mouseenter', function () {
                $(this).addClass('liHover').find('>ul.submenu,>ul.menu').show("fast");
            }).on('mouseleave', function () {
                $(this).removeClass('liHover').find('>ul.submenu,>ul.menu').hide();
            });
            $('#ifr').height($('#ifr').parent().height());
            $('#ifr').width($('#ifr').parent().width() - 1);
        });
        function setURL(Obj) {
            var $Obj = $(Obj);
            $('#ifr').attr('src', $Obj.attr('URL').replace('#', ''));
            var pageTitle = $Obj.html();
            $('#spanTitle').attr('pageTitle', pageTitle).html('Loading ' + $Obj.html());
        }
        function setURL2(pageTitle, url) {
            $('#ifr').attr('src', url);
            $('#spanTitle').attr('pageTitle', pageTitle).html(pageTitle);
            return false;
        }
        function MyIframeReadyStateChanged() {
            if (document.getElementById('ifr').readyState == null || document.getElementById('ifr').readyState == 'complete') {
                $('#spanTitle').html($('#spanTitle').attr('pageTitle'));
            }
        }
    </script>
    </form>
</body>
</html>
