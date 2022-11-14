<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestTeam.aspx.cs" Inherits="Testing_TestTeam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID ="lblTeamInfo" runat="server"></asp:Label>
        <table><tr><td>
        <asp:Button ID="btnIG" runat="server" Text="Increase Game" OnClick="btnIG_Click" /></td><td>
         <asp:Button ID="btnDG" runat="server" Text="Decrease Game" OnClick="btnDG_Click" /></td></tr><tr><td>
         <asp:Button ID="btnIP" runat="server" Text="Increase Practice" OnClick="btnIP_Click" /></td><td>
         <asp:Button ID="btnDP" runat="server" Text="Decrease Practice" OnClick="btnDP_Click" /></td></tr></table>
    </div>
   
        <asp:Button ID="btnNotify" runat="server" Text="Flip notifications" OnClick="btnNotify_Click" />
        <br />
        <asp:Label ID="lblNotify" runat="server" Text=""></asp:Label>

        <br />
        <br />


    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="False" DataKeyNames="TeamId" DataSourceID="SqlDataSource1">
        <Fields>
            <asp:BoundField DataField="TeamId" HeaderText="TeamId" InsertVisible="False" ReadOnly="True" SortExpression="TeamId" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:BoundField DataField="Division" HeaderText="Division" SortExpression="Division" />
            <asp:BoundField DataField="CoachEmail" HeaderText="CoachEmail" SortExpression="CoachEmail" />
            <asp:BoundField DataField="ManagerEmail" HeaderText="ManagerEmail" SortExpression="ManagerEmail" />
            <asp:BoundField DataField="GameBalance" HeaderText="GameBalance" SortExpression="GameBalance" />
            <asp:BoundField DataField="PracticeBalance" HeaderText="PracticeBalance" SortExpression="PracticeBalance" />
            <asp:BoundField DataField="WhoToEmail" HeaderText="WhoToEmail" SortExpression="WhoToEmail" />
            <asp:BoundField DataField="Announcement" HeaderText="Announcement" SortExpression="Announcement" />
            <asp:BoundField DataField="ReceiveNotifications" HeaderText="ReceiveNotifications" SortExpression="ReceiveNotifications" />
        </Fields>
    </asp:DetailsView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" SelectCommand="SELECT * FROM [Teams] WHERE ([TeamId] = @TeamId)">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="TeamId" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

         <br />
        <br />
        <br />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Get Id" />
        <br />
        <asp:Label ID="lblTeamId" runat="server" Text="Label"></asp:Label>

         <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Test Get all contact info" />
        <br />
        <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>


        <br />
        <br />
        <br />


        <asp:TextBox ID="txtCoachEmail" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnCoach" runat="server" Text="Button" OnClick="btnCoach_Click" />
        <br />
        <asp:Label ID="lblcoach" runat="server" Text="Label"></asp:Label>
         </form>
</body>
</html>
