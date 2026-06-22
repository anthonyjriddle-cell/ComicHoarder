using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Mappers;
using ComicHoarder.Application.Interfaces;
using ComicHoarder.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicHoarder.Infrastructure
{
    public class IssueFormatEFCoreRepository : IIssueFormatRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;

        public IssueFormatEFCoreRepository(IDbContextFactory<CHContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<IssueFormat>> GetAllAsync()
        {
            using var db = this.contextFactory.CreateDbContext();

            var data = await db.IssueFormats
                           .AsNoTracking()
                           .ToListAsync();

            return data.Select(IssueFormatDataMapper.ToDomain).ToList();
        }
    }
}
