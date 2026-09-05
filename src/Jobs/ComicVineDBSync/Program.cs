using System;
using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure;
using ComicHoarder.Infrastructure.ComicVine;
using ComicHoarder.Infrastructure.ComicVine.ComicVine;
using ComicHoarder.Infrastructure.Models;
using ComicHoarder.Shared;
using ComicVineDBSync;
using ComicVineDBSync.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GetNewIssues
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            using var loggerFactory = LoggingSetup.CreateLoggerFactory(configuration, "ComicVineDBSync");
            var logger = loggerFactory.CreateLogger<Program>();
            //var logger = CreateLogger();

            logger.LogTrace("******************************************");
            logger.LogTrace("* ComicVine DB Sync                      *");
            logger.LogTrace("******************************************");


            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            DateTime? lastDate = null;
            if (args.Length > 0)
            {
                if (DateTime.TryParse(args[0], out var parsedDate))
                {
                    lastDate = parsedDate;
                }
                else
                {
                    logger.LogWarning("Could not parse date argument {Argument}, proceeding without it", args[0]);
                }
            }

            CHContext db = new CHContext();
            var key = db.Settings.Where(x => x.Name == "ComicVineKey").FirstOrDefault().Value;

            var services = new ServiceCollection();

            // Configuration
            services.AddSingleton<IConfiguration>(configuration);

            // Logging
            services.AddSingleton(loggerFactory);
            services.AddLogging();

            // Infrastructure - call your existing DI extension method twice with named keys
            services.AddInfrastructure(connectionString);

            // Services
            services.AddTransient<WebDataService>(x =>
            {
                return new WebDataService(key);
            });
            services.AddTransient<PublisherEFCoreRepository>();
            services.AddTransient<VolumeEFCoreRepository>();
            services.AddTransient<IssueEFCoreRepository>();
            services.AddTransient<VolumeService>();
            services.AddTransient<IssueService>();
            services.AddTransient<ComicVineDBSyncJob>();

            var serviceProvider = services.BuildServiceProvider();

            var job = serviceProvider.GetRequiredService<ComicVineDBSyncJob>();
            await job.RunAsync(lastDate);
        }
    }
}