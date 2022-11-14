<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotFound.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Error_ErrorPage" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

     <h1>404 Page Not Found</h1>
     <br />
     The page you were looking for could not be found. Please return to your home page and try the action again.
     <br />
     <asp:HyperLink ID="hlHome" runat="server">Home</asp:HyperLink>


     </asp:Content>