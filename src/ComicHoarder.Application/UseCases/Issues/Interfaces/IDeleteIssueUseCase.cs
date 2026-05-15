namespace ComicHoarder.Application.UseCases.Issues.Interfaces
{
    public interface IDeleteIssueUseCase
    {
        Task ExecuteAsync(int issueId);
    }
}