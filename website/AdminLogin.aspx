<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="AdminLogin" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
<!--Author: Krisha-->
    <title>Seafair Minor Hockey</title>
</asp:Content>

 <asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
        <!--Login Controller-->
        <div id="loginContainer">
            <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/Default">
                <LayoutTemplate>
                    <table cellpadding="1" cellspacing="0" style="border-collapse:collapse; width:400px">
                        <tr>
                            <td align="center" >
                                <table cellpadding="0" style="align-content:center;">
                                    <tr>
                                        <td align="center" colspan="2">Admin Log-In<td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name: </asp:Label>
                                        </td>
                                        <td align="left">
                                           
                                           <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
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
                                        <td align="center" colspan="2" style="color:Red;">
                                            <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center" style="color:Blue;">
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
                    <br />
                </LayoutTemplate>
            </asp:Login>
        </div>
    

        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx" Text="Team Log In">Team Log In</asp:HyperLink>
</asp:Content>