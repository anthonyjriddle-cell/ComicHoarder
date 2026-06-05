using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Configuration;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using GetComicsDownloader.Services;
using GetComicsDownloader;
using ComicHoarder.Shared;

namespace GetComicsDownload
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var downloadPath = configuration["DownloadPath"] ?? string.Empty;

            using var loggerFactory = LoggingSetup.CreateLoggerFactory(configuration, "GetComicDownloader");
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogTrace("******************************************");
            logger.LogTrace("* Downloading Comics from GetComics.org  *");
            logger.LogTrace("******************************************");


            using var httpService = new GetComicsHttpService(loggerFactory.CreateLogger<GetComicsHttpService>());
            var job = new GetComicsJob(httpService, loggerFactory.CreateLogger<GetComicsJob>(), downloadPath);

            await job.RunAsync();
        }
    }
}