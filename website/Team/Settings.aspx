<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Team_Settings" %>


<asp:Content ContentPlaceHolderID="body" runat="server">
    <!--Author: Krisha-->

    <h3>Settings</h3>
    <div>
        <table style="width: 400px">
            <tr>
                <td>
                    <asp:Label ID="lblWhoToEmail" runat="server" Text="Preferred Contact:" AssociatedControlID="ddlWhoToEmail" CssClass="settingsLabel"></asp:Label>

                </td>
                <td style="margin-left: 40px">
                    <asp:DropDownList ID="ddlWhoToEmail" runat="server">
                        <asp:ListItem Value="1">Coach</asp:ListItem>
                        <asp:ListItem Value="2">Manager</asp:ListItem>
                        <asp:ListItem Value="3">Both</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr>
                <td width="auto">
                    <asp:Label ID="lblCoachEmail" runat="server" Text="Coach E-mail:" AssociatedControlID="txtCoachEmail" CssClass="settingsLabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCoachEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="emailRequired" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCoachEmail"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="emailRegExValidator" runat="server" ErrorMessage="Please enter a valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" ControlToValidate="txtCoachEmail" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblManEmail" runat="server" Text="Manager E-mail:" AssociatedControlID="txtManEmail" CssClass="settingsLabel"></asp:Label>

                </td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="txtManEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="manRequired" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtManEmail"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="manEmailRegexValidator" runat="server" ErrorMessage="Please enter a valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" ControlToValidate="txtManEmail" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCurrent" runat="server" Text="Current password:" AssociatedControlID="txtCurrent" CssClass="settingsLabel"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtCurrent" runat="server" OnTextChanged="txtCurrent_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblNewPass" runat="server" Text="New password:" AssociatedControlID="txtNewPass" CssClass="settingsLabel"></asp:Label>

                </td>
                <td>
                    <asp:TextBox ID="txtNewPass" runat="server" TextMode="Password" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="newPassRequired" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNewPass" Enabled="False"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="newPassRegexValidator" runat="server" ErrorMessage="Must be at least 6 characters long" ForeColor="Red" ControlToValidate="txtNewPass" ValidationExpression=".{6,}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblConfirmPass" runat="server" Text="Confirm new password:" AssociatedControlID="txtConfirmPass" CssClass="settingsLabel"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPass" runat="server" TextMode="Password" Enabled="False"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="confirmRequired" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtConfirmPass" Enabled="False"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="confirmCompareValidator" runat="server" ErrorMessage="Passwords must match" ForeColor="Red" ControlToValidate="txtConfirmPass" ControlToCompare="txtNewPass"></asp:CompareValidator>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblReceiveNotifications" runat="server" Text="Receive all Notifications:" AssociatedControlID="rblReceiveNotifications" CssClass="settingsLabel"></asp:Label>

                </td>
                <td>
                    <asp:RadioButtonList ID="rblReceiveNotifications" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="true">Yes</asp:ListItem>
                        <asp:ListItem Value="false">No</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Save Changes" OnClick="btnSubmit_Click" />
        <br />
        <asp:Label ID="lblResult" runat="server"></asp:Label>
    </div>
</asp:Content>
