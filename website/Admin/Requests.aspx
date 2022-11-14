<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Requests.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_Requests" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
   <!--Author: Adrian-->
     <h3>List of all Requests for Ice:</h3>
    <asp:GridView ID="gridReturnedIce" runat="server" AutoGenerateColumns="False" DataSourceID="RequestsForIce" DataKeyNames="IceId"
        OnRowCommand="ReturnedIce_RowCommand" OnDataBound="gridClosedRequests_DataBound"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4" AllowSorting="True">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>There are no returned ice.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Requests" HeaderText="Requests" SortExpression="Requests" ReadOnly="True" />
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
            <asp:BoundField DataField="End Time" HeaderText="End Time" />
            <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
            <asp:BoundField DataField="Returner" HeaderText="Returner" />
            <asp:BoundField DataField="Date Returned" HeaderText="Date Returned" SortExpression="Date Returned" />
            <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnRequestDetails"
                        Text="See Requests"
                        CommandName="RequestDetails"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblReturnMessage" runat="server" Text="Label"></asp:Label>
    <br />
    <h3>Closed Requests:</h3>
    <asp:GridView ID="gridClosedRequests" runat="server" AutoGenerateColumns="False" DataSourceID="ClosedRequests" DataKeyNames="IceId"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4" AllowSorting="True" OnDataBound="gridClosedRequests_DataBound">
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField DataField="Requests" HeaderText="Requests" SortExpression="Requests" ReadOnly="True" />
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
            <asp:BoundField DataField="End Time" HeaderText="End Time" />
            <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
            <asp:BoundField DataField="Returner" HeaderText="Returner" ReadOnly="True" />
            <asp:BoundField DataField="Date Returned" HeaderText="Date Returned" SortExpression="Date Returned" />
            <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="IceId" HeaderText="IceId" ReadOnly="True" SortExpression="IceId" Visible="False" />
        </Columns>

        <FooterStyle CssClass="footerStyle"></FooterStyle>

        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
    </asp:GridView>
    <asp:SqlDataSource ID="RequestsForIce" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT ReturnedIce.IceId,
        (Select Count(IceId) FROM Requests WHERE Requests.IceId = ReturnedIce.IceId AND Status = 'Open') As 'Requests',
        (Select FullName FROM Locations WHERE IceTimes.LocationId = Locations.LocationId) AS 'Location',
        Date AS 'Date',
        StartTime AS 'Start Time',
        EndTime AS 'End Time',
        TimeLength AS 'Duration (min)',
        (Select UserName FROM Teams WHERE Teams.TeamId = ReturnedIce.ReturnerId) AS 'Returner',
        ReturnedDate as 'Date Returned',
        IceType AS 'Ice Type'
        FROM ReturnedIce
        INNER JOIN IceTimes ON IceTimes.IceId = ReturnedIce.IceId
        WHERE Status = 'Open' AND
        Date &gt;= GetDate()
        ORDER BY Date ASC, StartTime ASC"></asp:SqlDataSource>


    <asp:SqlDataSource ID="ClosedRequests" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT ReturnedIce.IceId,
        (Select Count(IceId) FROM Requests WHERE Requests.IceId = ReturnedIce.IceId AND Status = 'Open') As 'Requests',
        (Select FullName FROM Locations WHERE IceTimes.LocationId = Locations.LocationId) AS 'Location',
        Date AS 'Date',
        StartTime AS 'Start Time',
        EndTime AS 'End Time',
        TimeLength AS 'Duration (min)',
        (Select UserName FROM Teams WHERE Teams.TeamId = ReturnedIce.ReturnerId) AS 'Returner',
        ReturnedDate as 'Date Returned',
        IceType AS 'Ice Type',
        Status
        FROM ReturnedIce
        INNER JOIN IceTimes ON IceTimes.IceId = ReturnedIce.IceId
        WHERE Status = 'Closed'  OR
        Date &lt;= GetDate()
        ORDER BY Date ASC, StartTime ASC
        "></asp:SqlDataSource>
</asp:Content>
