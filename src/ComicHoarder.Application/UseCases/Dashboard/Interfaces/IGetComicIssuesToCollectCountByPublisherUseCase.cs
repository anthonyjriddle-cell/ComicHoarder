using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Dashboard.Interfaces
{
    public interface IGetComicIssuesToCollectCountByPublisherUseCase
    {
        Task<IEnumerable<ComicIssuesToCollectCountByPublisher>> ExecuteAsync();
    }
}