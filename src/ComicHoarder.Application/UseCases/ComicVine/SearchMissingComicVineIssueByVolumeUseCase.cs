using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.ComicVine.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine
{
    public class SearchMissingComicVineIssuesByVolumeUseCase : ISearchMissingComicVineIssuesByVolumeUseCase
    {
        private readonly IWebDataService webDataService;
        private readonly IIssueRepository issueRepository;

        public SearchMissingComicVineIssuesByVolumeUseCase(
            IWebDataService webDataService,
            IIssueRepository issueRepository)
        {
            this.webDataService = webDataService;
            this.issueRepository = issueRepository;
        }

        public async Task<IEnumerable<Issue>> ExecuteAsync(int volumeId)
        {
            // Get issues from ComicVine for this volume
            var comicVineIssues = webDataService.GetIssuesFromVolume(volumeId)
                ?? new List<Issue>();

            // Get all local issue IDs
            var localIssueIds = await issueRepository.GetAllIssueIds();

            // Return only issues not already in the database
            var issuesNotInDatabase = comicVineIssues
                .Where(i => !localIssueIds.Contains(i.Id))
                .ToList();

            return issuesNotInDatabase;
        }
    }
}
