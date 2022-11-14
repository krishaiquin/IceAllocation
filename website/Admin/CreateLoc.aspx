<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateLoc.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Admin_CreateLoc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
   <!--Author: Adrian-->
     <h3>Edit Locations</h3>

    <div>
        <asp:ListView ID="LocationListView" runat="server" DataSourceID="Locations" InsertItemPosition="LastItem"
            DataKeyNames="LocationId" OnItemCommand="LocationListView_ItemCommand">
            <EditItemTemplate>
                <tr style="">
                    <td>
                        <asp:TextBox ID="EditLocationNameTextBox" runat="server" Text='<%# Bind("LocationName") %>' /><br />
                        <asp:RequiredFieldValidator ID="EditLocationValidator" runat="server" ErrorMessage="Please enter a location" ForeColor="Red" ControlToValidate="EditLocationNameTextBox" ValidationGroup="UpdateValidation"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="RinkNameTextBox" runat="server" Text='<%# Bind("RinkName") %>' /><br />
                        <asp:RequiredFieldValidator ID="RinkNameValidator" runat="server" ErrorMessage="Please enter a rink" ForeColor="Red" ControlToValidate="RinkNameTextBox" ValidationGroup="UpdateValidation"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Label ID="lblLocationId" Visible="false" runat="server" Text='<%# Eval("LocationId") %>' />
                    </td>
                    <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" ValidationGroup="UpdateValidation" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <InsertItemTemplate>
                <tr style="">
                    <td>
                        <asp:TextBox ID="InsertLocationNameTextBox" runat="server" Text='<%# Bind("LocationName") %>' /><br />
                        <asp:RequiredFieldValidator ID="InsertLocationValidator" runat="server" ErrorMessage="Please enter a location" ForeColor="Red" ControlToValidate="InsertLocationNameTextBox" ValidationGroup="InsertValidation"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:TextBox ID="InsertRinkNameTextBox" runat="server" ControlToValicate="txtRinkName2" Text='<%# Bind("RinkName") %>' /><br />
                        <asp:RequiredFieldValidator ID="InsertRinkValidator" runat="server" ErrorMessage="Please enter a rink" ForeColor="Red" ControlToValidate="InsertRinkNameTextBox" ValidationGroup="InsertValidation"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" ValidationGroup="InsertValidation" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />
                    </td>
                </tr>
            </InsertItemTemplate>
            <ItemTemplate>
                <tr style="">
                    <td>
                        <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("LocationName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblRinkName" runat="server" Text='<%# Eval("RinkName") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblLocationId" Visible="false" runat="server" Text='<%# Eval("LocationId") %>' />
                    </td>
                    <td>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="DeleteIce" />
                    </td>
                </tr>
            </ItemTemplate>
            <LayoutTemplate>
                <table runat="server">
                    <tr runat="server">
                        <td runat="server">
                            <table id="itemPlaceholderContainer" runat="server" border="0" style="">
                                <tr runat="server" style="">

                                    <th runat="server">Location</th>
                                    <th runat="server">Rink</th>
                                    <th runat="server" visible="false">LocationId</th>
                                    <th runat="server"></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td runat="server" style=""></td>
                    </tr>
                </table>
            </LayoutTemplate>
        </asp:ListView>
    </div>

    <asp:Label ID="lblResult" CssClass="lblResult" runat="server" Text="" ></asp:Label>

    <asp:SqlDataSource ID="Locations" runat="server" ConnectionString="<%$ ConnectionStrings:IceAllocationConnectionString %>"
        InsertCommand="INSERT INTO [Locations] ([LocationName], [RinkName], [FullName]) VALUES (@LocationName, @RinkName, @LocationName + ' - ' + @RinkName)"
        SelectCommand="SELECT LocationName, RinkName, LocationId FROM Locations"
        UpdateCommand="UPDATE [Locations] SET [LocationName] = @LocationName, [RinkName] = @RinkName, [FullName] = @LocationName + ' - ' + @RinkName WHERE [LocationId] = @LocationId">
        <InsertParameters>
            <asp:Parameter Name="LocationName" Type="String" />
            <asp:Parameter Name="RinkName" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="LocationName" Type="String" />
            <asp:Parameter Name="RinkName" Type="String" />
            <asp:Parameter Name="LocationId" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

</asp:Content>
