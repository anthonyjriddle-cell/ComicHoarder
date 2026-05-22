using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues.Interfaces
{
    public interface IAddIssueUseCase
    {
        Task ExecuteAsync(Issue issue);
    }
}
