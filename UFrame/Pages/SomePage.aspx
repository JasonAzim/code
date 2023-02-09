<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SomePage.aspx.cs" Inherits="SomPage" %>
<%@ OutputCache NoStore="true" Location="None" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    
    <script src="../Javascripts/jquery-1.2.3.js" type="text/javascript"></script>
    <script src="SomePage.js" type="text/javascript"></script>
    <script type="text/javascript">
    $(document).ready(function()
    {
        var messageId = "#message_<%= Request["ID"] %>";
        if( typeof thisIsSomeFunction == "function" ) 
            $(messageId)
                .css("color", "green")
                .text("Works!");
        else 
            $(messageId)
                .css("color", "red")
                .text("External Javascript Failed to load or failed to initialize before inline Javascript evaluated!");
    });
    </script>
    
    <link rel="Stylesheet" type="text/css" href="SomePage.css" />
    <style type="text/css">
    #somePage #somePageForm input { background-color: lightyellow }
    </style>
</head>
<body>
<div id="somePage">
    <h1>This is SomePage.aspx's output</h1>
    <form id="somePageForm" runat="server">        
        <asp:Label runat="server" ID="PostbackLabel" Visible="False" EnableViewState="false" Text="Postback worked!" Font-Bold="true" ForeColor="Red"></asp:Label>
        <p>Testing inline javascript:<span id="message_<%= Request["ID"] %>" ></span></p>         
        
        <p><strong>Test Form submission</strong><br />
        Name: <asp:TextBox runat="server" ID="Name" Text="Omar AL Zabir" /><br />
        Age: <asp:TextBox runat="server" ID="Age" Text="25" MaxLength="3" Columns="3" /><br />
        Country: 
        <asp:DropDownList runat="server" ID="Country" OnSelectedIndexChanged="Country_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="United States" Value="USA" />
            <asp:ListItem Text="Bangladesh" Value="USA" Selected="true" />            
        </asp:DropDownList>
        <br />
        State: <asp:TextBox runat="server" Enabled="false" MaxLength="25" Columns="20" ID="State" /><br />
        
        <asp:Button ID="Submit" runat="server" OnClientClick="thisIsSomeFunction()" Text="OK" /><small>&lt;-- Click this button</small>
        </p>
        
        <h2>Test Data Grid</h2>
        <asp:DataGrid ID="SomeGrid" runat="server" AutoGenerateColumns="False" 
            BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" 
            CellPadding="2" ForeColor="Black" GridLines="None" 
            oneditcommand="SomeGrid_EditCommand" 
            oncancelcommand="SomeGrid_CancelCommand">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" 
                HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:BoundColumn DataField="FirstName" HeaderText="First" />
                <asp:BoundColumn DataField="LastName" HeaderText="Last" />
                <asp:BoundColumn DataField="Age" HeaderText="Age" />
                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Action" ButtonType="LinkButton"
                    UpdateText="Update" />
            </Columns>
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
        </asp:DataGrid>

        <h2>Request Data</h2>        
        <pre style="overflow:scroll">
<asp:Literal runat="server" ID="PostedData" EnableViewState="false" />
        </pre>
    </form>
</div>
</body>
</html>
