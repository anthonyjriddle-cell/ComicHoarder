using ComicHoarder.Domain.Models;

namespace CH.UseCases.PluginInterfaces
{
    public interface IPublisherRepository
    {
        Task<Publisher> GetPublisherByIdAsync(int publisherId);
        Task<IEnumerable<Publisher>> GetPublishersByNameAsync(string name);
        Task UpdatePublisherAsnc(Publisher publisher);
        Task AddPublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(int publisherId);
    }
}