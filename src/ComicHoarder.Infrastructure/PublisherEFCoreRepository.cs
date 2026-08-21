using ComicHoarder.Infrastructure.Models;
using ComicHoarder.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Mappers;
using ComicHoarder.Infrastructure.Logging;
using Microsoft.Extensions.Logging;

namespace ComicHoarder.Infrastructure
{
    public class PublisherEFCoreRepository : IPublisherRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;
        private readonly ILogger<IssueEFCoreRepository> logger;

        public PublisherEFCoreRepository(IDbContextFactory<CHContext> contextFactory, ILogger<IssueEFCoreRepository> logger)
        {
            this.contextFactory = contextFactory;
            this.logger = logger;
        }

        public async Task AddPublisherAsync(ComicHoarder.Domain.Models.Publisher publisher)
        {
            using var db = this.contextFactory.CreateDbContext();
            var pub = await db.Publishers
                    .FirstOrDefaultAsync(x => x.Id == publisher.Id);

            if (pub == null)
            {
                pub = new PublisherEntity
                {
                    Id = publisher.Id,
                    Name = publisher.Name,
                    Description = publisher.Description,
                    DateLastUpdated = publisher.DateLastUpdated,
                    Enabled = publisher.Enabled
                };

                db.Add(pub);

                await db.SaveChangesAsync();
            }
        }

        public async Task DeletePublisherAsync(int publisherId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var pub = await db.Publishers
                    .FirstOrDefaultAsync(x => x.Id == publisherId);

            if (pub != null)
            {
                db.Remove(pub);
                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Publisher>> GetAllPublishersAsync()
        {
            using var db = contextFactory.CreateDbContext();

            var data = await db.Publishers
                .OrderBy(x => x.Name)
                .ToListAsync();

            return data
                .Select(PublisherDataMapper.ToDomain)
                .ToList();
        }

        public async Task<Publisher> GetPublisherByIdAsync(int publisherId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var publisher = await db.Publishers.FirstOrDefaultAsync(x => x.Id == publisherId);
            if (publisher != null)
            {
                return PublisherDataMapper.ToDomain(publisher);
            }
            return new Publisher();
        }

        public async Task<IEnumerable<Publisher>> GetPublishersByNameAsync(string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            var data = await db.Publishers.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToListAsync();
            return data.Select(PublisherDataMapper.ToDomain).ToList();
        }

        public async Task UpdatePublisherAsnc(ComicHoarder.Domain.Models.Publisher publisher)
        {
            using var db = this.contextFactory.CreateDbContext();
            var pub = await db.Publishers
                    .FirstOrDefaultAsync(x => x.Id == publisher.Id);

            if (pub != null)
            {
                pub.Name = publisher.Name;
                pub.Description = publisher.Description;
                pub.DateLastUpdated = publisher.DateLastUpdated;
                pub.Enabled = publisher.Enabled;

                EntityChangeLogger.LogChanges(db.Entry(pub), "Publisher", logger);

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<int>> GetAllEnabledPublisherIdsAsync()
        {
            using var db = this.contextFactory.CreateDbContext();
            var ids = await db.Publishers.Where(x => x.Enabled == true).Select(x => x.Id).ToListAsync();
            return ids;
        }
    }
}
