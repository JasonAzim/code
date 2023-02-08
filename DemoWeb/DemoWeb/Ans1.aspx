<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ans1.aspx.cs" Inherits="DemoWeb.Ans1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
        var TimerValue = 21;

        function CountDown() {

            if ((TimerValue != null) && (TimerValue != 1)) timerId = setTimeout("CountDown()", 1000);

            var TimerControl = document.getElementById("<%= info.ClientID %>");

            TimerValue = TimerValue - 1;

            TimerControl.value = TimerValue;

         }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width: 100%; border-collapse: collapse">   
        <tr>
           <td>Refresh Timer:&nbsp;&nbsp;
           <asp:TextBox ID="info" runat="server" Font-Bold="False" Visible="false" ForeColor="Black" Width="60px" BorderColor="Blue"></asp:TextBox>&nbsp;&nbsp;
           </td>
           <td>&nbsp;</td>
           <td>&nbsp;</td>
        </tr>
        </table>
        <asp:HiddenField ID="hidPageStatus" runat="server" Value="Initialized" />

    </div>
    </form>
</body>
</html>
