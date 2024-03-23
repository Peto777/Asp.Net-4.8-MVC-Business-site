using System.Collections.Generic;

namespace www.pgsoftweb.sk_2023.Models.News
{
    public class NewsModel
    {
        public List<NewsItem> Items = new List<NewsItem>();
    }

    public class NewsItem
    {
        public string Title;
        public string Summary;
        public string Url;
        public string PublishDate;
        public string Text;
        public string Img;
        public string ImgUrl;
    }
}