using ComicHoarder.Application.Interfaces;
using ComicHoarder.Application.UseCases.Issues.Interfaces;
using ComicHoarder.Domain.Models;
using System.Collections.Generic;

namespace ComicHoarder.Application.UseCases.Issues
{
    public class GetAllIssueFormatsUseCase : IGetAllIssueFormatsUseCase
    {
        private readonly IIssueFormatRepository issueFormatRepository;

        public GetAllIssueFormatsUseCase(IIssueFormatRepository issueFormatRepository)
        {
            this.issueFormatRepository = issueFormatRepository;
        }

        public async Task<IEnumerable<IssueFormat>> ExecuteAsync()
        {
            return await issueFormatRepository.GetAllAsync();
        }
    }
}
