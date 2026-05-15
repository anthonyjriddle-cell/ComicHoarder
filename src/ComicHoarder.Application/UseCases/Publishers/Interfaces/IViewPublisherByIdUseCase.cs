using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Publishers.Interfaces
{
    public interface IViewPublisherByIdUseCase
    {
        Task<Publisher> ExecuteAsync(int publisherId);
    }
}