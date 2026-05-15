using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Issues.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues
{
    public class EditIssueUseCase : IEditIssueUseCase
    {
        private readonly IIssueRepository issueRepository;

        public EditIssueUseCase(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public async Task ExecuteAsync(Issue issue)
        {
            await issueRepository.UpdateIssueAsync(issue);
        }
    }
}
