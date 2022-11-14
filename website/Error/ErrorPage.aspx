<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" MasterPageFile="~/MasterPage/MasterPage.master" Inherits="Error_ErrorPage" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">

     <h1>An Error Has Occured</h1>
     <br />
     An unexpected error has occured on our website. Please return to your home page and try the action again.
     <br />
     <asp:HyperLink ID="hlHome" runat="server">Home</asp:HyperLink>


     </asp:Content>