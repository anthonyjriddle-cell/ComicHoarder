using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues.Interfaces
{
    public interface IEditIssueUseCase
    {
        Task ExecuteAsync(Issue issue);
    }
}