using ComicHoarder.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PDFToCBZ.Services;

namespace PDFToCBZ
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var inputFolder = configuration["InputFolder"] ?? string.Empty;
            var workFolder = configuration["WorkFolder"] ?? string.Empty;
            var outputFolder = configuration["OutputFolder"] ?? string.Empty;

            using var loggerFactory = LoggingSetup.CreateLoggerFactory(configuration, "PDFToCBZ");
            var logger = loggerFactory.CreateLogger<Program>();

            logger.LogTrace("*****************************");
            logger.LogTrace("* Converting PDFs to CBZ    *");
            logger.LogTrace("*****************************");

            var pdfToImagesService = new PDFToImagesService();
            var zipService = new ZipService();

            var job = new PDFToCBZJob(pdfToImagesService, zipService, loggerFactory.CreateLogger<PDFToCBZJob>(), inputFolder, workFolder, outputFolder);
            await job.RunAsync();
        }
    }
}