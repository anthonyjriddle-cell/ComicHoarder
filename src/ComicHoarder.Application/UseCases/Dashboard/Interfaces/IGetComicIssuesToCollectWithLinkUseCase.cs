using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Dashboard.Interfaces
{
    public interface IGetComicIssuesToCollectWithLinkUseCase
    {
        Task<IEnumerable<ComicIssuesToCollectWithLink>> ExecuteAsync(int publisherId);
    }
}