using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Publishers.Interfaces
{
    public interface IAddPublisherUseCase
    {
        Task ExecuteAsync(Publisher publisher);
    }
}