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
using System.Xml;
using System.Collections.Generic;

public partial class AnotherPage : System.Web.UI.Page
{
    private const string FLICKR_API_KEY = "c705bfbf75e8d40f584c8a946cf0834c";
    private const string MOST_RECENT = "http://www.flickr.com/services/rest/?method=flickr.photos.getRecent&api_key=" + FLICKR_API_KEY;
    private const string INTERESTING = "http://www.flickr.com/services/rest/?method=flickr.interestingness.getList&api_key=" + FLICKR_API_KEY;
    private const string ENTER_TAG = "http://www.flickr.com/services/rest/?method=flickr.photos.search&api_key=" + FLICKR_API_KEY + "&tags=";
    private const string FIND_BY_USERNAME = "http://www.flickr.com/services/rest/?method=flickr.people.findByUsername&api_key=" + FLICKR_API_KEY + "&username=";
    private const string FIND_BY_EMAIL = "http://www.flickr.com/services/rest/?method=flickr.people.findByEmail&api_key=" + FLICKR_API_KEY + "&find_email=";
    private const string PHOTOS_FROM_FLICKR_USER = "http://www.flickr.com/services/rest/?method=flickr.people.getPublicPhotos&api_key=" + FLICKR_API_KEY + "&user_id=";

    private int Columns = 3;
    private int Rows = 3;

    private int PageIndex
    {
        get
        {
            return (int)(ViewState[this.ClientID + "_PageIndex"] ?? 0);
        }
        set { ViewState[this.ClientID + "_PageIndex"] = value; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    private XmlDocument LoadPictures()
    {
        string url = INTERESTING;
        
        XmlDocument cachedXml = Cache[url] as XmlDocument;
        if (null == cachedXml)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(url);
                Cache.Insert(url, doc, null, DateTime.MaxValue, TimeSpan.FromMinutes(10));
                return doc;
            }
            catch
            {
                return null;
            }
        }
        else
        {
            return cachedXml;
        }
    }

    private void ShowPictures(int pageIndex)
    {
        XmlDocument doc = this.LoadPictures();
        if( null == doc ) return;
        
        List<PhotoInfo> photos = new List<PhotoInfo>();
        
        foreach( XmlElement e in doc.DocumentElement.GetElementsByTagName("photo") )
        {
            PhotoInfo photo = new PhotoInfo();

            photo.Id = e.GetAttribute("id");
            photo.Owner = e.GetAttribute("owner");
            photo.Title = e.GetAttribute("title");
            photo.Secret = e.GetAttribute("secret");
            photo.Server = e.GetAttribute("server");
            photo.Farm = e.GetAttribute("Farm");

            photos.Add(photo);
        }
        
        HtmlTable table = new HtmlTable();
        table.Align = "center";
        var row = 0;
        var col = 0;
        var count = 0;

        int picturesPerPage = Rows * Columns;
        photos.RemoveRange(0, this.PageIndex * picturesPerPage);

        if (photos.Count > picturesPerPage)
        {
            photos.RemoveRange(picturesPerPage, photos.Count - picturesPerPage);
            this.ShowNext.Visible = true;
        }
        else
        {
            this.ShowNext.Visible = false;
        }

        foreach( PhotoInfo photo in photos )
        {
            if( col == 0 )
                table.Rows.Add( new HtmlTableRow() );

            var cell = new HtmlTableCell();
            
            var img = new HtmlImage();
            img.Src = photo.PhotoUrl(true);
            img.Width = img.Height = 75;
            img.Border = 0;

            var link = new HtmlGenericControl("a");
            link.Attributes["href"] = photo.PhotoPageUrl;      
            link.Attributes["target"] = "_blank";
            link.Attributes["title"] = photo.Title;
            link.Controls.Add(img);
            
            cell.Controls.Add(link);
            table.Rows[row].Cells.Add(cell);

            col ++;
            if( col == Columns )
            {
                col = 0; row ++;
            }

            count ++;
            
        }

        photoPanel.Controls.Clear();
        photoPanel.Controls.Add(table);

        if( pageIndex == 0 )
        {
            this.ShowPrevious.Visible = false;
            this.ShowNext.Visible = true;
        }
        else
        {
            this.ShowPrevious.Visible = true;
        }
        
    }
    protected void ShowPrevious_Click(object sender, EventArgs e)
    {
        this.PageIndex--;        
    }
    protected void ShowNext_Click(object sender, EventArgs e)
    {
        this.PageIndex++;        
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        this.ShowPictures(this.PageIndex);
    }
}
