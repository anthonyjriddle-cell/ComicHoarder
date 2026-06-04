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
            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var marvelSpan = pageDocument.DocumentNode
                .SelectNodes("//span")
                ?.FirstOrDefault(x => x.InnerText.StartsWith("MARVEL COMICS"));

            if (marvelSpan == null)
                return new List<string>();

            var links = marvelSpan.ParentNode.NextSibling
                .Descendants("a")
                .Where(a => a.Attributes["href"]?.Value.Contains("https://getcomics.org/marvel/") == true)
                .Select(a => a.Attributes["href"].Value)
                .ToList();

            return links;
        }

        public static DownloadLink GetDownloadLinkFromHtml(string pageContents)
        {
            var result = new DownloadLink();
            var comicDocument = new HtmlDocument();
            comicDocument.LoadHtml(pageContents);

            var titleNode = comicDocument.DocumentNode.SelectSingleNode("//h1[@class='post-title']");
            result.ComicName = titleNode != null
                ? System.Web.HttpUtility.HtmlDecode(titleNode.InnerText)
                : string.Empty;

            var downloadLink = comicDocument.DocumentNode
                .SelectNodes("//a")
                ?.FirstOrDefault(a => string.Equals(
                    a.Attributes["Title"]?.Value,
                    "Download Now",
                    StringComparison.OrdinalIgnoreCase));

            if (downloadLink != null)
                result.Link = downloadLink.Attributes["href"].Value;

            return result;
        }
    }
}
