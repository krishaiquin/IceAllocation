<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Teams.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_Teams" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
<!--Author: Krisha-->

        <h3>Team Summary</h3>

        <!--Table that shows information about the teams-->
        <asp:GridView ID="teams" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TeamId" DataSourceID="teamTable" OnRowCommand="teams_RowCommand" OnDataBound="teams_DataBound"
            CssClass="gridviewStyle"
            FooterStyle-CssClass="footerStyle"
            HeaderStyle-CssClass="headerStyle"
            AlternatingRowStyle-CssClass="alt"
            CellPadding="4" Width="90%">
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField DataField="TeamId" HeaderText="TeamId" InsertVisible="False" ReadOnly="True" Visible="False" SortExpression="TeamId" />
                <asp:BoundField DataField="Team" HeaderText="Team" SortExpression="Team" />
             
                <asp:BoundField DataField="Primary Contact" HeaderText="Primary Contact" />
                <asp:BoundField DataField="GameBalance" HeaderText="Game Balance" />
                <asp:BoundField DataField="PracticeBalance" HeaderText="Practice Balance" />
                <asp:BoundField DataField="Enabled" HeaderText="Enabled" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnDetails" runat="server" Text="View Details"
                            CommandName="seeDetails"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

            <FooterStyle CssClass="footerStyle"></FooterStyle>

            <HeaderStyle CssClass="headerStyle"></HeaderStyle>
        </asp:GridView>
        
        <!--SQL source for the table-->
        <asp:SqlDataSource ID="teamTable" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
            SelectCommand="SELECT Teams.TeamId, Teams.UserName AS 'Team',  Teams.WhoToEmail AS 'Primary Contact', aspnet_Membership.IsApproved AS 'Enabled', Teams.GameBalance, Teams.PracticeBalance
 FROM Teams INNER JOIN aspnet_Membership ON Teams.UserId = aspnet_Membership.UserId"></asp:SqlDataSource>
</asp:Content>
