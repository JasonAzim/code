<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register src="UserControls/Footer.ascx" tagname="Footer" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Application - Default</title>
   <link rel="stylesheet" type="text/css" href="css/common.css" />
</head>
<body>
<form id="form1" runat="server">
<div class="divMain">
    
<table class="tableMain">
<tr>
    <td class="RightTopMenu">
        <b>Logged in as: </b>  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>
        <a href="Logout.aspx" class="signout">Log out</a></b>
    </td>
</tr>
<tr>
    <td>
        <img class="Logo" alt="Logo" src="images/bp-logo_trans.gif" />
        <span class="Caption">Well Log DropBox</span>
    </td>
</tr>
<tr>
    <td class="tdNoBorder">
    <ul class="glossymenu">
       <li class="current"><a><b>Home</b></a></li>
       <li class="glossymenu"><a href="WellProfile.aspx"><b>Profile</b></a></li>	
       <li class="glossymenu"><a href="Suggestion.aspx"><b>Suggestions</b></a></li>	
    </ul>
	</td>
</tr>
<tr>
    <td class="tdColor" align="left">               
    <table class="SubMenu">
    <tr>
        <td><a class="linkLabelMenu">Home</a></td>
        <td><a class="linkLabelMenu">|</a></td>
        <td><a class="linkLabelMenu" href="Explorer.aspx">Upload</a></td>                            
    </tr>
    </table>            
    </td>
</tr>
<tr>
    <td class="tdNoBorder">
    <div class="PageContent">
    <p>The quick brown fox jumped over the lazy dog</p>
    </div>
    </td>
</tr>
<tr>
    <td class="tdNoBorder">&nbsp;</td>
</tr>
<tr>
    <td class="footerContent" valign="middle">
        <uc1:Footer ID="Footer1" runat="server" />
    </td>                       
</tr>
</table>

</div>
</form>
</body>
</html>
