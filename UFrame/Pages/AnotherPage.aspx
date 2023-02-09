<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AnotherPage.aspx.cs" Inherits="AnotherPage" %>
<%@ OutputCache NoStore="true" Location="None" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
    #flickr a { color: navy }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="flickr">        
        <center>
        <asp:Panel ID="photoPanel" runat="server"></asp:Panel>
        <asp:LinkButton ID="ShowPrevious" runat="server" OnClick="ShowPrevious_Click">&lt; Previous</asp:LinkButton> | <asp:LinkButton ID="ShowNext" runat="server" OnClick="ShowNext_Click" >Next &gt;</asp:LinkButton>
        </center>
    </div>
    </form>
</body>
</html>
