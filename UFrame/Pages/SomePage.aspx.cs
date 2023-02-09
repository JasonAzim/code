using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;

public partial class SomPage : System.Web.UI.Page
{
    protected override void AddedControl(Control control, int index)
    {
        if (control is HtmlForm)
        {
            foreach (Control c in control.Controls)
            {
                if (c.ID != null && c.ID.Length > 0)
                {
                    c.ID = Request["ID"] + c.ID;
                }
            }
        }
        base.AddedControl(control, index);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Thread.Sleep(500);
        PostedData.Text = "Query String: " + Request.QueryString.ToString();
        if (Page.IsPostBack)
        {
            PostbackLabel.Visible = true;
            foreach (string key in Request.Form.Keys)
            {
                string value = Request.Form[key];

                PostedData.Text += key + " = " + value + Environment.NewLine;
            }
        }

        this.LoadDataGrid();
    }

    
    private void LoadDataGrid()
    {
        SomeGrid.DataSource = new SomeData[] {
            new SomeData("Omar", "Zabir", 25),
            new SomeData("Bill", "Gates", 50)
        };

        SomeGrid.DataBind();
    }
    protected void SomeGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        this.SomeGrid.EditItemIndex = e.Item.ItemIndex;
        this.SomeGrid.DataBind();
    }
    protected void SomeGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {
        this.SomeGrid.EditItemIndex = -1;
        this.SomeGrid.DataBind();
    }


    protected void Country_SelectedIndexChanged(object sender, EventArgs e)
    {
        State.Enabled = Country.SelectedValue == "USA";
    }
}
