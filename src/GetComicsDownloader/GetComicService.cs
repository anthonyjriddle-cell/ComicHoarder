using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetComicsDownload
{
    public class GetComicService
    {

        public async Task<string> GetHtmlFromUrl(string url, HttpClient client)
        {
            var response = await client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public List<string> GetPageUrlsFromHtml(string pageContents)
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

        public string GetDownLinkFromHtml(string pageContents)
        {
            var result = "";

            HtmlDocument comicDocument = new HtmlDocument();
            comicDocument.LoadHtml(pageContents);

            var allLinks = comicDocument.DocumentNode.SelectNodes("//a");

            foreach (var allLink in allLinks)
            {
                if (allLink.Attributes["Title"] != null)
                {
                    if (allLink.Attributes["Title"].Value == "Download Now")
                    {
                        result = allLink.Attributes["href"].Value;
                    }
                }
            }

            return result;
        }

        public DownloadLink GetDownloadLinkFromHtml(string pageContents)
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

        public async Task<string> DownloadFileFromUrl(string url, string downloadPath, HttpClient client)
        {
            var response = await client.GetAsync(url);
            using (var ms = new MemoryStream())
            {
                var filename = System.Net.WebUtility.UrlDecode(response.RequestMessage.RequestUri.ToString().Split('/').Last());
                filename = Path.Combine(downloadPath, filename);

                await response.Content.CopyToAsync(ms);
                byte[] data;
                data = ms.ToArray();

                using (var fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(data, 0, data.Length);
                }
                return filename;
            }
        }

        public List<DateTime> GetAllWednesdaysBetweenDates(DateTime beginDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime date = endDate.Date;
            while(date.DayOfWeek != DayOfWeek.Wednesday)
            {
                date = date.AddDays(-1);
            }
            while(date > beginDate)
            {
                dates.Add(date);
                date = date.AddDays(-7);
            }
            dates.Reverse();
            return dates;
        }

        public string GenerateUrlFromDate(DateTime date)
        {
            return String.Format("https://getcomics.org/other-comics/{0}-{1}-{2}-weekly-pack/", date.Year, string.Format("{0:00}", date.Month), string.Format("{0:00}", date.Day));
        }
    }
}
