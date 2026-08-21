using ComicHoarder.Application.Interfaces;
using ComicHoarder.Infrastructure.Models;
using ComicHoarder.Infrastructure.Mappers;
using ComicHoarder.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ComicHoarder.Infrastructure.Repositories.Dashboard
{
    public class ComicIssuesToCollectWithLinkEFCoreRepository : IComicIssuesToCollectWithLinkEFCoreRepository

    {
        private readonly IDbContextFactory<CHContext> contextFactory;

        public ComicIssuesToCollectWithLinkEFCoreRepository(IDbContextFactory<CHContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task<IEnumerable<ComicIssuesToCollectWithLink>> GetAllAsync()
        {
            using var db = this.contextFactory.CreateDbContext();

            var data = await db.ComicIssuesToCollectWithLinks
                           .AsNoTracking()
                           .ToListAsync();
            
            return data.Select(ComicIssuesToCollectWithLinkDataMapper.ToDomain).ToList();
        }

        public async Task<IEnumerable<ComicIssuesToCollectWithLink>> GetByPublisherAsync(int publisherId)
        {
            using var db = this.contextFactory.CreateDbContext();
            List<ComicIssuesToCollectWithLinkEntity> data = new List<ComicIssuesToCollectWithLinkEntity> ();

            var publisherName = db.Publishers.Where(x => x.Id == publisherId)?.FirstOrDefault()?.Name;

            if (publisherName == null)
            {
                data = await db.ComicIssuesToCollectWithLinks
                .AsNoTracking()
                .ToListAsync();
            }
            else
            {
                data = await db.ComicIssuesToCollectWithLinks
                    .Where(x => x.PublisherName == publisherName)
                    .AsNoTracking()
                    .ToListAsync();
            }

            return data.Select(ComicIssuesToCollectWithLinkDataMapper.ToDomain).ToList();
        }
    }
}
