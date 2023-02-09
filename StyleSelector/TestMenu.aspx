<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestMenu.aspx.cs" Inherits="TestMenu" %>

<%@ Register src="UserControls/TabControl.ascx" tagname="TabControl" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/common.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <uc1:TabControl ID="TabControl1" runat="server" />
        
    </div>
    </form>
</body>
</html>
