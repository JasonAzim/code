using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoWeb
{
    public partial class Ans1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.hidPageStatus.Value = "StartTimer";
                info.Visible = true;
                StartTimer();
            }
            else
            {
            }
        }

        protected void StartTimer()
        {
            string scriptString = "<script language=JavaScript>";
            scriptString += "CountDown();";
            scriptString += "</script>";

            if (!this.ClientScript.IsStartupScriptRegistered("Startup"))
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "Startup", scriptString);
            }
        }

    }
}