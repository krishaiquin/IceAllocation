
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailForm.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="_Default" %>
 
<asp:Content ContentPlaceHolderID="title" runat="server">
    <h1>Seafair Minor Hockey</h1>
</asp:Content>

<asp:Content ContentPlaceHolderID="body" runat="server">
    This is the body.
</asp:Content>

<asp:Content ContentPlaceHolderId="ContactUs" runat="server">
    <div id="ContactUsForm">
        <h2>Contact Us</h2>
        <br />
        <table>
            <tr>
                <td align="right">
                    To:</td>
                <td>
                    <asp:TextBox ID="txtTo"
                                    runat="server"
                                    Columns="50"></asp:TextBox>
                </td>
            </tr>
            <!-- Name -->
            <tr>
                <td align="right">
                    Name:</td>
                <td>
                    <asp:TextBox ID="txtName"
                                    runat="server"
                                    Columns="50"></asp:TextBox>
                </td>
            </tr>
 
            <!-- Subject -->
            <tr>
                <td align="right">
                    Subject:
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubject" runat="server">
                        <asp:ListItem>Ask a question</asp:ListItem>
                        <asp:ListItem>Report a bug</asp:ListItem>
                        <asp:ListItem>Customer support ticket</asp:ListItem>
                        <asp:ListItem>Other</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
 
            <!-- Message -->
            <tr>
                <td align="right">
                    Message:
                </td>
                <td>
                    <asp:TextBox ID="txtMessage"
                                    runat="server"
                                    Columns="40"
                                    Rows="6"
                                    TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
 
            <!-- Submit -->
            <tr align="center">
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                        onclick="btnSubmit_Click" />
                </td>
            </tr>
             
            <!-- Results -->
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblResult" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

