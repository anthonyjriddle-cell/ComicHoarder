using GetComicsDownloader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetComicsDownloader
{
    public class GetComicsJob
    {
        private readonly GetComicsHttpService _httpService;
        private readonly string _downloadPath;

        public GetComicsJob(GetComicsHttpService httpService, string downloadPath)
        {
            _httpService = httpService;
            _downloadPath = downloadPath;
        }

        public async Task RunAsync()
        {
            var beginDate = DateTime.Parse(File.ReadAllText("LastDate.txt"));
            var dates = GetComicsUrlBuilder.GetAllWednesdaysBetweenDates(beginDate, DateTime.Now);

            if (!dates.Any())
            {
                // log: no comics to get
                return;
            }

            foreach (var date in dates)
            {
                // log: getting comics for date
                var weeklyUrl = GetComicsUrlBuilder.GenerateUrlFromDate(date);
                var weeklyHtml = await _httpService.GetHtmlFromUrl(weeklyUrl);
                var comicPageLinks = GetComicsHtmlParser.GetPageUrlsFromHtml(weeklyHtml);

                foreach (var link in comicPageLinks)
                {
                    var comicHtml = await _httpService.GetHtmlFromUrl(link);
                    var comicDownload = GetComicsHtmlParser.GetDownloadLinkFromHtml(comicHtml);

                    // log: downloading comicDownload.ComicName
                    try
                    {
                        var result = await _httpService.DownloadFileFromUrl(comicDownload.Link, _downloadPath);
                        // log: file result is complete
                    }
                    catch (Exception ex)
                    {
                        // log: error downloading comicDownload.Link
                    }
                }

                File.WriteAllText("LastDate.txt", date.ToShortDateString());
            }
        }

    }
}
