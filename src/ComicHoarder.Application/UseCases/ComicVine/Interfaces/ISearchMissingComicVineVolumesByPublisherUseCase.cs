using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine.Interfaces
{
    public interface ISearchMissingComicVineVolumesByPublisherUseCase
    {
        Task<IEnumerable<Volume>> ExecuteAsync(int publisherId);
    }
}