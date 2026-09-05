using ComicHoarder.Infrastructure;
using ComicHoarder.Infrastructure.ComicVine;
using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using ComicHoarder.Infrastructure.ComicVine.ComicVine;

namespace ComicVineDBSync
{
    public class IssueService
    {
        private readonly WebDataService _webDataService;
        private readonly PublisherEFCoreRepository _publisherRepository;
        private readonly VolumeEFCoreRepository volumeRepository;
        private readonly IssueEFCoreRepository _readIssueRepository;
        private readonly IssueEFCoreRepository _writeIssueRepository;
        private readonly ILogger<IssueService> _logger;

        public IssueService(WebDataService webDataService, PublisherEFCoreRepository publisherRepository, VolumeEFCoreRepository volumeRepository, IssueEFCoreRepository readIssueRepository, IssueEFCoreRepository writeIssueRepository, ILogger<IssueService> logger)
        {
            _webDataService = webDataService;
            _publisherRepository = publisherRepository;
            this.volumeRepository = volumeRepository;
            _readIssueRepository = readIssueRepository;
            _writeIssueRepository = writeIssueRepository;
            _logger = logger;
        }

        public async Task GetAndWriteLatestIssuesAsync(DateTime? lastDate)
        {
            var resolvedDate = await ResolveLastDateAsync(lastDate);
            var volumeIds = await volumeRepository.GetAllVolumeIdAsync();

            _logger.LogInformation("Updating issues back to date {LastDate}", resolvedDate);

            var issues = new List<Issue>();
            var offset = 0;

            try
            {
                var cvIssues = _webDataService.GetNewIssues(offset);
                Thread.Sleep(2000);

                while (cvIssues != null && cvIssues.OrderBy(x => x.DateAdded).FirstOrDefault()?.DateAdded > resolvedDate)
                {
                    _logger.LogInformation("Checking {Offset} issues back to {EarliestDate}", offset + 100, cvIssues.OrderBy(x => x.DateAdded).FirstOrDefault()?.DateAdded);

                    foreach (var cvIssue in cvIssues)
                    {
                        if (!volumeIds.Contains(cvIssue.VolumeId))
                            continue;

                        if (cvIssue.DateAdded <= resolvedDate)
                            continue;

                        if (await _readIssueRepository.GetIssueByIdAsync(cvIssue.Id) != null)
                            continue;

                        var volume = await volumeRepository.GetVolumeByIdAsync(cvIssue.VolumeId);

                        if (volume?.PublisherId != 31)
                        {
                            var publisherName = (await _publisherRepository.GetPublisherByIdAsync(volume.PublisherId)).Name;
                            _logger.LogDebug("Skipping non-Marvel issue - publisher {PublisherName}, volume {VolumeId} {VolumeName}", publisherName, volume.Id, volume.Name);
                            continue;
                        }

                        var publisher = (await _publisherRepository.GetPublisherByIdAsync(volume.PublisherId)).Name;
                        _logger.LogInformation("Creating issue {VolumeName} number {IssueNumber} for publisher {PublisherName}", volume.Name, cvIssue.IssueNumber, publisher);

                        issues.Add(cvIssue);
                    }
                    offset += 100;
                    cvIssues = _webDataService.GetNewIssues(offset);
                    Thread.Sleep(2000);
                }

                issues = issues.OrderBy(x => x.DateAdded).ToList();
            }
            finally
            {
                _logger.LogInformation("Writing {Count} issue(s) to database", issues.Count);
                foreach (var issue in issues)
                {
                    await _writeIssueRepository.AddIssueAsync(issue);
                }
            }
        }

        private async Task<DateTime?> ResolveLastDateAsync(DateTime? lastDate)
        {
            if (await _readIssueRepository.AnyIssuesAsync())
            {
                var mostRecent = await _readIssueRepository.GetMostRecentIssueDateAsync(lastDate);
                return mostRecent == null ? mostRecent?.AddMonths(-2) : DateTime.Now.AddMonths(-2);
            }

            return DateTime.Now.AddMonths(-2);
        }
    }
}