using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Volumes.Interfaces
{
    public interface IViewVolumesByPublisherAndNameUseCase
    {
        Task<IEnumerable<Volume>> ExecuteAsync(int id, string name);
    }
}