using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace GetComicsDownload
{
    internal class Program
    {

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        async static Task MainAsync(string[] args)
        {
            var _getComicService = new GetComicService();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var downloadpath = configuration["DownloadPath"] ?? string.Empty;
            var logger = CreateLogger();
            using (logger.BeginScope("[scope is enabled]"))
            {
                using (HttpClient client = new HttpClient())
                {
                    logger.LogInformation("******************************************");
                    logger.LogInformation("* Downloading Comics from GetComics.org  *");
                    logger.LogInformation("******************************************");


                    var beginDate = DateTime.Parse(File.ReadAllText("LastDate.txt"));
                    var dates = _getComicService.GetAllWednesdaysBetweenDates(beginDate, DateTime.Now);

                    if (!dates.Any())
                    {
                        logger.LogInformation("No comics to get, all caught up!");
                        return;
                    }

                    logger.LogInformation(String.Format("Getting comics from {0} to {1}", dates.First().ToShortDateString(), dates.Last().ToShortDateString()));

                    foreach (var date in dates)
                    {
                        logger.LogInformation("");
                        logger.LogInformation("Getting comics for {0}", date.ToShortDateString());

                        var weeklyUrl = _getComicService.GenerateUrlFromDate(date);

                        var weeklyContents = await _getComicService.GetHtmlFromUrl(weeklyUrl, client);
                        var comicPageDlLinks = _getComicService.GetPageUrlsFromHtml(weeklyContents);

                        foreach (var comicPageDllink in comicPageDlLinks)
                        {
                            logger.LogInformation("Loading Link {0}", comicPageDllink);
                        }

                        foreach (var link in comicPageDlLinks)
                        {
                            var comicLink = await _getComicService.GetHtmlFromUrl(link, client);
                            var comicDownload = _getComicService.GetDownloadLinkFromHtml(comicLink);

                            logger.LogInformation("");
                            logger.LogInformation(String.Format("Downloading {0}", comicDownload.ComicName));

                            try
                            {
                                var result = await _getComicService.DownloadFileFromUrl(comicDownload.Link, downloadpath, client);
                                logger.LogInformation(String.Format("File {0} is complete.", result));
                            }
                            catch (Exception ex)
                            {
                                logger.LogError(String.Format("Could not download {0}, Error: {1}", comicDownload.Link, ex.Message));
                            }
                        }
                        File.WriteAllText("LastDate.txt", date.ToShortDateString());
                    }
                }
            }
        }

        public static ILogger<Program> CreateLogger()
        {
            using ILoggerFactory loggerFactory =
                 LoggerFactory.Create(builder =>
                 builder.AddSimpleConsole(options =>
                 {
                     options.IncludeScopes = false;
                     options.SingleLine = true;
                     options.TimestampFormat = "HH:mm:ss.ff ";
                 }));


            return loggerFactory.CreateLogger<Program>();
        }
    }
}