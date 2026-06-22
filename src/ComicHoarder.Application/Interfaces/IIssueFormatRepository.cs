using ComicHoarder.Domain.Models;

namespace ComicHoarder.Application.Interfaces
{
    public interface IIssueFormatRepository
    {
        Task<IEnumerable<IssueFormat>> GetAllAsync();
    }
}