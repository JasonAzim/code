using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic; 

public partial class StyleSheetTester : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStyle("Styles");
        }
        else
        {
        }

        AddStyleSheetLink();
    }

    protected void LoadStyle(string StyleDirectory)
    {
        string[] StyleList;
        StyleList = Directory.GetDirectories(Server.MapPath(StyleDirectory));

        string[] files;
        string StyleName = string.Empty;
        string FileName = string.Empty;

        for (int k = 0; k < StyleList.Length; k++)
        {
            files = Directory.GetFiles(StyleList[k]);

            for (int i = 0; i < files.Length; i++)
            {
                // get the last file name
                FileName = files[i];
                string[] Parts = FileName.Split(new char[] { '\\' });
                FileName = Parts[Parts.Length - 1];
                StyleName = Parts[Parts.Length - 2];

                // make file path
                FileName = StyleDirectory + "/" + StyleName + "/" + FileName;
                this.drpStyleName.Items.Add(FileName);
            }
        }
    }

    protected void AddStyleSheetLink()
    {
        HtmlLink cssLink = new HtmlLink();
        cssLink.Href = "~/" + this.drpStyleName.SelectedItem.Text;
        cssLink.Attributes.Add("rel", "stylesheet");
        cssLink.Attributes.Add("type", "text/css");
        Header.Controls.Add(cssLink);
    }

    protected void btnTester_Click(object sender, EventArgs e)
    {
        string FilePath = Server.MapPath(this.drpStyleName.SelectedItem.Text);

        string[] Parts = FilePath.Split(new char[] { '\\' });
        string FileName = Parts[Parts.Length - 1];
        string StyleName = Parts[Parts.Length - 2];

        string Line = string.Empty;
        string ReplaceLine = string.Empty;

        int counter = 0;

        bool ClassTagOpen = false;
        string ClassName = string.Empty;
        string ClassContents = string.Empty;
        Hashtable ClassList = new Hashtable(); 

        try
        {
            // Create an instance of StreamReader to read from a file.
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(FilePath))
            {
                // Read and display lines from the file until the end of 
                // the file is reached.
                while ((Line = sr.ReadLine()) != null)
                {
                    if (Line == "")
                    {
                        // do nothing, ignore blank lines
                    }
                    else if (Regex.IsMatch(Line, @"^[.]"))
                    {
                        counter += 1;

                        ClassName = Line.Replace(".", "");
                    }
                    else if (Regex.IsMatch(Line, @"{"))
                    {
                        ClassTagOpen = true;
                    }
                    else if (Regex.IsMatch(Line, @"}"))
                    {
                        ClassTagOpen = false;

                        ClassList[ClassName] = ClassContents;
                        ClassContents = string.Empty;
                    }
                    else
                    {
                        if (ClassTagOpen)
                        {
                            ReplaceLine = "Styles" + "/" + StyleName + "/" + "images";
                            //Line = Line.Replace("images", ReplaceLine);
                            ClassContents = ClassContents + Line + "<BR>";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Let the user know what went wrong.
            Response.Write("The file could not be read:");
            Response.Write(ex.Message);
        }

        TableRow TR = null;

        counter = 0;
        foreach (DictionaryEntry c in ClassList)
        {
            counter += 1;
            // Response.Write("Class Name = " + c.Key + "<BR><HR>");
            // Response.Write(c.Value);

            TR = new TableRow();

            TableCell TC3 = new TableCell();

            Label TestLabel3 = new Label();
            TestLabel3.Text = c.Key.ToString();
            TC3.Controls.Add(TestLabel3);

            TR.Controls.Add(TC3);

            TableCell TC1 = new TableCell();

            Label TestLabel1 = new Label();
            TestLabel1.Text = c.Value.ToString();
            TestLabel1.ID = "lblClassName_" + counter.ToString();
            TC1.Controls.Add(TestLabel1);

            TR.Controls.Add(TC1);

            TableCell TC2 = new TableCell();

            if (this.drpControls.SelectedItem.Text == "LinkButton")
            {
                LinkButton TestLabel2 = new LinkButton();
                TestLabel2.Text = "This is a test";
                TestLabel2.ID = "TestLabel_" + counter.ToString();
                TestLabel2.CssClass = c.Key.ToString();
                TC2.Controls.Add(TestLabel2);
            }
            else if (this.drpControls.SelectedItem.Text == "Button")
            {
                Button TestLabel2 = new Button();
                TestLabel2.Text = "This is a test";
                TestLabel2.ID = "TestLabel_" + counter.ToString();
                TestLabel2.CssClass = c.Key.ToString();
                TC2.Controls.Add(TestLabel2);
            }
            else if (this.drpControls.SelectedItem.Text == "TextBox")
            {
                TextBox TestLabel2 = new TextBox();
                TestLabel2.Text = "This is a test";
                TestLabel2.ID = "TestLabel_" + counter.ToString();
                TestLabel2.CssClass = c.Key.ToString();
                TC2.Controls.Add(TestLabel2);
            }
            else
            {
                Label TestLabel2 = new Label();
                TestLabel2.Text = "This is a test";
                TestLabel2.ID = "TestLabel_" + counter.ToString();
                TestLabel2.CssClass = c.Key.ToString();
                TC2.Controls.Add(TestLabel2);
            }

            TR.Controls.Add(TC2);


            this.tblOutput.Rows.Add(TR);
        }

    }
}
