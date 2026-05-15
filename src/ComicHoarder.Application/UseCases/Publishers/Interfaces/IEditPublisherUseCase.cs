using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Publishers.Interfaces
{
    public interface IEditPublisherUseCase
    {
        Task ExecuteAsync(Publisher publisher);
    }
}