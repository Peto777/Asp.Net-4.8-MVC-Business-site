using System;
using System.Globalization;
using System.IO;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;
using www.pgsoftweb.sk_2023.Models.News;

namespace www.pgsoftweb.sk_2023.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/IndexSk
        public ActionResult IndexSk()
        {
            NewsModel model = CreateNewsModel("http://blog.pgsoftweb.sk");
            return View(model);
        }

        private NewsModel CreateNewsModel(string blogUrl)
        {
            NewsModel model = new NewsModel();

            try
            {
                XmlReader reader = new MyXmlReader(string.Format("{0}/syndication.axd", blogUrl));
                //XmlReader reader = XmlReader.Create(url);
                //XmlDocument doc = new XmlDocument();
                //doc.Load(reader);
                SyndicationFeed feed = SyndicationFeed.Load(reader);
                reader.Close();

                foreach (SyndicationItem feedItem in feed.Items)
                {
                    NewsItem newsItem = NewsItemFromFeed(feedItem, blogUrl);
                    if (newsItem.Url != null)
                    {
                        model.Items.Add(newsItem);
                    }
                }
            }
            catch (Exception exc)
            {
                exc.ToString();
            }

            return model;
        }

        NewsItem NewsItemFromFeed(SyndicationItem feedItem, string rootUrl)
        {
            NewsItem newsItem = new NewsItem();
            newsItem.Title = feedItem.Title.Text;
            newsItem.Summary = feedItem.Summary.Text.Replace("/content/images/posts", string.Format("{0}/content/images/posts", rootUrl));
            newsItem.Url = feedItem.Links.Count > 0 ? feedItem.Links[0].Uri.ToString() : null;
            newsItem.PublishDate = GetPublishDate(feedItem, string.Format("{0}/post/", rootUrl), newsItem.Url);

            newsItem.Text = newsItem.Summary;
            int imgPos = newsItem.Summary.IndexOf("<p><img");
            if (imgPos > 0)
            {
                int imgEndPos = newsItem.Summary.IndexOf("/></p>", imgPos);
                int posOffset = 6;
                if (imgEndPos < 0)
                {
                    imgEndPos = newsItem.Summary.IndexOf("</img></p>", imgPos);
                    posOffset = 10;
                }

                newsItem.Summary = newsItem.Summary.Substring(0, imgEndPos + posOffset + 1);
                newsItem.Text = newsItem.Summary.Substring(0, imgPos);
                newsItem.Img = newsItem.Summary.Substring(imgPos + 3, imgEndPos + posOffset - imgPos - 3 - 4);
                newsItem.ImgUrl = getImgUrl(newsItem.Img);
            }

            return newsItem;
        }

        string getImgUrl(string img)
        {
            img = img.Trim().Replace('\'', '\"');

            int srcPos = img.IndexOf("src=\"");
            if (srcPos > 0)
            {
                int srcEndPos = img.IndexOf("\"", srcPos + 5);
                if (srcEndPos > 0)
                {
                    return img.Substring(srcPos + 5, srcEndPos - srcPos - 5);
                }
            }
            return string.Empty;
        }

        string GetPublishDate(SyndicationItem feedItem, string rootUrl, string url)
        {
            string result = null;
            try
            {
                string postUrl = url.Substring(rootUrl.Length);
                string[] items = postUrl.Split('/');

                result = string.Format("{0}.{1}.{2}", items[2], items[1], items[0]);
            }
            catch
            {
                result = feedItem.PublishDate.ToString("dd.MM.yyyy");
            }

            return result;
        }
    }

    class MyXmlReader : XmlTextReader
    {
        private bool readingDate = false;
        //const string CustomUtcDateTimeFormat = "ddd MMM dd HH:mm:ss Z yyyy"; // Wed Oct 07 08:00:07 GMT 2009
        const string CustomUtcDateTimeFormat = "ddd, dd MM yyyy HH:mm:ss"; // st, 6 7 2016 21:20:00 +0200

        public MyXmlReader(Stream s) : base(s) { }

        public MyXmlReader(string inputUri) : base(inputUri) { }

        public override void ReadStartElement()
        {
            if (string.Equals(base.NamespaceURI, string.Empty, StringComparison.InvariantCultureIgnoreCase) &&
                (string.Equals(base.LocalName, "lastBuildDate", StringComparison.InvariantCultureIgnoreCase) ||
                string.Equals(base.LocalName, "pubDate", StringComparison.InvariantCultureIgnoreCase)))
            {
                readingDate = true;
            }
            base.ReadStartElement();
        }

        public override void ReadEndElement()
        {
            if (readingDate)
            {
                readingDate = false;
            }
            base.ReadEndElement();
        }

        public override string ReadString()
        {
            if (readingDate)
            {
                DateTime dt = ParseBlogDate(base.ReadString());
                return dt.ToString("R", CultureInfo.InvariantCulture);
            }
            else
            {
                return base.ReadString();
            }
        }

        public DateTime ParseBlogDate(string dateString)
        {
            DateTime dt;
            if (!DateTime.TryParse(dateString, out dt))
            {
                try
                {
                    // st, 6 7 2016 21:20:00 +0200
                    string[] items = dateString.Split(' ');
                    dt = new DateTime(int.Parse(items[3]), int.Parse(items[2]), int.Parse(items[1]));
                }
                catch
                {
                    dt = DateTime.Today;
                }
            }

            return dt;
        }
    }
}
