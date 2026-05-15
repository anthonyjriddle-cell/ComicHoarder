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

namespace ComicHoarder.Infrastructure
{
    public class VolumeEFCoreRepository : IVolumeRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;

        public VolumeEFCoreRepository(IDbContextFactory<CHContext> contextFactory)
        {
            this.contextFactory = contextFactory;
        }

        //public async Task AddPublisherAsync(Core.Models.Publisher publisher)
        //{
        //    using var db = this.contextFactory.CreateDbContext();
        //    var pub = await db.Publishers
        //            .FirstOrDefaultAsync(x => x.Id == publisher.id);

        //    if (pub == null)
        //    {
        //        pub = new Publisher
        //        {
        //            Name = publisher.name,
        //            Description = publisher.description,
        //            DateLastUpdated = publisher.dateLastUpdated,
        //            Enabled = publisher.enabled
        //        };

        //        db.Add(pub);

        //        await db.SaveChangesAsync();
        //    }
        //}

        public async Task DeleteVolumeAsync(int volumeId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var vol = await db.Volumes
                    .FirstOrDefaultAsync(x => x.Id == volumeId);

            if (vol != null)
            {
                db.Remove(vol);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Volume> GetVolumeByIdAsync(int volumeId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var volume = await db.Volumes.FirstOrDefaultAsync(x => x.Id == volumeId);
            if (volume != null)
            {
                return VolumeDataMapper.ToDomain(volume);
            }
            return new Volume();
        }

        public async Task<IEnumerable<ComicHoarder.Domain.Models.Volume>> GetVolumesByPublisherAndNameAsync(int id, string name)
        {
            using var db = this.contextFactory.CreateDbContext();
            var data = await db.Volumes.Where(x => x.PublisherId == id && x.Name.ToLower().Contains(name.ToLower())).OrderBy(x => x.Name).ToListAsync();
            return data.Select(VolumeDataMapper.ToDomain).ToList();
        }

        public async Task UpdateVolumeAsnc(ComicHoarder.Domain.Models.Volume volume)
        {
            using var db = this.contextFactory.CreateDbContext();
            var vol = await db.Volumes
                    .FirstOrDefaultAsync(x => x.Id == volume.id);

            if (vol != null)
            {
                vol.Name = volume.name;
                vol.Description = volume.description;
                vol.DateLastUpdated = volume.dateLastUpdated;
                vol.Enabled = volume.enabled;

                await db.SaveChangesAsync();
            }
        }
    }
}
