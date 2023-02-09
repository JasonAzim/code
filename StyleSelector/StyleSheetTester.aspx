<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StyleSheetTester.aspx.cs" Inherits="StyleSheetTester" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Stylesheet Tester</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblName" runat="server" Text="Stylesheet"></asp:Label>&nbsp;<asp:DropDownList
            ID="drpStyleName" runat="server" Width="262px">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Text="Control"></asp:Label>
        <asp:DropDownList ID="drpControls" runat="server">
            <asp:ListItem>LinkButton</asp:ListItem>
            <asp:ListItem>Label</asp:ListItem>
            <asp:ListItem>TextBox</asp:ListItem>
            <asp:ListItem>Button</asp:ListItem>
        </asp:DropDownList>
        <asp:Button ID="btnTester" runat="server" Text="Load" OnClick="btnTester_Click" />
        <br />
        <br />
        <asp:Table ID="tblOutput" runat="server" BorderColor="Black" BorderStyle="Solid"
            GridLines="Both" Width="945px">
        </asp:Table>
    </div>
    </form>
</body>
</html>
