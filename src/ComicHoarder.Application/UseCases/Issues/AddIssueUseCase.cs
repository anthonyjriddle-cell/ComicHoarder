using ComicHoarder.Domain.Models;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Issues.Interfaces;
using ComicHoarder.Application.UseCases.Volumes;

namespace ComicHoarder.Application.UseCases.Issues
{
    public class AddIssueUseCase : IAddIssueUseCase
    {
        private readonly IIssueRepository issueRepository;

        public AddIssueUseCase(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public async Task ExecuteAsync(Issue issue)
        {
            await issueRepository.AddIssueAsync(issue);
        }
    }
}
