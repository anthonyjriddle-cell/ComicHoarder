using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.Interfaces
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetAllPublishersAsync();
        Task<Publisher> GetPublisherByIdAsync(int publisherId);
        Task<IEnumerable<Publisher>> GetPublishersByNameAsync(string name);
        Task UpdatePublisherAsnc(Publisher publisher);
        Task AddPublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(int publisherId);
        Task<List<int>> GetAllEnabledPublisherIdsAsync();
    }
}