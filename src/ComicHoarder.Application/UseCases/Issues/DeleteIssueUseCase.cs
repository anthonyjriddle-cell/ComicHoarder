using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Issues.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues
{
    public class DeleteIssueUseCase : IDeleteIssueUseCase
    {
        private readonly IIssueRepository issueRepository;

        public DeleteIssueUseCase(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public async Task ExecuteAsync(int issueId)
        {
            await issueRepository.DeleteIssueAsync(issueId);
        }
    }
}
