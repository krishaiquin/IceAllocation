<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_Settings" %>

<asp:Content ContentPlaceHolderId="body" runat="server">
    <!--Author: Krisha-->

    <br />
    <div class="settingsGroup">
        <br />
        <table>
            <tr>
                <td>Email:</td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="emailRequired" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                    <br/>
                    <asp:RegularExpressionValidator ID="emailRegexValid" runat="server" ErrorMessage="Please enter a valid email" ForeColor="Red" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>Current Password:</td>
                <td>
                    <asp:TextBox ID="currentPass" runat="server"  OnTextChanged="currentPass_TextChanged" AutoPostBack="true"></asp:TextBox><br />
                    <asp:Label ID="lblWarning" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                   New password
                </td>
                <td>
                   <asp:TextBox ID="txtNewPass" runat="server" Enabled="False"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="passRequired" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtNewPass" Enabled="False"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="passRegexValid" runat="server" ErrorMessage="Must be at least 6 characters long" ControlToValidate="txtNewPass" ForeColor="Red" ValidationExpression=".{6,}"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Confirm password
                </td>
                <td>
                    <asp:TextBox ID="txtConfirmPass" runat="server" Enabled="False"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="confirmRequired" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtconfirmPass" Enabled="False"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="confirmCompareValidator" runat="server" ErrorMessage="Passwords must match" ForeColor="Red" ControlToValidate="txtConfirmPass" ControlToCompare="txtNewPass"></asp:CompareValidator>
                </td>
            </tr>
        </table>
        <br />
           
        <asp:Button ID="btnSubmit" runat="server" Text="Save changes" OnClick="btnSubmit_Click" />
        <br />
        <asp:Label ID="lblSuccess" runat="server"></asp:Label>


    </div>
    
</asp:Content>
