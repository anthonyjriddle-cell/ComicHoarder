using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Issues.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues
{
    public class ViewIssueByIdUseCase : IViewIssueByIdUseCase
    {
        private readonly IIssueRepository issueRepository;

        public ViewIssueByIdUseCase(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public async Task<Issue> ExecuteAsync(int issueId)
        {
            return await issueRepository.GetIssueByIdAsync(issueId);
        }
    }
}
