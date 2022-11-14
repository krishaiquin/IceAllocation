<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditIce.aspx.cs" Inherits="Admin_EditIce" MasterPageFile="~/MasterPage/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
<!--Author: Krisha-->

    <div>
        <h3>List of All Ice times:</h3>

    <!--Gridview that shows information of all Ice Times-->
    <asp:GridView ID="iceTimes" runat="server" DataSourceID="IceTime" AutoGenerateColumns="False"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4" DataKeyNames="IceId"
        OnRowCommand="iceTimes_RowCommand" OnSelectedIndexChanged="iceTimes_SelectedIndexChanged" AllowSorting="True" OnDataBound="iceTimes_DataBound">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>There are no ice times.</EmptyDataTemplate>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField DataField="FullName" HeaderText="Location" SortExpression="FullName" />
            <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
            <asp:BoundField DataField="End Time" HeaderText="End Time" />
            <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
            <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" />
            <asp:BoundField DataField="IceId" HeaderText="IceId" SortExpression="IceId" InsertVisible="False" ReadOnly="True" Visible="False" />
            <asp:BoundField DataField="LocationId" HeaderText="LocationId" Visible="False" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnDetails" runat="server" Text="Edit"
                        CommandName="seeDetails"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle CssClass="footerStyle"></FooterStyle>
        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
    </asp:GridView>

    <!--SQL source for gridview/table-->
        &nbsp;<asp:SqlDataSource ID="IceTime" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
            SelectCommand="SELECT
    IceTimes.Date AS 'Date',
    IceTimes.StartTime AS 'Start Time',
    IceTimes.EndTime AS 'End Time',
    IceTimes.TimeLength AS 'Duration (min)', 
    IceTimes.IceType AS 'Ice Type', 
    Teams.UserName AS 'Team',
    IceTimes.IceId, 
    IceTimes.LocationId,  
    Locations.FullName 
    FROM IceTimes INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId 
    LEFT OUTER JOIN Teams ON IceTimes.TeamId = Teams.TeamId
    ORDER BY Date ASC, StartTime ASC"></asp:SqlDataSource>
    </div>
</asp:Content>
