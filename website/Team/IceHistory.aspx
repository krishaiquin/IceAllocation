<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IceHistory.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Team_IceHistory" %>


<asp:Content ContentPlaceHolderID="body" runat="server">
<!--Author: Krisha-->

    <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>

    <h3>Open Requests:</h3>
    <asp:GridView ID="currentlyRequested" runat="server" AutoGenerateColumns="False" DataSourceID="requestedSQLSource" OnDataBound="gridView_DataBound"
        CssClass="gridviewStyle" 
        FooterStyle-CssClass="footerStyle" 
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4" AllowSorting="True">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>You have no open requests history.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Arena" HeaderText="Arena" ReadOnly="True" SortExpression="Arena" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="StartTime" HeaderText="Start Time"  />
            <asp:BoundField DataField="EndTime" HeaderText="End Time"  />
            <asp:BoundField DataField="TimeLength" HeaderText="Duration"  />
            <asp:BoundField DataField="IceType" HeaderText="Game/Practice"  />
        </Columns>
    </asp:GridView>
    <br />

    <h3>Accepted Ice:</h3>
    <asp:GridView ID="acceptedRequest" runat="server" AutoGenerateColumns="False" DataSourceID="acceptedSQLSource" OnDataBound="gridView_DataBound"
        CssClass="gridviewStyle" 
        FooterStyle-CssClass="footerStyle" 
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4" AllowSorting="True">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>You have no accepted requests history.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Arena" HeaderText="Arena" ReadOnly="True" SortExpression="Arena" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="StartTime" HeaderText="Start Time"  />
            <asp:BoundField DataField="EndTime" HeaderText="End Time"  />
            <asp:BoundField DataField="TimeLength" HeaderText="Duration"  />
            <asp:BoundField DataField="IceType" HeaderText="Game/Practice"  />
        </Columns>
    </asp:GridView>
    <br />
    <h3>Rejected Ice:</h3>
    <asp:GridView ID="rejectedRequest" runat="server" AutoGenerateColumns="False" DataSourceID="rejectedSQLSource" OnDataBound="gridView_DataBound"
        CssClass="gridviewStyle" 
        FooterStyle-CssClass="footerStyle" 
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4" AllowSorting="True">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>You have no rejected requests history.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Arena" HeaderText="Arena" ReadOnly="True" SortExpression="Arena" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="StartTime" HeaderText="Start Time" />
            <asp:BoundField DataField="EndTime" HeaderText="End Time"  />
            <asp:BoundField DataField="TimeLength" HeaderText="Duration"  />
            <asp:BoundField DataField="IceType" HeaderText="Game/Practice"  />
        </Columns>
    </asp:GridView>
    <br />
    <h3>Returned Ice:</h3>
    <asp:GridView ID="returnedIce" runat="server" AutoGenerateColumns="False" DataSourceID="returnedIceSQLSource" OnDataBound="returnedIce_DataBound"
        CssClass="gridviewStyle" 
        FooterStyle-CssClass="footerStyle" 
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4" AllowSorting="True">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>You have no returned ice history.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Arena" HeaderText="Arena" ReadOnly="True" SortExpression="Arena" />
            <asp:BoundField DataField="ReturnedDate" HeaderText="Returned Date" SortExpression="ReturnedDate" />
        </Columns>
    </asp:GridView>


    <asp:SqlDataSource ID="requestedSQLSource" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" 
        SelectCommand="SELECT (Locations.LocationName+ ' - ' +Locations.RinkName) AS Arena, 
        IceTimes.Date AS 'Date', 
        IceTimes.StartTime AS 'StartTime', 
        IceTimes.EndTime AS 'EndTime', 
        IceTimes.TimeLength, 
        IceTimes.IceType 
        FROM Requests INNER JOIN IceTimes ON Requests.IceId = IceTimes.IceId 
        INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId 
        WHERE (Requests.TeamId = @TeamId) AND (Requests.Status = 'Open')
        Order by Date ASC, StartTime ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Label1" Name="TeamId" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="acceptedSQLSource" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" 
        SelectCommand="SELECT (Locations.LocationName+ ' - '+Locations.RinkName) As Arena, 
        IceTimes.Date AS 'Date', 
        IceTimes.StartTime AS 'StartTime', 
        IceTimes.EndTime AS 'EndTime', 
        IceTimes.TimeLength,  
        IceTimes.IceType 
        FROM Requests 
        INNER JOIN IceTimes ON Requests.IceId = IceTimes.IceId 
        INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
        WHERE (Requests.TeamId = @TeamId) AND (Requests.Status = 'Accepted')
        Order by Date ASC, StartTime ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Label1" Name="TeamId" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="rejectedSQLSource" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" 
        SelectCommand="SELECT (Locations.LocationName+ ' - '+Locations.RinkName) As Arena, 
        IceTimes.Date AS 'Date', 
        IceTimes.StartTime AS 'StartTime', 
        IceTimes.EndTime AS 'EndTime',
        IceTimes.TimeLength,   
        IceTimes.IceType 
        FROM Requests 
        INNER JOIN IceTimes ON Requests.IceId = IceTimes.IceId 
        INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
        WHERE (Requests.TeamId = @TeamId) AND (Requests.Status = 'Rejected')
        Order by Date ASC, StartTime ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Label1" Name="TeamId" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="returnedIceSQLSource" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" 
        SelectCommand="SELECT (Locations.LocationName+' - '+Locations.RinkName) As Arena, 
        ReturnedIce.ReturnedDate AS 'ReturnedDate' 
        FROM ReturnedIce 
        INNER JOIN IceTimes ON ReturnedIce.IceId = IceTimes.IceId 
        INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId 
        WHERE (ReturnedIce.ReturnerId = @ReturnedId)
        Order by ReturnedDate ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="Label1" Name="ReturnedId" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    </asp:Content>