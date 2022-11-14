<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditTeam.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_EditTeam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
<!--Author: Krisha-->

    <div>
        <h3>Edit Team</h3>
        <table class="auto-style1">
            <tr>
                <td>Team Name:</td>
                <td>
                    <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    Game Balance:
                </td>
                <td>
                    <asp:Label ID="lblGame" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Practice Balance:
                </td>
                <td>
                    <asp:Label ID="lblPrac" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Coach Email:</td>
                <td>
                    <asp:TextBox ID="txtCoachEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredCoachEmail" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCoachEmail" ValidationGroup="requiredGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Manager Email:</td>
                <td>
                    <asp:TextBox ID="txtManEmail" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="requiredManEmail" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtManEmail" ValidationGroup="requiredGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Preferred Contact:</td>
                <td>
                    <asp:DropDownList ID="ddlContact" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlContact_SelectedIndexChanged">
                        <asp:ListItem Value="1">Coach</asp:ListItem>
                        <asp:ListItem Value="2">Manager</asp:ListItem>
                        <asp:ListItem Value="3">Both</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblStatus" CssClass="lblResult" runat="server"></asp:Label><br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" ValidationGroup="requiredGroup" /> &nbsp; <asp:Button ID="btnToggle" runat="server" Text="Disable Team" OnClick="btnToggle_Click" OnClientClick="Confirm()" /><br /><br />

        

    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <!-- JavaScript -->
    <script type = "text/javascript">
        //handles which appropriate message to show
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            //store the text value of btnToggle
            var btnText = document.getElementById('<%=btnToggle.ClientID%>').value;
            var message = "";
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            //check what the btnToggle's text value then show the appropriate message
            if (btnText == "Disable Team") {
                message = "Are you sure you want to disable this team?";

            }
            if (btnText == "Enable Team") {
                message = "Are you sure you want to enable this team?";
            }

            if (confirm(message)) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>

