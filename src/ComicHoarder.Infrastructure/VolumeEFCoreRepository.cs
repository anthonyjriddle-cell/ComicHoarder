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
using Microsoft.Extensions.Logging;
using ComicHoarder.Infrastructure.Logging;

namespace ComicHoarder.Infrastructure
{
    public class VolumeEFCoreRepository : IVolumeRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;
        private readonly ILogger<IssueEFCoreRepository> logger;

        public VolumeEFCoreRepository(IDbContextFactory<CHContext> contextFactory, ILogger<IssueEFCoreRepository> logger)
        {
            this.contextFactory = contextFactory;
            this.logger = logger;
        }

        public async Task AddVolumeAsync(ComicHoarder.Domain.Models.Volume volume)
        {
            using var db = this.contextFactory.CreateDbContext();

            var vol = await db.Volumes
                .FirstOrDefaultAsync(x => x.Id == volume.Id);

            if (vol == null)
            {
                vol = new VolumeEntity
                {
                    Id = volume.Id,
                    PublisherId = volume.PublisherId,
                    Name = volume.Name,
                    Description = volume.Description,
                    DateAdded = volume.DateAdded,
                    DateLastUpdated = volume.DateLastUpdated,
                    Collectable = volume.Collectable,
                    CountOfIssues = volume.CountOfIssues,
                    StartYear = volume.StartYear,
                    Enabled = volume.Enabled,
                    Complete = volume.Complete
                };

                db.Add(vol);

                await db.SaveChangesAsync();
            }
        }

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
            var data = await db.Volumes
                .Where(x => x.PublisherId == id &&
                            x.Name.Contains(name))
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .ToListAsync();
            return data.Select(VolumeDataMapper.ToDomain).ToList();
        }

        public async Task<IEnumerable<Volume>> GetVolumesByPublisherIdAsync(int publisherId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var data = await db.Volumes
                .Where(x => x.PublisherId == publisherId)
                .OrderBy(x => x.Name)
                .AsNoTracking()
                .IgnoreAutoIncludes()
                .ToListAsync();
            return data.Select(VolumeDataMapper.ToDomain).ToList();
        }

        public async Task UpdateVolumeAsnc(ComicHoarder.Domain.Models.Volume volume)
        {
            using var db = this.contextFactory.CreateDbContext();
            var vol = await db.Volumes
                    .FirstOrDefaultAsync(x => x.Id == volume.Id);

            if (vol != null)
            {
                vol.Name = volume.Name;
                vol.Description = volume.Description;
                vol.DateLastUpdated = volume.DateLastUpdated;
                vol.Collectable = volume.Collectable;
                vol.Enabled = volume.Enabled;

                EntityChangeLogger.LogChanges(db.Entry(vol), "Volume", logger);

                await db.SaveChangesAsync();
            }
        }

        public async Task<List<int>> GetAllVolumeIdAsync()
        {
            using var db = this.contextFactory.CreateDbContext();
            var ids = await db.Volumes.Select(x => x.Id).ToListAsync();
            return ids;
        }
    }
}
