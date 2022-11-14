<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Login" %>


<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
<!--Author: Krisha-->
    <title></title>
</asp:Content>



<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <!--Login Controller-->
    <div>
        <div id="loginContainer">
            <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Home">
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse: collapse; width: 400px">
                        <tr>
                            <td align="center">
                                <table cellpadding="0" style="align-content: center;">
                                    <tr>
                                        <td align="center" colspan="2">Team Log-In<td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Team:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="UserName" runat="server" DataSourceID="SqlDataSource1" DataTextField="UserName" DataValueField="UserName"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="Team is required." ToolTip="Team name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="color: Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" style="color: Blue;">
                                            <asp:HyperLink ID="PasswordRecovery" runat="server" NavigateUrl="~/RecoverPassword">&emsp;Forgot Password?</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>
        </div>

    </div>


    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" SelectCommand="SELECT Teams.UserName FROM Teams INNER JOIN aspnet_Membership ON Teams.UserId = aspnet_Membership.UserId WHERE (Teams.UserName NOT LIKE '%' + @UserName + '%') AND (aspnet_Membership.IsApproved = 1) ORDER BY Teams.UserName">
        <SelectParameters>
            <asp:Parameter DefaultValue="admin" Name="UserName" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/AdminLogin.aspx">Admin Log In</asp:HyperLink>
    <br />
</asp:Content>
