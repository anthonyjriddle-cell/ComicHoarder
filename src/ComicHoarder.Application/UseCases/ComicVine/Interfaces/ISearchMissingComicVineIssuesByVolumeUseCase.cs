using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine.Interfaces
{
    public interface ISearchMissingComicVineIssuesByVolumeUseCase
    {
        Task<IEnumerable<Issue>> ExecuteAsync(int volumeId);
    }
}