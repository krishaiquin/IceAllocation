<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReviewIce.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_ReviewIce" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
<!--Author: Adrian-->
    <div>
        Enable Reviewing:<br />
        <asp:RadioButtonList ID="reviewingToggleList" runat="server">
            <asp:ListItem Value="true">Enable Reviewing of Ice Time</asp:ListItem>
            <asp:ListItem Value="false">Disable Reviewing of Ice Time</asp:ListItem>
        </asp:RadioButtonList><br />
        <asp:Button ID="setReviewing" runat="server" Text="Confirm" OnClick="btn_setReviewing" />
        <asp:Label ID="lblReviewingStatus" CssClass="lblResult" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <br />
        <br />

    </div>
    <asp:Panel ID="pnlReviewOptions" runat="server">
        <div>
            <asp:GridView ID="gridReturnedIce" runat="server" DataSourceID="ReturnedIceDataSrc"
                CssClass="gridviewStyle"
                FooterStyle-CssClass="footerStyle"
                HeaderStyle-CssClass="headerStyle"
                AlternatingRowStyle-CssClass="alt"
                EmptyDataRowStyle-BorderStyle="None"
                CellPadding="4" AutoGenerateColumns="False" DataKeyNames="IceId">
                <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
                <EmptyDataTemplate>No returned ice to approve</EmptyDataTemplate>
                <Columns>
                    <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" />
                    <asp:BoundField DataField="Returner" HeaderText="Returner" SortExpression="Returner" />
                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" ReadOnly="True" />
                    <asp:BoundField DataField="Start Time" HeaderText="Start Time" SortExpression="Start Time" ReadOnly="True" />
                    <asp:BoundField DataField="End Time" HeaderText="End Time" SortExpression="End Time" ReadOnly="True" />
                    <asp:BoundField DataField="Duration (min)" HeaderText="Duration (min)" SortExpression="Duration (min)" />
                    <asp:BoundField DataField="Ice Type" HeaderText="Ice Type" SortExpression="Ice Type" />
                    <asp:BoundField DataField="IceId" HeaderText="IceId" ReadOnly="True" SortExpression="IceId" Visible="false" />
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chbSelect" runat="server" />
                            <asp:Label ID="lblIceId" runat="server" Text='<%# Eval("IceId") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>
            <br />

            Select Action:
        <asp:RadioButtonList ID="rblChoose" runat="server" OnSelectedIndexChanged="rblChoose_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Selected="True">Approve Return</asp:ListItem>
            <asp:ListItem>Assign To Team</asp:ListItem>
            <asp:ListItem>Drop Ice Time</asp:ListItem>
        </asp:RadioButtonList>
            <br />
            <br />

            <asp:Panel ID="pnlApprove" runat="server" Visible="true">
                <asp:Button ID="btnApprove" runat="server" Text="Approve Selected" OnClick="btnApprove_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlAssign" runat="server" Visible="false">
                <asp:DropDownList ID="ddlTeam" runat="server" DataSourceID="SqlDataTeams" DataTextField="UserName" DataValueField="TeamID" AppendDataBoundItems="True">
                    <asp:ListItem Value="-">Please select a team</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblSelectTeamVal" runat="server" Text="Select a team" ForeColor="Red" Visible="false"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblComment" runat="server" Text="Comments:"></asp:Label><br />
                <asp:TextBox ID="txbAssignComment" runat="server" MaxLength="2000" Height="50px" Width="300px" TextMode="MultiLine"></asp:TextBox><br />
                <br />
                <asp:Button ID="btnAssign" runat="server" Text="Assign Selected to Team" OnClick="btnAssign_Click" />
            </asp:Panel>

            <asp:Panel ID="pnlDrop" runat="server" Visible="false">
                <asp:Button ID="btnDrop" runat="server" Text="Drop Selected" OnClick="btnDrop_Click" />
            </asp:Panel>

            <br />
            <asp:Label ID="lblResult" CssClass="lblResult" runat="server" Text=""></asp:Label>

        </div>
    </asp:Panel>


    <asp:SqlDataSource ID="ReturnedIceDataSrc" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT 
        Locations.FullName AS Location,
        FORMAT (IceTimes.Date, 'ddd, d MMM') AS 'Date',
        RIGHT(LEFT (IceTimes.StartTime, 19),7) AS 'Start Time',
        RIGHT(LEFT (IceTimes.EndTime, 19),7) AS 'End Time',
        IceTimes.TimeLength AS 'Duration (min)',
        IceTimes.IceType AS 'Ice Type',
        Teams.UserName AS 'Returner',
        ReturnedIce.IceId 
        FROM ReturnedIce 
        INNER JOIN IceTimes ON ReturnedIce.IceId = IceTimes.IceId 
        INNER JOIN Locations ON IceTimes.LocationId = Locations.LocationId
        INNER JOIN Teams ON ReturnedIce.ReturnerId = Teams.TeamId
        WHERE ReturnedIce.IsApproved = 'false'
        AND ReturnedIce.Status = 'Open'"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataTeams" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" SelectCommand="SELECT [UserName], [TeamID] FROM [Teams]"></asp:SqlDataSource>

</asp:Content>
