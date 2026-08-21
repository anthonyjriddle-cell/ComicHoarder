using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.Interfaces
{
    public interface IComicIssuesToCollectWithLinkEFCoreRepository
    {
        Task<IEnumerable<ComicIssuesToCollectWithLink>?> GetAllAsync();
        Task<IEnumerable<ComicIssuesToCollectWithLink>?> GetByPublisherAsync(int publisherId);
    }
}