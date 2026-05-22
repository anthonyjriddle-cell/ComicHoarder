using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine.Interfaces
{
    public interface ISearchMissingComicVinePublishersUseCase
    {
        Task<IEnumerable<Publisher>> ExecuteAsync(string partialPublisherName);
    }
}