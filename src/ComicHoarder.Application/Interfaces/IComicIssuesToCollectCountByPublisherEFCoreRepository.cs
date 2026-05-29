using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.Interfaces
{
    public interface IComicIssuesToCollectCountByPublisherEFCoreRepository
    {
        Task<IEnumerable<ComicIssuesToCollectCountByPublisher>?> GetAllAsync();
    }
}