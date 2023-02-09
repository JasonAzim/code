<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControl.ascx.cs" Inherits="UserControls_TabControl" %>

<asp:Repeater id="Repeater1" runat="server">
   <HeaderTemplate>
   <table border="1" cellpadding="4" cellspacing="0">
   <tr>
      <th>Company</th>
      <th>Symbol</th>
   </tr>
   </HeaderTemplate>
   <ItemTemplate>
          <tr>
            <td> <%# DataBinder.Eval(Container.DataItem, "Name") %></td>
            <td> <%# DataBinder.Eval(Container.DataItem, "Ticker") %></td>
          </tr>
        </ItemTemplate>
  <FooterTemplate>
          </table>
  </FooterTemplate>
</asp:Repeater>
 