using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.Interfaces
{
    public interface IIssueRepository
    {
        Task AddIssueAsync(Issue issue);
        Task DeleteIssueAsync(int issueId);
        Task<Issue> GetIssueByIdAsync(int issueId);
        Task<IEnumerable<Issue>> GetIssuesByVolumeIdAsync(int volumeId);
        Task<IEnumerable<Issue>> GetIssuesByVolumeAndNameAsync(int volumeId, string name);
        Task UpdateIssueAsync(Issue issue);
        Task<List<int>> GetAllIssueIds();
        Task<bool> AnyIssuesAsync();
        Task<DateTime?> GetMostRecentIssueDateAsync(DateTime? lastDate);
    }
}