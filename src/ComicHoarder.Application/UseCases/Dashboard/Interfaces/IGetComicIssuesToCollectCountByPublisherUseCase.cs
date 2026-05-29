using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Statistics.Interfaces
{
    public interface IGetComicIssuesToCollectCountByPublisherUseCase
    {
        Task<IEnumerable<ComicIssuesToCollectCountByPublisher>> ExecuteAsync();
    }
}