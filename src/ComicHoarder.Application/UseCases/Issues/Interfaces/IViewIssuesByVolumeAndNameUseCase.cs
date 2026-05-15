using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues.Interfaces
{
    public interface IViewIssuesByVolumeAndNameUseCase
    {
        Task<IEnumerable<Issue>> ExecuteAsync(int volumeId, string name);
    }
}