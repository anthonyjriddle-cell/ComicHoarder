using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine.Interfaces
{
    public interface ISearchComicVinePublisherUseCase
    {
        Task<IEnumerable<Publisher>> ExecuteAsync(string partialPublisherName);
    }
}