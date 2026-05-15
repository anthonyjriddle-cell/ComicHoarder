using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Issues.Interfaces;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.UseCases.Issues
{
    public class ViewIssuesByVolumeAndNameUseCase : IViewIssuesByVolumeAndNameUseCase
    {
        private readonly IIssueRepository issueRepository;

        public ViewIssuesByVolumeAndNameUseCase(IIssueRepository issueRepository)
        {
            this.issueRepository = issueRepository;
        }

        public async Task<IEnumerable<Issue>> ExecuteAsync(int volumeId, string name)
        {
            return await issueRepository.GetIssuesByVolumeAndNameAsync(volumeId, name);
        }
    }
}
