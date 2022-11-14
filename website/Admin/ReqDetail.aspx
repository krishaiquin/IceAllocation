<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReqDetail.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_ReqDetail" %>

<asp:Content ID="head" runat="server" ContentPlaceHolderID="head">
<!--Author: Krisha-->

    <script type="text/javascript">
        //client side event that fires when cancel button is clicked
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            //alerts the user the button is clicked
            if (confirm("Are you sure you want to cancel this Request?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }

            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h3>Ice Information:</h3>

    <!--Information about the clicked requested ice time-->
    <table class="reqDetailTable">
        <tr>
            <td>Location:</td>
            <td>
                <asp:Label ID="lblLoc" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Date:</td>
            <td>
                <asp:Label ID="lblDate" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Duration:</td>
            <td>
                <asp:Label ID="lblDur" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Ice Type:</td>
            <td>
                <asp:Label ID="lblType" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Date Returned:</td>
            <td>
                <asp:Label ID="lblDateRet" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Returned by:</td>
            <td>
                <asp:Label ID="lblRetTeam" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
      <div class="helpItem">
    <h3>Requests:</h3>

    <!--Table/gridview that shows the team/s who requested for the clicked ice time-->
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSourceGetRequests" AutoGenerateColumns="False"
        OnRowCommand="AssignIce_RowCommand" DataKeyNames="TeamId"
        CssClass="gridviewStyle"
        FooterStyle-CssClass="footerStyle"
        HeaderStyle-CssClass="headerStyle"
        AlternatingRowStyle-CssClass="alt"
        CellPadding="4">
        <EmptyDataRowStyle BorderColor="White" BorderStyle="None" BorderWidth="0px" />
        <EmptyDataTemplate>There are no requests.</EmptyDataTemplate>
        <Columns>
            <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
            <asp:BoundField DataField="GameBalance" HeaderText="Game Balance" />
            <asp:BoundField DataField="PracticeBalance" HeaderText="Practice Balance" />
            <asp:TemplateField HeaderText="Comment">
                <ItemTemplate>
                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnAssign" runat="server" Text="Assign" CommandName="AssignIce" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="TeamId" HeaderText="TeamId" SortExpression="TeamId" ReadOnly="True" Visible="False" />
            <asp:BoundField DataField="IceId" HeaderText="IceId" ReadOnly="True" SortExpression="IceId" Visible="False" />

        </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="lblComment" runat="server">Comment:</asp:Label>
    <asp:TextBox ID="txtCommentAll" runat="server" Rows="2" TextMode="MultiLine"></asp:TextBox>
          <br />
        <asp:CheckBox ID="chkToggle" runat="server" Text="Include comments in notification to teams." />

    <br />
    <br />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel this Ice Time" OnClick="btnCancel_Click" OnClientClick="Confirm()" />
    <br />

    <asp:Label ID="lblResult" runat="server" CssClass="lblResult" Text="Label" Visible="false">Results:</asp:Label>
           </div>



    <br />
    <br />
    <div class="helpItem">
    <h3>Non-Request Assignment (Ignores Ice Balance):</h3>
   

    <asp:DropDownList ID="ddlNonRequestTeam" runat="server" DataSourceID="SqlDataSourceGetAllTeams" DataTextField="TeamInfo" DataValueField="TeamId"></asp:DropDownList>


    <br />
    <asp:Label ID="lblComment0" runat="server">Comment:</asp:Label>
    <asp:TextBox ID="txtCommentNonRequest" runat="server" Rows="2" TextMode="MultiLine"></asp:TextBox>
    <br />
    <asp:CheckBox ID="chkNonRequestComment" runat="server" Text="Include comment in notification to teams."/>

    <br />
    <asp:Button ID="btnAssignToNonRequest" runat="server" OnClick="btnAssignToNonRequest_Click" Text="Assign" />
    <br />
    <asp:Label ID="lblNonRequestResult" runat="server" Text=""></asp:Label>
        </div>
    <asp:SqlDataSource ID="SqlDataSourceGetRequests" 
        runat="server" 
        ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        SelectCommand="SELECT Teams.UserName, Requests.TeamId, Requests.IceId, Teams.GameBalance, Teams.PracticeBalance FROM Requests INNER JOIN Teams ON Requests.TeamId = Teams.TeamID WHERE (Requests.IceId = @IceId) AND Status = 'Open'">
        <SelectParameters>
            <asp:SessionParameter 
                Name="IceId" 
                SessionField="IceIdDetails" 
                Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>

  

    <asp:SqlDataSource ID="SqlDataSourceGetAllTeams" 
        runat="server" 
        ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>" 
        SelectCommand="SELECT [TeamId], [UserName] + '  (G: '+ cast([GameBalance] as varchar)+ '  P:'+ cast([PracticeBalance] as varchar)+')'
AS TeamInfo FROM [Teams]">
    </asp:SqlDataSource>

</asp:Content>


