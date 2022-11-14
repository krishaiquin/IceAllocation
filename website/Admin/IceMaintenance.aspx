<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IceMaintenance.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_IceMaintenance" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
<!--Author: Krisha-->

    <div>
        <h3>Edit Ice</h3>
        <table class="auto-style1">
            <tr>
                <td>Location: </td>
                <td>
                    <asp:Label ID="lblLoc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>Date: </td>
                <td>
                    <asp:Calendar ID="calSched" runat="server"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td>Start Time:</td>
                <td>
                    <asp:DropDownList ID="ddlStart" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStart_SelectedIndexChanged">
                        <asp:ListItem >00:00</asp:ListItem>
                        <asp:ListItem>00:15</asp:ListItem>
                        <asp:ListItem>00:30</asp:ListItem>
                        <asp:ListItem>00:45</asp:ListItem>
                        <asp:ListItem>01:00</asp:ListItem>
                        <asp:ListItem>01:15</asp:ListItem>
                        <asp:ListItem>01:30</asp:ListItem>
                        <asp:ListItem>01:45</asp:ListItem>
                        <asp:ListItem>02:00</asp:ListItem>
                        <asp:ListItem>02:15</asp:ListItem>
                        <asp:ListItem>02:30</asp:ListItem>
                        <asp:ListItem>02:45</asp:ListItem>
                        <asp:ListItem>03:00</asp:ListItem>
                        <asp:ListItem>03:15</asp:ListItem>
                        <asp:ListItem>03:30</asp:ListItem>
                        <asp:ListItem>03:45</asp:ListItem>
                        <asp:ListItem>04:00</asp:ListItem>
                        <asp:ListItem>04:15</asp:ListItem>
                        <asp:ListItem>04:30</asp:ListItem>
                        <asp:ListItem>04:45</asp:ListItem>
                        <asp:ListItem>05:00</asp:ListItem>
                        <asp:ListItem>05:15</asp:ListItem>
                        <asp:ListItem>05:30</asp:ListItem>
                        <asp:ListItem>05:45</asp:ListItem>
                        <asp:ListItem>06:00</asp:ListItem>
                        <asp:ListItem>06:15</asp:ListItem>
                        <asp:ListItem>06:30</asp:ListItem>
                        <asp:ListItem>06:45</asp:ListItem>
                        <asp:ListItem>07:00</asp:ListItem>
                        <asp:ListItem>07:15</asp:ListItem>
                        <asp:ListItem>07:30</asp:ListItem>
                        <asp:ListItem>07:45</asp:ListItem>
                        <asp:ListItem>08:00</asp:ListItem>
                        <asp:ListItem>08:15</asp:ListItem>
                        <asp:ListItem>08:30</asp:ListItem>
                        <asp:ListItem>08:45</asp:ListItem>
                        <asp:ListItem>09:00</asp:ListItem>
                        <asp:ListItem>09:15</asp:ListItem>
                        <asp:ListItem>09:30</asp:ListItem>
                        <asp:ListItem>09:45</asp:ListItem>
                        <asp:ListItem>10:00</asp:ListItem>
                        <asp:ListItem>10:15</asp:ListItem>
                        <asp:ListItem>10:30</asp:ListItem>
                        <asp:ListItem>10:45</asp:ListItem>
                        <asp:ListItem>11:00</asp:ListItem>
                        <asp:ListItem>11:15</asp:ListItem>
                        <asp:ListItem>11:30</asp:ListItem>
                        <asp:ListItem>11:45</asp:ListItem>
                        <asp:ListItem>12:00</asp:ListItem>
                        <asp:ListItem>12:15</asp:ListItem>
                        <asp:ListItem>12:30</asp:ListItem>
                        <asp:ListItem>12:45</asp:ListItem>
                        <asp:ListItem>13:00</asp:ListItem>
                        <asp:ListItem>13:15</asp:ListItem>
                        <asp:ListItem>13:30</asp:ListItem>
                        <asp:ListItem>13:45</asp:ListItem>
                        <asp:ListItem>14:00</asp:ListItem>
                        <asp:ListItem>14:15</asp:ListItem>
                        <asp:ListItem>14:30</asp:ListItem>
                        <asp:ListItem>14:45</asp:ListItem>
                        <asp:ListItem>15:00</asp:ListItem>
                        <asp:ListItem>15:15</asp:ListItem>
                        <asp:ListItem>15:30</asp:ListItem>
                        <asp:ListItem>15:45</asp:ListItem>
                        <asp:ListItem>16:00</asp:ListItem>
                        <asp:ListItem>16:15</asp:ListItem>
                        <asp:ListItem>16:30</asp:ListItem>
                        <asp:ListItem>16:45</asp:ListItem>
                        <asp:ListItem>17:00</asp:ListItem>
                        <asp:ListItem>17:15</asp:ListItem>
                        <asp:ListItem>17:30</asp:ListItem>
                        <asp:ListItem>17:45</asp:ListItem>
                        <asp:ListItem>18:00</asp:ListItem>
                        <asp:ListItem>18:15</asp:ListItem>
                        <asp:ListItem>18:30</asp:ListItem>
                        <asp:ListItem>18:45</asp:ListItem>
                        <asp:ListItem>19:00</asp:ListItem>
                        <asp:ListItem>19:15</asp:ListItem>
                        <asp:ListItem>19:30</asp:ListItem>
                        <asp:ListItem>19:45</asp:ListItem>
                        <asp:ListItem>20:00</asp:ListItem>
                        <asp:ListItem>20:15</asp:ListItem>
                        <asp:ListItem>20:30</asp:ListItem>
                        <asp:ListItem>20:45</asp:ListItem>
                        <asp:ListItem>21:00</asp:ListItem>
                        <asp:ListItem>21:15</asp:ListItem>
                        <asp:ListItem>21:30</asp:ListItem>
                        <asp:ListItem>21:45</asp:ListItem>
                        <asp:ListItem>22:00</asp:ListItem>
                        <asp:ListItem>22:15</asp:ListItem>
                        <asp:ListItem>22:30</asp:ListItem>
                        <asp:ListItem>22:45</asp:ListItem>
                        <asp:ListItem>23:00</asp:ListItem>
                        <asp:ListItem>23:15</asp:ListItem>
                        <asp:ListItem>23:30</asp:ListItem>
                        <asp:ListItem>23:45</asp:ListItem>
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>End Time:</td>
                <td>
                    <asp:DropDownList ID="ddlEnd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEndTime_SelectedIndexChanged">
                         <asp:ListItem>00:00</asp:ListItem>
                        <asp:ListItem>00:15</asp:ListItem>
                        <asp:ListItem>00:30</asp:ListItem>
                        <asp:ListItem>00:45</asp:ListItem>
                        <asp:ListItem>01:00</asp:ListItem>
                        <asp:ListItem>01:15</asp:ListItem>
                        <asp:ListItem>01:30</asp:ListItem>
                        <asp:ListItem>01:45</asp:ListItem>
                        <asp:ListItem>02:00</asp:ListItem>
                        <asp:ListItem>02:15</asp:ListItem>
                        <asp:ListItem>02:30</asp:ListItem>
                        <asp:ListItem>02:45</asp:ListItem>
                        <asp:ListItem>03:00</asp:ListItem>
                        <asp:ListItem>03:15</asp:ListItem>
                        <asp:ListItem>03:30</asp:ListItem>
                        <asp:ListItem>03:45</asp:ListItem>
                        <asp:ListItem>04:00</asp:ListItem>
                        <asp:ListItem>04:15</asp:ListItem>
                        <asp:ListItem>04:30</asp:ListItem>
                        <asp:ListItem>04:45</asp:ListItem>
                        <asp:ListItem>05:00</asp:ListItem>
                        <asp:ListItem>05:15</asp:ListItem>
                        <asp:ListItem>05:30</asp:ListItem>
                        <asp:ListItem>05:45</asp:ListItem>
                        <asp:ListItem>06:00</asp:ListItem>
                        <asp:ListItem>06:15</asp:ListItem>
                        <asp:ListItem>06:30</asp:ListItem>
                        <asp:ListItem>06:45</asp:ListItem>
                        <asp:ListItem>07:00</asp:ListItem>
                        <asp:ListItem>07:15</asp:ListItem>
                        <asp:ListItem>07:30</asp:ListItem>
                        <asp:ListItem>07:45</asp:ListItem>
                        <asp:ListItem>08:00</asp:ListItem>
                        <asp:ListItem>08:15</asp:ListItem>
                        <asp:ListItem>08:30</asp:ListItem>
                        <asp:ListItem>08:45</asp:ListItem>
                        <asp:ListItem>09:00</asp:ListItem>
                        <asp:ListItem>09:15</asp:ListItem>
                        <asp:ListItem>09:30</asp:ListItem>
                        <asp:ListItem>09:45</asp:ListItem>
                        <asp:ListItem>10:00</asp:ListItem>
                        <asp:ListItem>10:15</asp:ListItem>
                        <asp:ListItem>10:30</asp:ListItem>
                        <asp:ListItem>10:45</asp:ListItem>
                        <asp:ListItem>11:00</asp:ListItem>
                        <asp:ListItem>11:15</asp:ListItem>
                        <asp:ListItem>11:30</asp:ListItem>
                        <asp:ListItem>11:45</asp:ListItem>
                        <asp:ListItem>12:00</asp:ListItem>
                        <asp:ListItem>12:15</asp:ListItem>
                        <asp:ListItem>12:30</asp:ListItem>
                        <asp:ListItem>12:45</asp:ListItem>
                        <asp:ListItem>13:00</asp:ListItem>
                        <asp:ListItem>13:15</asp:ListItem>
                        <asp:ListItem>13:30</asp:ListItem>
                        <asp:ListItem>13:45</asp:ListItem>
                        <asp:ListItem>14:00</asp:ListItem>
                        <asp:ListItem>14:15</asp:ListItem>
                        <asp:ListItem>14:30</asp:ListItem>
                        <asp:ListItem>14:45</asp:ListItem>
                        <asp:ListItem>15:00</asp:ListItem>
                        <asp:ListItem>15:15</asp:ListItem>
                        <asp:ListItem>15:30</asp:ListItem>
                        <asp:ListItem>15:45</asp:ListItem>
                        <asp:ListItem>16:00</asp:ListItem>
                        <asp:ListItem>16:15</asp:ListItem>
                        <asp:ListItem>16:30</asp:ListItem>
                        <asp:ListItem>16:45</asp:ListItem>
                        <asp:ListItem>17:00</asp:ListItem>
                        <asp:ListItem>17:15</asp:ListItem>
                        <asp:ListItem>17:30</asp:ListItem>
                        <asp:ListItem>17:45</asp:ListItem>
                        <asp:ListItem>18:00</asp:ListItem>
                        <asp:ListItem>18:15</asp:ListItem>
                        <asp:ListItem>18:30</asp:ListItem>
                        <asp:ListItem>18:45</asp:ListItem>
                        <asp:ListItem>19:00</asp:ListItem>
                        <asp:ListItem>19:15</asp:ListItem>
                        <asp:ListItem>19:30</asp:ListItem>
                        <asp:ListItem>19:45</asp:ListItem>
                        <asp:ListItem>20:00</asp:ListItem>
                        <asp:ListItem>20:15</asp:ListItem>
                        <asp:ListItem>20:30</asp:ListItem>
                        <asp:ListItem>20:45</asp:ListItem>
                        <asp:ListItem>21:00</asp:ListItem>
                        <asp:ListItem>21:15</asp:ListItem>
                        <asp:ListItem>21:30</asp:ListItem>
                        <asp:ListItem>21:45</asp:ListItem>
                        <asp:ListItem>22:00</asp:ListItem>
                        <asp:ListItem>22:15</asp:ListItem>
                        <asp:ListItem>22:30</asp:ListItem>
                        <asp:ListItem>22:45</asp:ListItem>
                        <asp:ListItem>23:00</asp:ListItem>
                        <asp:ListItem>23:15</asp:ListItem>
                        <asp:ListItem>23:30</asp:ListItem>
                        <asp:ListItem>23:45</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Duration:
                </td>
                <td>
                    <asp:Label ID="lblDur" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Ice Type: </td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem>Game</asp:ListItem>
                        <asp:ListItem>Practice</asp:ListItem>
                        <asp:ListItem>Open</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Team: </td>
                <td><asp:DropDownList ID="ddlTeam" runat="server" DataSourceID="teamSql" DataTextField="UserName" DataValueField="UserName"></asp:DropDownList>
                    <asp:CheckBox ID="chkUnassign" runat="server" Text="Unassign" AutoPostBack="True" OnCheckedChanged="chkUnassign_CheckedChanged" />
                </td>
                
            </tr>

        </table><br /><br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <br />
        <asp:Label ID="lblStatus" CssClass="lblResult" runat="server"></asp:Label>
        <asp:SqlDataSource ID="teamSql" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" SelectCommand="SELECT [UserName] FROM [Teams]"></asp:SqlDataSource>

    </div>
    

</asp:Content>
<asp:Content ID="Content1" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>

