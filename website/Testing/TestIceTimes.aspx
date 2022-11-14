<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestIceTimes.aspx.cs" Inherits="Testing_TestIceTimes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtIceId" runat="server"></asp:TextBox>
        <asp:Button ID="btnGetIceTime" runat="server" Text="GetIceData" OnClick="btnGetIceTime_Click" />
        <br />
        <asp:Label ID="lblReturnMsg" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblFullMsg" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:CheckBox ID="cbxGameWindow" runat="server" Text="Is in game Window" />
        <br />
        <asp:Label ID="lblHasConflict" runat="server" Text="Label"></asp:Label>
        <br />
     
    </div>
        
        
    
        
      

    </form>
</body>
</html>
