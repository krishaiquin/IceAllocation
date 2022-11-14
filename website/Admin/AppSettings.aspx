<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppSettings.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_AppSettings" %>

<asp:Content ContentPlaceHolderID="body" runat="server">
    <br />

    <asp:Label ID="lblWarning" runat="server"></asp:Label>
    <br />
    <asp:Button ID="btnTopSubmit" runat="server" Text="Save Changes" OnClick="Submit_Click" />
    <br />

    <br />

    <div class="settingsGroup">
        <asp:Label ID="lblAnnouncements" runat="server" Text="Update Announcement:" CssClass="settingsLabel" AssociatedControlID="Announcements"></asp:Label><br />
        <asp:TextBox ID="Announcements" runat="server" Height="50" Width="500" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
        <br />
        <asp:Button ID="clearAnnouncement" runat="server" Text="Delete Announcement" OnClick="btn_clearAnnouncement" />
    </div>


    <div class="settingsGroup">
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblShutdownToggle" runat="server" Text="Shutdown site:" AssociatedControlID="shutdownToggleList" CssClass="settingsLabel"></asp:Label></td>
                <td>
                    <asp:Label ID="lblTeamScheduling" runat="server" Text="Disable team scheduling:"  AssociatedControlID="rblTeamScheduling" CssClass="settingsLabel"></asp:Label></td>
            </tr>
            <tr>
                <td>

                    <asp:RadioButtonList ID="shutdownToggleList" runat="server">
                        <asp:ListItem Value="true">Enable Team Interaction</asp:ListItem>
                        <asp:ListItem Value="false">Disable Team Interaction</asp:ListItem>
                    </asp:RadioButtonList>
                    <br />
                    <asp:Label ID="lblAutoOn" runat="server" Text="Automatically turn back on in:" AssociatedControlID="ddlAutoOn" CssClass="settingsLabel"></asp:Label>
        <asp:DropDownList ID="ddlAutoOn" runat="server">
            <asp:ListItem Value="0">Never</asp:ListItem>
            <asp:ListItem Value="1">One Hour</asp:ListItem>
            <asp:ListItem Value="2">Two Hours</asp:ListItem>
            <asp:ListItem Value="3">Three Hours</asp:ListItem>
            <asp:ListItem Value="4">Four Hours</asp:ListItem>
            <asp:ListItem Value="5">Five Hours</asp:ListItem>
        </asp:DropDownList>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblTeamScheduling" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="false">Yes</asp:ListItem>
                        <asp:ListItem Value="true">No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>

    </div>

    <div class="settingsGroup">
        <asp:Label ID="lblReviewing" runat="server" Text="Enable Reviewing:" AssociatedControlID="reviewingToggleList" CssClass="settingsLabel"></asp:Label><br />
        <asp:RadioButtonList ID="reviewingToggleList" runat="server">
            <asp:ListItem Value="true">Enable Reviewing of Ice Time</asp:ListItem>
            <asp:ListItem Value="false">Disable Reviewing of Ice Time</asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <div class="settingsGroup">
        <asp:Label ID="lblGameWindows" runat="server" Text="Game Window:" CssClass="settingsLabel"></asp:Label>
        <table>
            <tr>
                <td></td>
                <td>Weekdays</td>
                <td>Weekends</td>
            </tr>
            <tr>
                <td>Start Time</td>
                <td>
                    <asp:DropDownList ID="ddlWeekDayStart" runat="server"></asp:DropDownList>

                </td>
                <td>
                    <asp:DropDownList ID="ddlWeekEndStart" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>End Time</td>
                <td>
                    <asp:DropDownList ID="ddlWeekDayFinish" runat="server"></asp:DropDownList>

                </td>
                <td>
                    <asp:DropDownList ID="ddlWeekEndFinish" runat="server"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>


    <div class="settingsGroup">
        <asp:Label ID="lblRollOvers" runat="server" Text="Roll Overs: The amount of time prior to start where an unassigned Ice Type will switch to the next level." CssClass="settingsLabel"></asp:Label>
        <br />
        <asp:Label ID="lblRollToPractice" runat="server" Text="Games roll over to Practice at:" AssociatedControlID="ddlRollToPractice" CssClass="settingsLabel"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlRollToPractice" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>24</asp:ListItem>
            <asp:ListItem>36</asp:ListItem>
            <asp:ListItem>48</asp:ListItem>
            <asp:ListItem>60</asp:ListItem>
            <asp:ListItem>72</asp:ListItem>
            <asp:ListItem>84</asp:ListItem>
            <asp:ListItem>96</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="lblRollToOpen" runat="server" Text="Practices roll over to Open at:" AssociatedControlID="ddlRollToOpen" CssClass="settingsLabel"></asp:Label>
        <br />
        <asp:DropDownList ID="ddlRollToOpen" runat="server">
            <asp:ListItem>0</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>24</asp:ListItem>
            <asp:ListItem>36</asp:ListItem>
            <asp:ListItem>48</asp:ListItem>
            <asp:ListItem>60</asp:ListItem>
            <asp:ListItem>72</asp:ListItem>
            <asp:ListItem>84</asp:ListItem>
            <asp:ListItem>96</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="settingsGroup">
        <asp:Label ID="lblLogo" runat="server" Text="Logo Url:" AssociatedControlID="txtLogoUrl" CssClass="settingsLabel"></asp:Label>
        <br />
        <asp:TextBox ID="txtLogoUrl" runat="server" MaxLength="200" Width="571px"></asp:TextBox><asp:CustomValidator ID="urlValidator" runat="server" ErrorMessage="You must enter a valid URL" ControlToValidate="txtLogoUrl" ForeColor="Red" OnServerValidate="urlValidator_ServerValidate" ValidationGroup="HeaderVal"></asp:CustomValidator>
        <br />
               
        <asp:Label ID="lblTitle" runat="server" Text="Website Header:" AssociatedControlID="txtTitle" CssClass="settingsLabel" Visible="false"></asp:Label>
        <asp:TextBox ID="txtTitle" runat="server" Enabled="false" Visible="false" MaxLength="30" Text="Seafair Ice Allocation"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="lblTheme" runat="server" Text="Color theme:" AssociatedControlID="ddlTheme" CssClass="settingsLabel"></asp:Label><br />
        <asp:DropDownList ID="ddlTheme" runat="server">
            <asp:ListItem Value="BlueTheme.css">Blue</asp:ListItem>
            <asp:ListItem Value="RedTheme.css">Red</asp:ListItem>
        </asp:DropDownList>
        <br />
       <asp:Literal ID="ltrlTheme" runat="server">Changes will be visible after navigating away from this settings page.</asp:Literal>
    </div>

    <div class="settingsGroup">
        <asp:Label ID="lblPublicEmail" runat="server" Text="Public Contact Info:" AssociatedControlID="txtPublicEmail" CssClass="settingsLabel"></asp:Label> 
        <br />
        <asp:TextBox ID="txtPublicEmail" runat="server" Width="229px" MaxLength="50"></asp:TextBox>
       
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPublicEmail" ErrorMessage="Please enter a valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNotifications" runat="server" Text="Receive all Notifications:" AssociatedControlID="rblNotifications" CssClass="settingsLabel"></asp:Label>
                </td>
                <td>
                    <asp:RadioButtonList ID="rblNotifications" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="true">Yes</asp:ListItem>
                        <asp:ListItem Value="false">No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
     </div>



    <div class="settingsGroup">
        <asp:Label ID="lblAssignEmail" runat="server" Text="Ice Assigned Email Template:" AssociatedControlID="txtAssignEmail" CssClass="settingsLabel"></asp:Label><br />
        <asp:TextBox ID="txtAssignEmail" runat="server" Height="50" Width="500" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
    </div>

    <div class="settingsGroup">
         <asp:Label ID="lblRejectEmail" runat="server" Text="Ice Rejected Email Template:" AssociatedControlID="txtRejectEmail" CssClass="settingsLabel"></asp:Label><br />
        <asp:TextBox ID="txtRejectEmail" runat="server" Height="50" Width="500" TextMode="MultiLine" MaxLength="1000"></asp:TextBox>
    </div>

    <br />
    <asp:Button ID="Submit" runat="server" Text="Save Changes" OnClick="Submit_Click" /><br />

</asp:Content>
