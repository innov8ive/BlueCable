<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="UploadAdImages.aspx.cs"
    Inherits="HMS.UploadAdImages" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                First Image (250 X 100)
            </td>
        </tr>
        <tr>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="Image1" runat="server" AlternateText="Image 1" />
            </td>
        </tr>
        <tr>
            <td>
                <hr />
                Second Image (250 X 200)
            </td>
        </tr>
        <tr>
            <td>
                <asp:FileUpload ID="FileUpload2" runat="server" />
                <asp:Button ID="btnUpload2" runat="server" Text="Upload" OnClick="btnUpload2_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="Image2" runat="server" AlternateText="Image 1" />
            </td>
        </tr>
          <tr>
            <td>
                <hr />
                Third Image (600 X 150)
            </td>
        </tr>
        <tr>
            <td>
                <asp:FileUpload ID="FileUpload3" runat="server" />
                <asp:Button ID="btnUpload3" runat="server" Text="Upload" OnClick="btnUpload3_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="Image3" runat="server" AlternateText="Image 3" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
