using GetComicsDownloader.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetComicsDownloader.Services
{
    public class GetComicsHtmlParser
    {
        public static List<string> GetPageUrlsFromHtml(string pageContents)
        {
            HtmlDocument pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            List<string> comicLinks = new List<string>();
            var dlNodes = pageDocument.DocumentNode.SelectNodes("//span").Where(x => x.InnerText.StartsWith("MARVEL COMICS")).FirstOrDefault().ParentNode.NextSibling.Descendants("a").ToList();

            foreach (var dlNode in dlNodes)
            {
                if (dlNode.Attributes["href"] is not null)
                {
                    if (dlNode.Attributes["href"].Value.Contains("https://getcomics.org/marvel/"))
                    {
                        comicLinks.Add(dlNode.Attributes["href"].Value);
                    }
                }
            }
            return comicLinks;
        }

        public static DownloadLink GetDownloadLinkFromHtml(string pageContents)
        {
            var result = new DownloadLink();

            HtmlDocument comicDocument = new HtmlDocument();
            comicDocument.LoadHtml(pageContents);

            result.ComicName = System.Web.HttpUtility.HtmlDecode(comicDocument.DocumentNode.SelectSingleNode("//h1[@class='post-title']").InnerText);

            var allLinks = comicDocument.DocumentNode.SelectNodes("//a");

            foreach (var allLink in allLinks)
            {
                if (allLink.Attributes["Title"] != null)
                {
                    if (allLink.Attributes["Title"].Value.ToUpper() == "Download Now".ToUpper())
                    {
                        result.Link = allLink.Attributes["href"].Value;
                    }
                }
            }

            return result;
        }
    }
}
