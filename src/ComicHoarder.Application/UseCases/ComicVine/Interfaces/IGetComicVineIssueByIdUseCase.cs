using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.ComicVine.Interfaces
{
    public interface IGetComicVineIssueByIdUseCase
    {
        Task<Issue?> ExecuteAsync(int id);
    }
}