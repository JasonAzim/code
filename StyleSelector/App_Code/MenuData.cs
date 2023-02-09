using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MenuData
/// </summary>
public class MenuData
{
    private bool active;
    private string menuStyle;
    private string menuLink;
    private string menuText;

    public MenuData()
	{
	}

    public bool Active
    {
        get
        {
            return active;
        }
    }

    public string MenuStyle
    {
        get
        {
            return menuStyle;
        }
    }

    public string MenuLink
    {
        get
        {
            return menuLink;
        }
    }

    public string MenuText
    {
        get
        {
            return menuText;
        }
    }

    public void AddMenuItem(string style, string link, string text,string CurrentPage)
    {
        if (CurrentPage.ToUpper() == link.ToUpper())
        {
            this.active = true;
        }
        else
        {
            this.active = false;
        }

        this.menuStyle = style;
        this.menuLink = link;
        this.menuText = text;
    }

    public void AddMenuItemOverride(bool active, string style, string link, string text)
    {
        this.active = active;
        this.menuStyle = style;
        this.menuLink = link;
        this.menuText = text;
    }

}
