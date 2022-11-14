<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateTeam.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_CreateTeam" %>

<asp:Content ContentPlaceHolderId="body" runat="server">
    <div>
        <h3>Create Team</h3>
       <table>
           <tr>
               <td>Team:</td>
               <td>
                   <asp:TextBox ID="txtTeam" runat="server" MaxLength="30"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="teamValidator" runat="server" ErrorMessage="Required. " ForeColor="Red" ControlToValidate="txtTeam"></asp:RequiredFieldValidator>
                   <asp:CustomValidator ID="duplicateNameValidator" runat="server" ErrorMessage="A team with that name already exists" ControlToValidate="txtTeam" ForeColor="Red" OnServerValidate="duplicateNameValidator_ServerValidate"></asp:CustomValidator>

               </td>
           </tr>
           <tr>
               <td>Password:</td>
               <td>
                   <asp:TextBox ID="txtPass" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="passValidator" runat="server" ErrorMessage="Required. " ForeColor="Red" ControlToValidate="txtPass"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="passRegExValidator" runat="server" ErrorMessage="Must be at least 6 characters" ForeColor="Red" ControlToValidate="txtPass" ValidationExpression=".{6,}"></asp:RegularExpressionValidator>

               </td>
           </tr>
           <tr>
               <td>Confirm Password:</td>
               <td>
                   <asp:TextBox ID="txtConfirmPass" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="confirmPassValidator" runat="server" ErrorMessage="Required. " ForeColor="Red" ControlToValidate="txtConfirmPass"></asp:RequiredFieldValidator>
                   <asp:CompareValidator ID="passCompareValidator" runat="server" ErrorMessage="Passwords must match" ForeColor="Red" ControlToValidate="txtConfirmPass" ControlToCompare="txtPass"></asp:CompareValidator>

               </td>
           </tr>
           <tr>
               <td>Coach Email:</td>
               <td>
                   <asp:TextBox ID="txtCoachEmail" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="reqCoachEmail" runat="server" ErrorMessage="Required. " ForeColor="Red" ControlToValidate="txtCoachEmail"></asp:RequiredFieldValidator>
                   <asp:RegularExpressionValidator ID="emailRegExValidator" runat="server" ErrorMessage="Please enter a valid email" ForeColor="Red" ControlToValidate="txtCoachEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
               </td>
           </tr>
           <tr>
               <td>Manager Email:</td>
               <td>
                   <asp:TextBox ID="txtManEmail" runat="server"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="reqManagerEmail" runat="server" ErrorMessage="Required. " ForeColor="Red" ControlToValidate="txtManEmail"></asp:RequiredFieldValidator>

                   <asp:RegularExpressionValidator ID="manEmailRegexValidators" runat="server" ErrorMessage="Please enter a valid email" ForeColor="Red" ControlToValidate="txtManEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
               </td>
           </tr>
       </table>
       <br />
       <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" PostBackUrl="~/CreateTeam" />
        <br />
        <asp:Label ID="errMsg" CssClass="lblResult" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label ID="lblEmailResult" CssClass="lblResult" runat="server" Text=""></asp:Label>
   </div>
</asp:Content>
