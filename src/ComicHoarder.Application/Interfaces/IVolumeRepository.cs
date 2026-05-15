using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.Interfaces
{
    public interface IVolumeRepository
    {
        Task<Volume> GetVolumeByIdAsync(int volumeId);
        Task<IEnumerable<Volume>> GetVolumesByPublisherAndNameAsync(int id, string name);
        Task UpdateVolumeAsnc(Volume volume);
        //Task AddPublisherAsync(Core.Models.Publisher publisher);
        Task DeleteVolumeAsync(int volumeId);
    }
}