using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Publishers.Interfaces
{
    public interface IViewPublishersByNameUseCase
    {
        Task<IEnumerable<Publisher>> ExecuteAsync(string name);
    }
}