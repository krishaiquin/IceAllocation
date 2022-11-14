<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Team_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
  <!--Author: Adrian-->
      <asp:Label ID="LabelUserName" runat="server" Text="Label" Visible="False"></asp:Label>
    <div>
        <asp:Panel ID="pnlMessages" runat="server" Visible="false">
            <h2>Announcements:</h2>
            <asp:Panel ID="pnlShutdown" runat="server" Visible="false">
                <asp:Label ID="lblShutdownMsg" CssClass="shutdownmsg" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="pnlAnnouncements" runat="server" Visible="false">
                <asp:Label ID="lblAnnouncements" CssClass="announcement" runat="server" Text=""></asp:Label>
                <br />
                <br />
                <br />
            </asp:Panel>
        </asp:Panel>
    </div>

    <asp:Panel ID="pnlAvailOpenIce" runat="server" Visible="true">
        <h3>Available Open Ice:</h3>
        <asp:GridView ID="gridAvailOpenIce" runat="server" DataSourceID="SqlAvailOpenIce" AutoGenerateColumns="false" AllowSorting="true"
            OnRowCommand="gridAvailOpenIce_RowCommand" DataKeyNames="IceId" OnDataBound="Scheduled_DataBound" OnRowDataBound="gridAvailOpenIce_RowDataBound"
            CssClass="gridviewStyle"
            FooterStyle-CssClass="footerStyle"
            HeaderStyle-CssClass="headerStyle"
            AlternatingRowStyle-CssClass="alt"
            CellPadding="4">
            <Columns>
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
                <asp:BoundField DataField="End Time" HeaderText="End Time" />
                <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
                <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" Visible="false" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btnClaimOpen"
                            Text="Claim Open Ice"
                            CommandName="ClaimOpen"
                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="lblOpenStatus" CssClass="lblResult" runat="server" Text=""></asp:Label>
        <br />
        <br />
    </asp:Panel>
    
    <h3>Available Game Ice:</h3>
    Game Balance: <b>
        <asp:Label ID="lblGameBalance" runat="server" Text=""></asp:Label></b>
    <asp:GridView ID="gridAvailableGameIce" runat="server" AutoGenerateColumns="False" DataSourceID="AvailableGameIce"
        OnRowCommand="RequestCommand" DataKeyNames="IceId" OnRowDataBound="gridReturnedIce_RowDataBound" AllowSorting="True" OnDataBound="Scheduled_DataBound"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>There is no available game ice.</EmptyDataTemplate>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
            <asp:BoundField DataField="End Time" HeaderText="End Time" />
            <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
            <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" Visible="false" />
            <asp:BoundField DataField="Returner" HeaderText="Returner" Visible="false" />
            <asp:BoundField DataField="Date Returned" HeaderText="Date Returned" Visible="false" />
            <asp:BoundField DataField="Requested" HeaderText="Requested" Visible="false" />
            <asp:BoundField DataField="Number of Requests" HeaderText="Number of Requests" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnRequest"
                        Text="Request Ice"
                        CommandName="RequestGame"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Request Multiple</HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chbRequest" runat="server" />
                    <asp:Label ID="lblIceId" runat="server" Text='<%# Eval("IceId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="IceId" HeaderText="IceId" ReadOnly="True" SortExpression="IceId" Visible="false" />
        </Columns>
        
        <FooterStyle CssClass="footerStyle"></FooterStyle>
        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
    </asp:GridView>
    <asp:Button ID="btnRequestGameSel" runat="server" Text="Request Selected" OnClick="btnRequestGameSel_Click" /><br />
    <asp:Label ID="lblRequestGameMessage" CssClass="lblResult" runat="server" Text=""></asp:Label>

    <br />
    <br />

    <h3>Available Practice Ice:</h3>
    Practice Balance: <b>
        <asp:Label ID="lblPracticeBalance" runat="server" Text=""></asp:Label></b>
    <asp:GridView ID="gridAvailablePracticeIce" runat="server" AutoGenerateColumns="False" DataSourceID="AvailablePracticeIce"
        OnRowCommand="RequestCommand" DataKeyNames="IceId" OnRowDataBound="gridReturnedIce_RowDataBound" AllowSorting="True" OnDataBound="Scheduled_DataBound"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>There is no available practice ice.</EmptyDataTemplate>
        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
        <Columns>
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
            <asp:BoundField DataField="End Time" HeaderText="End Time" />
            <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
            <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" Visible="false" />
            <asp:BoundField DataField="Returner" HeaderText="Returner" Visible="false" />
            <asp:BoundField DataField="Date Returned" HeaderText="Date Returned" Visible="false" />
            <asp:BoundField DataField="Requested" HeaderText="Requested" Visible="false" />
            <asp:BoundField DataField="Number of Requests" HeaderText="Number of Requests" />
            <asp:BoundField DataField="IceId" HeaderText="IceId" ReadOnly="True" SortExpression="IceId" Visible="false" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnRequest"
                        Text="Request Ice"
                        CommandName="RequestPractice"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Request Multiple</HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chbRequest" runat="server" />
                    <asp:Label ID="lblIceId" runat="server" Text='<%# Eval("IceId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <FooterStyle CssClass="footerStyle"></FooterStyle>
        <HeaderStyle CssClass="headerStyle"></HeaderStyle>
    </asp:GridView>
    <asp:Button ID="btnRequestPracticeSel" runat="server" Text="Request Selected" OnClick="btnRequestPracticeSel_Click" /><br />
    <asp:Label ID="lblRequestPracticeMessage" CssClass="lblResult" runat="server" Text=""></asp:Label>

    <br />
    <br />

    <h3>Your Scheduled Game Ice:</h3>
    <asp:GridView ID="gridScheduledGameIce" runat="server" AutoGenerateColumns="False" DataSourceID="ScheduledGameIce"
        OnRowCommand="ReturnCommand" DataKeyNames="IceId" OnRowDataBound="gridScheduled_RowDataBound" AllowSorting="True" OnDataBound="Scheduled_DataBound"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>Your game schedule is empty.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
            <asp:BoundField DataField="End Time" HeaderText="End Time" />
            <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
            <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" Visible="false" />
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblReceived" runat="server" Text='<%# Eval("Received") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Return Single</HeaderTemplate>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnReturn"
                        Text="Return Ice"
                        CommandName="ReturnGame"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Return Multiple</HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chbReturn" runat="server" />
                    <asp:Label ID="lblIceId" runat="server" Text='<%# Eval("IceId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnReturnGameSel" runat="server" Text="Return Selected" OnClick="btnReturnGameSel_Click" /><br />
    <asp:Label ID="lblReturnGameMessage" CssClass="lblResult" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblGameBalanceReturn" runat="server" Text=""></asp:Label>

    <br />
    <br />

    <h3>Your Scheduled Practice Ice:</h3>
    <asp:GridView ID="gridScheduledPracticeIce" runat="server" AutoGenerateColumns="False" DataSourceID="ScheduledPracticeIce"
        OnRowCommand="ReturnCommand" DataKeyNames="IceId" OnRowDataBound="gridScheduled_RowDataBound" AllowSorting="True" OnDataBound="Scheduled_DataBound"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>Your practice schedule is empty.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
            <asp:BoundField DataField="End Time" HeaderText="End Time" />
            <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
            <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" Visible="false" />
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblReceived" runat="server" Text='<%# Eval("Received") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Return Single</HeaderTemplate>
                <ItemTemplate>
                    <asp:Button runat="server" ID="btnReturn"
                        Text="Return Ice"
                        CommandName="ReturnPractice"
                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>Return Multiple</HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chbReturn" runat="server" />
                    <asp:Label ID="lblIceId" runat="server" Text='<%# Eval("IceId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnReturnPracticeSel" runat="server" Text="Return Selected" OnClick="btnReturnPracticeSel_Click" /><br />
    <asp:Label ID="lblReturnPracticeMessage" CssClass="lblResult" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblPracticeBalanceReturn" runat="server" Text=""></asp:Label>

    <br />
    <br />

    <asp:Panel ID="pnlOpenIce" runat="server" Visible="true">
        <h3>Your Open Ice:</h3>
        <asp:GridView ID="gridOpenIce" runat="server" DataSourceID="SqlOpenIce" AutoGenerateColumns="false" AllowSorting="true" OnDataBound="Scheduled_DataBound"
            CssClass="gridviewStyle"
            FooterStyle-CssClass="footerStyle"
            HeaderStyle-CssClass="headerStyle"
            AlternatingRowStyle-CssClass="alt"
            CellPadding="4">
            <Columns>
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ReadOnly="True" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                <asp:BoundField DataField="Start Time" HeaderText="Start Time" />
                <asp:BoundField DataField="End Time" HeaderText="End Time" />
                <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" />
                <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" Visible="false" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" Visible="false" />
            </Columns>
        </asp:GridView>
        <br />
        <br />
    </asp:Panel>

    &nbsp;<asp:SqlDataSource ID="SqlAvailOpenIce" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT
            IceTimes.IceId,
            Locations.FullName AS Location,
            IceTimes.Date AS 'Date',
            IceTimes.StartTime AS 'Start Time',
            IceTimes.EndTime AS 'End Time',
            IceTimes.TimeLength AS 'Duration (min)'
            FROM IceTimes
            INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
            WHERE IceTimes.IceType = 'Open'
            AND IceTimes.TeamId IS NULL
            AND IceTimes.Date &gt;= GETDATE()
            ORDER BY IceTimes.Date ASC, IceTimes.StartTime ASC"></asp:SqlDataSource>

    <asp:SqlDataSource ID="SqlOpenIce" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT
            IceTimes.IceId,
            Locations.FullName AS Location,
            IceTimes.Date AS 'Date',
            IceTimes.StartTime AS 'Start Time',
            IceTimes.EndTime AS 'End Time',
            IceTimes.TimeLength AS 'Duration (min)',
			Teams.UserName
            FROM IceTimes
            INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
			INNER JOIN Teams ON IceTimes.TeamId = Teams.TeamId
            WHERE IceTimes.IceType = 'Open'
            AND (Teams.UserName = @username)
            AND IceTimes.Date &gt;= GETDATE()
            ORDER BY IceTimes.Date ASC, IceTimes.StartTime ASC">

        <SelectParameters>
            <asp:ControlParameter ControlID="LabelUserName" Name="username" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="ScheduledGameIce" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT
            IceTimes.IceId,
            Locations.FullName AS Location,
            IceTimes.Date AS 'Date',
            IceTimes.StartTime AS 'Start Time',
            IceTimes.EndTime AS 'End Time',
            IceTimes.TimeLength AS 'Duration (min)',
            IceTimes.IceType AS 'Ice Type',
            Teams.UserName,
            (SELECT COUNT(*) FROM ReturnedIce WHERE ReturnedIce.IceId = IceTimes.IceId) AS 'Received'
            FROM IceTimes
            INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
            INNER JOIN Teams ON IceTimes.TeamId = Teams.TeamId
            WHERE (Teams.UserName = @username)
            AND IceTimes.IceType = 'Game'
            AND IceTimes.Date &gt;= GETDATE()
            ORDER BY IceTimes.Date ASC, IceTimes.StartTime ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="LabelUserName" Name="username" PropertyName="Text" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IceId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="ScheduledPracticeIce" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT
            IceTimes.IceId,
            Locations.FullName AS Location,
            IceTimes.Date AS 'Date',
            IceTimes.StartTime AS 'Start Time',
            IceTimes.EndTime AS 'End Time',
            IceTimes.TimeLength AS 'Duration (min)',
            IceTimes.IceType AS 'Ice Type',
            Teams.UserName,
            (SELECT COUNT(*) FROM ReturnedIce WHERE ReturnedIce.IceId = IceTimes.IceId) AS 'Received'
            FROM IceTimes
            INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
            INNER JOIN Teams ON IceTimes.TeamId = Teams.TeamId
            WHERE (Teams.UserName = @username)
            AND IceTimes.IceType = 'Practice'
            AND IceTimes.Date &gt;= GETDATE()
            ORDER BY IceTimes.Date ASC, IceTimes.StartTime ASC">
        <SelectParameters>
            <asp:ControlParameter ControlID="LabelUserName" Name="username" PropertyName="Text" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="IceId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="AvailableGameIce" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT
            ReturnedIce.IceId,
            Locations.FullName AS Location,
            IceTimes.Date AS 'Date',
            IceTimes.StartTime AS 'Start Time',
            IceTimes.EndTime AS 'End Time',
            IceTimes.TimeLength AS 'Duration (min)',
            IceTimes.IceType AS 'Ice Type',
            Teams.UserName AS 'Returner',
            ReturnedIce.ReturnedDate AS 'Date Returned',
            (SELECT COUNT(*) FROM Requests WHERE Requests.IceId = ReturnedIce.IceId AND TeamId=(Select TeamId FROM Teams WHERE UserName = @username) AND Status = 'Open') AS 'Requested',
            (SELECT COUNT(*) FROM Requests WHERE Requests.IceId = ReturnedIce.IceId AND Status = 'Open') AS 'Number of Requests'
            FROM ReturnedIce
            INNER JOIN IceTimes ON IceTimes.IceId = ReturnedIce.IceId  
            INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
            INNER JOIN Teams ON ReturnedIce.ReturnerId = Teams.TeamId
            WHERE ReturnedIce.Status = 'Open'
            AND ReturnedIce.IsApproved = 'true'
            AND IceTimes.IceType = 'Game'
            AND IceTimes.Date &gt;= GETDATE()
            ORDER BY IceTimes.Date ASC, IceTimes.StartTime ASC
            ">
        <SelectParameters>
            <asp:ControlParameter ControlID="LabelUserName" Name="username" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="AvailablePracticeIce" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT
            ReturnedIce.IceId,
            Locations.FullName AS Location,
            IceTimes.Date AS 'Date',
            IceTimes.StartTime AS 'Start Time',
            IceTimes.EndTime AS 'End Time',
            IceTimes.TimeLength AS 'Duration (min)',
            IceTimes.IceType AS 'Ice Type',
            Teams.UserName AS 'Returner',
            ReturnedIce.ReturnedDate AS 'Date Returned',
            (SELECT COUNT(*) FROM Requests WHERE Requests.IceId = ReturnedIce.IceId AND TeamId=(Select TeamId FROM Teams WHERE UserName = @username) AND Status = 'Open') AS 'Requested',
            (SELECT COUNT(*) FROM Requests WHERE Requests.IceId = ReturnedIce.IceId AND Status = 'Open') AS 'Number of Requests'
            FROM ReturnedIce
            INNER JOIN IceTimes ON IceTimes.IceId = ReturnedIce.IceId  
            INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
            INNER JOIN Teams ON ReturnedIce.ReturnerId = Teams.TeamId
            WHERE ReturnedIce.Status = 'Open'
            AND ReturnedIce.IsApproved = 'true'
            AND IceTimes.IceType = 'Practice'
            AND IceTimes.Date &gt;= GETDATE()
            ORDER BY IceTimes.Date ASC, IceTimes.StartTime ASC
            ">
        <SelectParameters>
            <asp:ControlParameter ControlID="LabelUserName" Name="username" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>

</asp:Content>
