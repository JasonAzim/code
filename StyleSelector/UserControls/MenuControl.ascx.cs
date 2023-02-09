using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class UserControls_MenuControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string CurrentPage = GetCurrentPageName();

            ArrayList values = new ArrayList();

            MenuData Tab1 = new MenuData();
            Tab1.AddMenuItem("current", "Layout.aspx", "Layout", CurrentPage);
            values.Add(Tab1);

            MenuData Tab2 = new MenuData();
            Tab2.AddMenuItem("glossymenu", "WellProfile.aspx", "Profile", CurrentPage);
            values.Add(Tab2);

            Repeater1.DataSource = values;
            Repeater1.DataBind();
        }
    }

    public string GetRawMenu()
    {
        return "";
    }

    public string GetCurrentPageName() 
    { 
        string sPath = System.Web.HttpContext.Current.Request.Url.AbsolutePath; 
        System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath); 
        string sRet = oInfo.Name; return sRet; 
    }

    protected string GenerateMenuItem(object dataItem)
    {

        bool active = (bool)DataBinder.Eval(dataItem, "Active");
        string MenuLink = (string)DataBinder.Eval(dataItem, "MenuLink");
        string MenuText = (string)DataBinder.Eval(dataItem, "MenuText");


        string ret = string.Empty;
        if (active)
            // Dont refresh the tab. Just show it as active
            ret = "<a><b>" + MenuText + @"</b></a>";
        else
            ret = "<a href=\"" + MenuLink + "\"" + @"><b>" + MenuText + @"</b></a>";

        return ret;
    }

}
