<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewSeason.aspx.cs" Inherits="Admin_NewSeason" MasterPageFile="~/MasterPage/MasterPage.master" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
    <!-- JavaScript Author: Krisha-->
    <script type="text/javascript">
        //handles which appropriate message to sho
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            //store the text value of btnDel
            var btnText = document.getElementById('<%=btnDel.ClientID%>').value;
            var message = "";
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            //check what the btnToggle's text value then show the appropriate message
            if (btnText == "Clear Season") {
                message = "Are you sure you want to delete all data?";

            }
            if (btnText == "Clear Teams") {
                message = "Are you sure you want to delete all ice teams from the database?";
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

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <h3>Reset the Season:</h3>
    <br />

    <asp:Label ID="lblInfo" runat="server">This will clear all ice times, returned ice, and requests from the database:</asp:Label><br />
    <asp:Button ID="btnDel" runat="server" Text="Clear Season" OnClick="btnDel_Click" OnClientClick="Confirm()" />

    <br />
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
</asp:Content>
