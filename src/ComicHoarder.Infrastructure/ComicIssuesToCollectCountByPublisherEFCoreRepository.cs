using ComicHoarder.Application.Interfaces;
using ComicHoarder.Infrastructure.Models;
using ComicHoarder.Infrastructure.Mappers;
using ComicHoarder.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicHoarder.Infrastructure.Repositories.Statistics
{
    public class ComicIssuesToCollectCountByPublisherEFCoreRepository : IComicIssuesToCollectCountByPublisherEFCoreRepository

    {
        private readonly IDbContextFactory<CHContext> contextFactory;

        public ComicIssuesToCollectCountByPublisherEFCoreRepository(IDbContextFactory<CHContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<ComicIssuesToCollectCountByPublisher>> GetAllAsync()
        {
            using var db = this.contextFactory.CreateDbContext();

            var data = await db.ComicIssuesToCollectCountByPublisher
                           .AsNoTracking()
                           .ToListAsync();
            
            return data.Select(ComicIssuesToCollectCountByPublisherDataMapper.ToDomain).ToList();
        }
    }
}
