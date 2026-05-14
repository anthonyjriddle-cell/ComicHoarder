using ComicHoarder.Infrastructure.Models;
using CH.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Mappers;

namespace ComicHoarder.Infrastructure
{
    public class PublisherEFCoreRepository : IPublisherRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;

        public PublisherEFCoreRepository(IDbContextFactory<CHContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        public async Task AddPublisherAsync(ComicHoarder.Domain.Models.Publisher publisher)
        {
            using var db = this.contextFactory.CreateDbContext();
            var pub = await db.Publishers
                    .FirstOrDefaultAsync(x => x.Id == publisher.id);

            if (pub == null)
            {
                pub = new PublisherEntity
                {
                    Name = publisher.name,
                    Description = publisher.description,
                    DateLastUpdated = publisher.dateLastUpdated,
                    Enabled = publisher.enabled
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
                    .FirstOrDefaultAsync(x => x.Id == publisher.id);

            if (pub != null)
            {
                pub.Name = publisher.name;
                pub.Description = publisher.description;
                pub.DateLastUpdated = publisher.dateLastUpdated;
                pub.Enabled = publisher.enabled;

                await db.SaveChangesAsync();
            }
        }
    }
}
