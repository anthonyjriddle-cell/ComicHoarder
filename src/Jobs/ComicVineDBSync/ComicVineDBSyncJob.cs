using ComicVineDBSync.Services;
using Microsoft.Extensions.Logging;

namespace ComicVineDBSync
{
    public class ComicVineDBSyncJob
    {
        private readonly VolumeService _volumeService;
        private readonly IssueService _issueService;
        private readonly ILogger<ComicVineDBSyncJob> _logger;

        public ComicVineDBSyncJob(VolumeService volumeService, IssueService issueService, ILogger<ComicVineDBSyncJob> logger)
        {
            _volumeService = volumeService;
            _issueService = issueService;
            _logger = logger;
        }

        public async Task RunAsync(DateTime? lastDate)
        {
            _logger.LogInformation("Begin Issue Update...");

            await _volumeService.FindNewVolumesAsync();

            await _issueService.GetAndWriteLatestIssuesAsync(lastDate);

            _logger.LogInformation("Finished getting new issues...");
        }
    }
}