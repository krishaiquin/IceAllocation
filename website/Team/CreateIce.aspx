<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateIce.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Team_CreateIce" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
  <!--Author: Adrian-->
      <div>
        <asp:Panel ID="pnlSchedulingOn" runat="server" Visible="false">
        <h3>Enter Schedule</h3>
        <table>
            <tr>
                <td>Date:</td>
                <td>
                    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                    <asp:Label ID="lblCalendarMsg" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Location:</td>
                <td>
                    <asp:DropDownList ID="ddlLocation" runat="server" DataSourceID="SqlDataLocations" DataTextField="Column1" DataValueField="LocationId" AppendDataBoundItems="True" AutoPostBack="True">
                        <asp:ListItem Value="-">Select a location</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblLocationMsg" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Start Time:</td>
                <td>
                    <asp:DropDownList ID="ddlStartTime" runat="server" OnSelectedIndexChanged="ddlStartTime_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>End Time:</td>
                <td>
                    <asp:DropDownList ID="ddlEndTime" runat="server" OnSelectedIndexChanged="ddlEndTime_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Duration:</td>
                <td>
                    <asp:Label ID="lblDuration" runat="server" Text="60 minutes"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ice Type:</td>
                <td>
                    <asp:DropDownList ID="ddlIceType" runat="server">
                        <asp:ListItem Selected="True" Value="-">Select ice type</asp:ListItem>
                        <asp:ListItem>Game</asp:ListItem>
                        <asp:ListItem>Practice</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblTypeMsg" runat="server" Text="" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Number of Ice Times:</td>
                <td>
                    <asp:DropDownList ID="ddlNumIce" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />

        <asp:Button ID="btnGenerate" runat="server" Text="Generate List" OnClick="btnGenerateList_Click" Enabled="True" />
        <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CausesValidation="False" />
        <br />
        <br />

        <asp:Label ID="lblCommonDetails" runat="server" Text="" Visible="false"></asp:Label>
        <br />
        <br />

        <asp:Panel ID="pnlMultiIce" runat="server" Visible="false">
            <asp:Label ID="lblSelInstruct" runat="server" Text="Deselect ice times you cannot use:"></asp:Label>
            <br />
            <asp:CheckBoxList ID="cblMultiIce" runat="server"></asp:CheckBoxList>
            <br />
            <br />
        </asp:Panel>

        <asp:Panel ID="pnlConflicts" runat="server" Visible="false">
            <asp:Label ID="lblCommonConflicts" runat="server" Text="These days have conflicting ice times:"></asp:Label>
            <br />
            <asp:BulletedList ID="blConflicts" runat="server"></asp:BulletedList>
            <br />
            <br />
        </asp:Panel>

        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Visible="false" />
        <br />
        <br />
        <asp:Label ID="lblResult" CssClass="lblResult" runat="server"></asp:Label>

</asp:Panel>
        <asp:Panel ID="pnlSchedulingOff" runat="server" Visible="false">

            <p class="announcement">Entering schedules is currently disabled.</p>

        </asp:Panel>

        <asp:SqlDataSource ID="SqlDataTeams" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" SelectCommand="SELECT [UserName], [TeamID] FROM [Teams]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataLocations" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" SelectCommand="SELECT [LocationId], (CONCAT ( [LocationName],' - ' , [RinkName]))  FROM [Locations]"></asp:SqlDataSource>
    </div>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .auto-style1 {
            height: 32px;
        }
    </style>
</asp:Content>
