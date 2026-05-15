using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues.Interfaces
{
    public interface IViewIssueByIdUseCase
    {
        Task<Issue> ExecuteAsync(int issueId);
    }
}