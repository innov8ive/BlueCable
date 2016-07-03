<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TODOPendingList.aspx.cs" Inherits="HMS.Transactions.TODO.TODOPendingList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <sc:ExGrid ID="PendingTODOListGrid" runat="server" Height="440px" Width="730px"
                AutoGenerateColumns="False" ToolTipCssClass="toolTip" DataValueField="ID"
                DataTextField="ID" KeyBoardNavigation="true"
                OnRowDataBound="PendingTODOListGrid_RowDataBound" OnRowCommand="PendingTODOListGrid_RowCommand">
                <FooterStyle CssClass="SGridFooter" />
                <RowStyle CssClass="SGridItem" />
                <HeaderStyle CssClass="SGrid_Header" />
                <SelectedRowStyle CssClass="SGridSelectedItem" />
                <EmptyDataTemplate>
                    There is no any pending TODO task(s).
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lbSrNo" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField SortExpression="Title" DataField="Title" HeaderText="Title" />
                    <asp:BoundField SortExpression="Details" DataField="Details" HeaderText="Details" />
                    <asp:BoundField SortExpression="DueDate" DataField="DueDate" HeaderText="Due Date" 
                        DataFormatString="{0:dd-MMM-yyyy}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnCompleted" runat="server" Text="Mark Completed" 
                                CommandName="Complete" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('Are you sure, want to complete this task?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </sc:ExGrid>
        </div>
    </form>
</body>
</html>
