using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Mappers;
using ComicHoarder.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using ComicHoarder.Application.Interfaces;
using Microsoft.Extensions.Logging;
using ComicHoarder.Infrastructure.Logging;

namespace ComicHoarder.Infrastructure
{
    public class IssueEFCoreRepository : IIssueRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;
        private readonly ILogger<IssueEFCoreRepository> logger;

        public IssueEFCoreRepository(
            IDbContextFactory<CHContext> contextFactory,
            ILogger<IssueEFCoreRepository> logger)
        {
            this.contextFactory = contextFactory;
            this.logger = logger;
        }

        public async Task AddIssueAsync(Issue issue)
        {
            using var db = this.contextFactory.CreateDbContext();

            var existing = await db.Issues
                .FirstOrDefaultAsync(x => x.Id == issue.Id);

            if (existing == null)
            {
                var entity = new IssueEntity
                {
                    VolumeId = issue.VolumeId,
                    Name = issue.Name,
                    IssueNumber = issue.IssueNumber,
                    PublishMonth = issue.PublishMonth,
                    PublishYear = issue.PublishYear,
                    Collected = issue.Collected,
                    Enabled = issue.Enabled,
                    IssueNumberSuffix = issue.IssueNumberSuffix,
                    FormatId = issue.FormatId,
                    Reprint = issue.Reprint,
                    DateAdded = issue.DateAdded,
                    CoverDate = issue.CoverDate,
                    DateLastUpdated = issue.DateLastUpdated
                };

                db.Add(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteIssueAsync(int issueId)
        {
            using var db = this.contextFactory.CreateDbContext();

            var entity = await db.Issues
                .FirstOrDefaultAsync(x => x.Id == issueId);

            if (entity != null)
            {
                db.Remove(entity);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Issue> GetIssueByIdAsync(int issueId)
        {
            using var db = this.contextFactory.CreateDbContext();

            var entity = await db.Issues
                .FirstOrDefaultAsync(x => x.Id == issueId);

            if (entity != null)
            {
                return IssueDataMapper.ToDomain(entity);
            }

            return new Issue();
        }

        public async Task<IEnumerable<Issue>> GetIssuesByVolumeIdAsync(int volumeId)
        {
            using var db = this.contextFactory.CreateDbContext();

            var data = await db.Issues
                .Where(x => x.VolumeId == volumeId)
                .OrderBy(x => x.IssueNumber)
                .ToListAsync();

            return data.Select(IssueDataMapper.ToDomain).ToList();
        }

        public async Task<IEnumerable<Issue>> GetIssuesByVolumeAndNameAsync(int volumeId, string name)
        {
            using var db = this.contextFactory.CreateDbContext();

            var data = await db.Issues
                .Where(x => x.VolumeId == volumeId &&
                            x.Name != null &&
                            x.Name.ToLower().Contains(name.ToLower()))
                .OrderBy(x => x.IssueNumber)
                .ToListAsync();

            return data.Select(IssueDataMapper.ToDomain).ToList();
        }

        public async Task UpdateIssueAsync(Issue issue)
        {
            using var db = this.contextFactory.CreateDbContext();

            var entity = await db.Issues
                .FirstOrDefaultAsync(x => x.Id == issue.Id);

            if (entity != null)
            {
                entity.VolumeId = issue.VolumeId;
                entity.Name = issue.Name;
                entity.IssueNumber = issue.IssueNumber;
                entity.PublishMonth = issue.PublishMonth;
                entity.PublishYear = issue.PublishYear;
                entity.Collected = issue.Collected;
                entity.Enabled = issue.Enabled;
                entity.IssueNumberSuffix = issue.IssueNumberSuffix;
                entity.FormatId = issue.FormatId;
                entity.Reprint = issue.Reprint;
                entity.DateAdded = issue.DateAdded;
                entity.CoverDate = issue.CoverDate;
                entity.DateLastUpdated = issue.DateLastUpdated;

                EntityChangeLogger.LogChanges(db.Entry(entity), "Issue", logger);

                await db.SaveChangesAsync();
            }
        }
        public async Task<List<int>> GetAllIssueIds()
        {
            using var db = this.contextFactory.CreateDbContext();
            var ids = await db.Issues.Select(x => x.Id).ToListAsync();
            return ids;
        }
        public async Task<bool> AnyIssuesAsync()
        {
            using var db = this.contextFactory.CreateDbContext();
            return await db.Issues.AnyAsync();
        }
        public async Task<DateTime?> GetMostRecentIssueDateAsync(DateTime? lastDate)
        {
            using var db = this.contextFactory.CreateDbContext();

            var mostRecent = await db.Issues
                .Where(x => x.DateAdded != null)
                .OrderByDescending(x => x.DateAdded)
                .FirstOrDefaultAsync();

            return mostRecent?.DateAdded;
        }
    }
}
