using System;
using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure;
using ComicHoarder.Infrastructure.ComicVine;
using ComicHoarder.Infrastructure.ComicVine.ComicVine;
using ComicHoarder.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GetNewIssues
{
    public class Program
    {
        static void Main(string[] args)
        {
            var logger = CreateLogger();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            var lastDate = DateTime.MaxValue;

            if (args.Length > 0)
            {
                lastDate = Convert.ToDateTime(args[0]);
            }

            using (logger.BeginScope("[Begin Logging Scope]"))
            {
                logger.LogInformation("Update Issues in Database from Comicvine");
                logger.LogInformation("Begin Issue Update...");

                CHContext context = new CHContext();
                CHContext updateContext = new CHContext();

                var key = context.Settings.Where(x => x.Name == "ComicVineKey").Select(x => x.Value).FirstOrDefault();

                WebDataService webDataService = new WebDataService(key);

                var volumeIds = context.Volumes.Select(x => x.Id).ToList();

                FindNewVolumes(webDataService, context, updateContext, logger, volumeIds);

                //refresh volumeIds with new volumes
                volumeIds = context.Volumes.Select(x => x.Id).ToList();

                List<Issue> issues = GetLatestIssues(webDataService, context, logger, volumeIds, ref lastDate);

                WriteIssues(updateContext, logger, issues);

                logger.LogInformation("Finished getting new issues...");
            }
        }

        private static void WriteIssues(CHContext updateContext, ILogger<Program> logger, List<Issue> issues)
        {
            logger.LogInformation("Writing {0} issue[s] to db.", issues.Count());
            foreach (var issue in issues)
            {
                var issueName = "";
                if (issue.Name != null && issue.Name.Length > 100)
                {
                    issueName = issue.Name.Substring(0, 99);
                }
                else
                {
                    issueName = issue.Name;
                }
                updateContext.Issues.Add(new IssueEntity()
                {
                    //TODO pull out mapping common to data class
                    Id = issue.Id,
                    DateAdded = issue.DateAdded,
                    Collected = false,
                    CoverDate = issue.CoverDate,
                    DateLastUpdated = issue.DateLastUpdated,
                    Enabled = true,
                    IssueNumber = issue.IssueNumber,
                    IssueNumberSuffix = issue.IssueNumberSuffix,
                    VolumeId = issue.VolumeId,
                    Name = issueName,
                    PublishMonth = issue.PublishMonth,
                    PublishYear = issue.PublishYear,
                    Reprint = false,
                    FormatId = null
                });
                updateContext.SaveChanges();
            }
        }

        private static List<Issue> GetLatestIssues(WebDataService webDataService, CHContext context, ILogger<Program> logger, List<int> volumeIds, ref DateTime lastDate)
        {
            if (lastDate == DateTime.MaxValue)
            {
                if (context.Issues.Any())
                {
                    lastDate = context.Issues.Where(x => x.DateAdded != null).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.HasValue ? context.Issues.Where(x => x.DateAdded != null).OrderByDescending(x => x.DateAdded).FirstOrDefault().DateAdded.Value : lastDate;
                    lastDate = lastDate.AddMonths(-2);
                }
                else
                {
                    //lastDate = DateTime.MinValue;
                    lastDate = DateTime.Now.AddMonths(-2);
                }
            }
            //lastDate = new DateTime(2022, 8, 9); Just in case
            logger.LogInformation("Updating Issues back to Date {0}", lastDate.ToString());
            var offset = 0;

            var cvIssues = webDataService.GetNewIssues(offset);
            Thread.Sleep(2000);

            var issues = new List<Issue>();

            while (cvIssues != null && cvIssues.OrderBy(x => x.DateAdded).FirstOrDefault().DateAdded > lastDate)
            {
                logger.LogInformation("Checking {0} Issues back to {1}", offset + 100, cvIssues.OrderBy(x => x.DateAdded).FirstOrDefault().DateAdded);
                foreach (var cvIssue in cvIssues)
                {
                    if (volumeIds.Contains(cvIssue.VolumeId))
                    {
                        if (cvIssue.DateAdded > lastDate)
                        {
                            if (!context.Issues.Where(x => x.Id == cvIssue.Id).Any())
                            {
                                var volume = context.Volumes.Where(x => x.Id == cvIssue.VolumeId).FirstOrDefault();
                                //Hack for Marvel
                                if (volume.PublisherId != 31)
                                {
                                    var publisher = context.Publishers.Where(x => x.Id == volume.PublisherId).FirstOrDefault();
                                    logger.LogInformation(String.Format($"Not Marvel - Publisher {publisher.Name}, Volume - {volume.Id} {volume.Name}"));
                                }
                                else
                                {
                                    var publisher = context.Publishers.Where(x => x.Id == volume.PublisherId).FirstOrDefault();
                                    logger.LogInformation("Creating Issue {0} Number {1} for Publisher {2}", volume.Name, cvIssue.IssueNumber, publisher.Name);
                                    //TODO Pull out to mapping class
                                    var issue = new Issue()
                                    {
                                        Id = cvIssue.Id,
                                        Name = cvIssue.Name,
                                        DateAdded = cvIssue.DateAdded,
                                        DateLastUpdated = cvIssue.DateLastUpdated,
                                        CoverDate = cvIssue.CoverDate,
                                        Collected = cvIssue.Collected,
                                        Enabled = cvIssue.Enabled,
                                        FormatId = cvIssue.FormatId,
                                        IssueNumber = cvIssue.IssueNumber,
                                        IssueNumberSuffix = cvIssue.IssueNumberSuffix,
                                        PublishMonth = cvIssue.PublishMonth,
                                        PublishYear = cvIssue.PublishYear,
                                        Reprint = cvIssue.Reprint,
                                        Summary = cvIssue.Summary,
                                        VolumeId = cvIssue.VolumeId
                                    };
                                    issues.Add(issue);
                                }
                            }
                        }
                    }
                }
                offset = offset + 100;
                cvIssues = webDataService.GetNewIssues(offset);
                Thread.Sleep(2000);
            }

            issues = issues.OrderBy(x => x.DateAdded).ToList();
            return issues;
        }

        private static void FindNewVolumes(WebDataService webDataService, CHContext context, CHContext updateContext, ILogger<Program> logger, List<int>? volumeIds)
        {
            var publisherIds = context.Publishers.Where(x => x.Enabled == true).Select(x => x.Id).ToList();

            foreach (var publisherId in publisherIds)
            {
                //Hack for only Marvel
                if (publisherId == 31)
                {
                    var publisherName = context.Publishers.Where(x => x.Id == publisherId).Select(x => x.Name).FirstOrDefault();
                    logger.LogInformation(string.Format("Checking Publisher {0} for updates", publisherName));

                    var volumes = webDataService.GetVolumesFromPublisher(publisherId);
                    Thread.Sleep(2000);

                    var newVolumes = volumes.Where(x => !volumeIds.Contains(x.Id));

                    logger.LogInformation("Creating {0} Volumes for Publisher {1}", newVolumes.Count(), publisherName);
                    foreach (var newVolume in newVolumes)
                    {
                        var volume = webDataService.GetVolume(newVolume.Id);
                        Thread.Sleep(2000);
                        if (volume is not null)
                        {
                            logger.LogInformation("Creating Volume {0} for {1}", newVolume.Name, publisherName);
                            var reprint = ReprintDetector.DetectReprint(volume);
                            updateContext.Volumes.Add(new VolumeEntity()
                            {
                                //TODO pull out to mapping class
                                Id = volume.Id,
                                Name = volume.Name,
                                DateAdded = volume.DateAdded,
                                Collectable = !reprint,
                                Complete = false,
                                CountOfIssues = 0,
                                DateLastUpdated = volume.DateLastUpdated,
                                Description = volume.Description,
                                Enabled = true,
                                PublisherId = publisherId,
                                StartYear = volume.StartYear,

                            });
                        }
                    }
                    updateContext.SaveChanges();
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