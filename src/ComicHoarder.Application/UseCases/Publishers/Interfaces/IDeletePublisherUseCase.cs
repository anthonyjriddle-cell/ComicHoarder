using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Publishers.Interfaces
{
    public interface IDeletePublisherUseCase
    {
        Task ExecuteAsync(int publisherId);
    }
}