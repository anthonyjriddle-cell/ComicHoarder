using GetComicsDownloader.Services;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetComicsJob> _logger;
        private readonly string _downloadPath;

        public GetComicsJob(GetComicsHttpService httpService, ILogger<GetComicsJob> logger, string downloadPath)
        {
            _httpService = httpService;
            _logger = logger;
            _downloadPath = downloadPath;
        }

        public async Task RunAsync()
        {
            var beginDate = DateTime.Parse(File.ReadAllText("LastDate.txt"));
            var dates = GetComicsUrlBuilder.GetAllWednesdaysBetweenDates(beginDate, DateTime.Now);

            if (!dates.Any())
            {
                _logger.LogInformation("No comics to get, all caught up!");
                return;
            }

            _logger.LogInformation("Getting comics from {BeginDate} to {EndDate}",
                dates.First().ToShortDateString(), dates.Last().ToShortDateString());

            foreach (var date in dates)
            {
                _logger.LogInformation("Getting comics for {Date}", date.ToShortDateString());

                var weeklyUrl = GetComicsUrlBuilder.GenerateUrlFromDate(date);
                var weeklyHtml = await _httpService.GetHtmlFromUrl(weeklyUrl);
                var comicPageLinks = GetComicsHtmlParser.GetPageUrlsFromHtml(weeklyHtml);

                foreach (var link in comicPageLinks)
                {
                    var comicHtml = await _httpService.GetHtmlFromUrl(link);
                    var comicDownload = GetComicsHtmlParser.GetDownloadLinkFromHtml(comicHtml);

                    _logger.LogInformation("Downloading {ComicName}", comicDownload.ComicName);
                    try
                    {
                        var result = await _httpService.DownloadFileFromUrl(comicDownload.Link, _downloadPath);
                        _logger.LogInformation("File {FileName} is complete", result);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Could not download {Link}, Error: {Error}", comicDownload.Link, ex.Message);
                    }
                }

                File.WriteAllText("LastDate.txt", date.ToShortDateString());
            }
        }

    }
}
