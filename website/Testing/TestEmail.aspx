<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestEmail.aspx.cs" Inherits="TestEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Static sendGmail() Method
            <asp:Table ID="table1" runat="server">
                <asp:TableRow>
                    <asp:TableCell>To</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Subject</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Message</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Send</asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="btnSubmit_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Label ID="lblResult" runat="server" Text="Label"></asp:Label>
        </div>
        <br />

        
        <div>
            <asp:Table ID="table2" runat="server">
                <asp:TableRow>
                    <asp:TableCell>To</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtMailTo" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="Button4" runat="server" Text="Button" OnClick="btnTo_Click" PostBackUrl="~/Testing/TestEmail.aspx" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Bcc</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtBcc" runat="server"></asp:TextBox>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="Button3" runat="server" Text="Button" OnClick="btnBcc_Click" PostBackUrl="~/Testing/TestEmail.aspx" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Subject</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtMailSubject" runat="server"></asp:TextBox>
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:Button ID="Button6" runat="server" Text="subject" OnClick="btnSubject_Click" PostBackUrl="~/Testing/TestEmail.aspx"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Message</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtMailMessage" runat="server"></asp:TextBox>
                    </asp:TableCell>
                      <asp:TableCell>
                        <asp:Button ID="Button5" runat="server" Text="Button" OnClick="btnMessage_Click" PostBackUrl="~/Testing/TestEmail.aspx" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>Send</asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="Button2" runat="server" Text="Button" OnClick="btnSubmitMail_Click" PostBackUrl="~/Testing/TestEmail.aspx" />
                    </asp:TableCell>
                </asp:TableRow>
                <%-- <asp:TableRow>
                    <asp:TableCell>Send</asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="Button7" runat="server" Text="Button" OnClick="btnOtherSubmitMail_Click" />
                    </asp:TableCell>
                </asp:TableRow>--%>
            </asp:Table>

            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="lblNumber" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
