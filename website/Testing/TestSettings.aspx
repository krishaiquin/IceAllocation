<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestSettings.aspx.cs" Inherits="TestSettings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="SettingId" DataSourceID="SqlDataSource1">
            <Columns>
                <asp:BoundField DataField="SettingId" HeaderText="SettingId" InsertVisible="False" ReadOnly="True" SortExpression="SettingId" />
                <asp:BoundField DataField="Announcement" HeaderText="Announcement" SortExpression="Announcement" />
                <asp:BoundField DataField="AdminEmail" HeaderText="AdminEmail" SortExpression="AdminEmail" />
                <asp:BoundField DataField="ReviewingOn" HeaderText="ReviewingOn" SortExpression="ReviewingOn" />
                <asp:BoundField DataField="AppOpen" HeaderText="AppOpen" SortExpression="AppOpen" />
                <asp:BoundField DataField="LogoUrl" HeaderText="LogoUrl" SortExpression="LogoUrl" />
                <asp:BoundField DataField="Colour" HeaderText="Colour" SortExpression="Colour" />
                <asp:BoundField DataField="GameWindowWeekDayStart" HeaderText="GameWindowWeekDayStart" SortExpression="GameWindowWeekDayStart" />
                <asp:BoundField DataField="GameWindowWeekDayFinish" HeaderText="GameWindowWeekDayFinish" SortExpression="GameWindowWeekDayFinish" />
                <asp:BoundField DataField="GameWindowWeekEndStart" HeaderText="GameWindowWeekEndStart" SortExpression="GameWindowWeekEndStart" />
                <asp:BoundField DataField="GameWindowWeekEndFinish" HeaderText="GameWindowWeekEndFinish" SortExpression="GameWindowWeekEndFinish" />
                <asp:BoundField DataField="EmailAssignTemplate" HeaderText="EmailAssignTemplate" SortExpression="EmailAssignTemplate" />
                <asp:BoundField DataField="EmailRejectTemplate" HeaderText="EmailRejectTemplate" SortExpression="EmailRejectTemplate" />
                <asp:BoundField DataField="RollOverTime" HeaderText="RollOverTime" SortExpression="RollOverTime" />
                <asp:BoundField DataField="RollPracticeOver" HeaderText="RollPracticeOver" SortExpression="RollPracticeOver" />
                <asp:BoundField DataField="SiteTitle" HeaderText="SiteTitle" SortExpression="SiteTitle" />
                <asp:BoundField DataField="ReceiveNotifications" HeaderText="ReceiveNotifications" SortExpression="ReceiveNotifications" />
            </Columns>
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" SelectCommand="SELECT * FROM [Settings]"></asp:SqlDataSource>

    </div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Change gwweekdayfinish" OnClick="Button1_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
