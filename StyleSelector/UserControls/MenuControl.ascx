<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuControl.ascx.cs" Inherits="UserControls_MenuControl" %>

<asp:Repeater id="Repeater1" runat="server">
   <HeaderTemplate>
      <ul class="glossymenu">
   </HeaderTemplate>
   <ItemTemplate>
      <li class="<%# DataBinder.Eval(Container.DataItem, "MenuStyle") %>"> 
         <!-- <a href="<%# DataBinder.Eval(Container.DataItem, "MenuLink") %>"><b><%# DataBinder.Eval(Container.DataItem, "MenuText")%></b></a> -->
         <%# GenerateMenuItem(Container.DataItem)%>
      </li>
   </ItemTemplate>
  <FooterTemplate>
     </ul>
  </FooterTemplate>
</asp:Repeater>
