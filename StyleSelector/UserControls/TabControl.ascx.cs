using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections; 

public partial class UserControls_TabControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ArrayList values = new ArrayList();
            values.Add(new PositionData("Microsoft", "Msft"));
            values.Add(new PositionData("Intel", "Intc"));
            values.Add(new PositionData("Dell", "Dell"));

            Repeater1.DataSource = values;
            Repeater1.DataBind();
        }
    }

    public class PositionData
    {
        private string name;
        private string ticker;

        public PositionData(string name, string ticker)
        {
            this.name = name;
            this.ticker = ticker;
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string Ticker
        {
            get
            {
                return ticker;
            }
        }
    }

}
