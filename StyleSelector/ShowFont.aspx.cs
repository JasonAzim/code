using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Text;

public partial class ShowFont : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DisplayFonts();
    }

    private void DisplayFonts()
    {
        string test = "<td><span style=\"font-family: {0}\">{1}</span></td>";
        string output = string.Empty;
        output = "<table><tr>";

        InstalledFontCollection fonts = new InstalledFontCollection();
        try
        {
            foreach (FontFamily font in fonts.Families)
            {
                output += string.Format(test, font.Name, font.Name);
            }
        }
        catch
        {

        }
        output = "</tr></table>";
        Response.Write(output);
    }
}
