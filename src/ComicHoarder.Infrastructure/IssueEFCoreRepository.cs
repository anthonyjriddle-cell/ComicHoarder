using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using ComicHoarder.Application.Interfaces;

namespace ComicHoarder.Infrastructure
{
    public class IssueEFCoreRepository : IIssueRepository
    {
        private readonly IDbContextFactory<CHContext> contextFactory;

        public IssueEFCoreRepository(IDbContextFactory<CHContext> contextFactory)
        {
            this.contextFactory = contextFactory;
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

                await db.SaveChangesAsync();
            }
        }
    }
}
