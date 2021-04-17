<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyWord.aspx.cs" Inherits="VietEngDict.ModifyWord" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Bảng từ vựng<br />
            <asp:GridView ID="gvWord" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="339px"
                OnRowCommand="gvWord_RowCommand" OnRowDeleting="gvWord_RowDeleting" DataKeyNames="idx">
                <Columns>
                    <asp:BoundField DataField="idx" HeaderText="TT" />
                    <asp:BoundField DataField="word" HeaderText="Từ" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnSelect" runat="server" Text="Chọn" CommandName="Select" CommandArgument='<%# Container.DataItemIndex %>'/> 
                            <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" ImageAlign="Middle"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="White" ForeColor="#000066" />
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                <RowStyle ForeColor="#000066" />
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#00547E" />
            </asp:GridView>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Từ:"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="txtWord" runat="server" Width="163px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Chi tiết:"></asp:Label>
            <asp:TextBox ID="txtDetail" runat="server" Height="340px" TextMode="MultiLine" Width="465px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Thêm" OnClick="Button1_Click" Width="77px" />
            &nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="Sửa" OnClick="Button2_Click" Width="77px" />
            <br />
        </div>
    </form>
</body>
</html>
