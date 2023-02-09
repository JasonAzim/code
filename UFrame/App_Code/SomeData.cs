using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for SomeData
/// </summary>
public class SomeData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }

    public SomeData(string first, string last, int age)
    {
        this.FirstName = first;
        this.LastName = last;
        this.Age = age;
    }
}
