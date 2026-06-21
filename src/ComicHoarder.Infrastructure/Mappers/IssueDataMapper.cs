using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Models;

namespace ComicHoarder.Infrastructure.Mappers
{
    public static class IssueDataMapper
    {
        public static Issue ToDomain(IssueEntity entity)
        {
            if (entity == null)
                return new Issue();

            return new Issue
            {
                Id = entity.Id,
                VolumeId = entity.VolumeId ?? 0,
                Name = entity.Name ?? string.Empty,
                IssueNumber = (float)entity.IssueNumber,
                PublishMonth = entity.PublishMonth ?? 0,
                PublishYear = entity.PublishYear ?? 0,
                Collected = entity.Collected,
                Enabled = entity.Enabled,
                IssueNumberSuffix = entity.IssueNumberSuffix ?? string.Empty,
                FormatId = entity.FormatId,
                Reprint = entity.Reprint ?? false,
                DateAdded = entity.DateAdded,
                CoverDate = entity.CoverDate,
                DateLastUpdated = entity.DateLastUpdated,

                Summary = null
            };
        }

        public static IssueEntity ToEntity(Issue domain)
        {
            if (domain == null)
                return new IssueEntity();

            return new IssueEntity
            {
                Id = domain.Id,
                VolumeId = domain.VolumeId,
                Name = domain.Name,
                IssueNumber = domain.IssueNumber,
                PublishMonth = domain.PublishMonth,
                PublishYear = domain.PublishYear,
                Collected = domain.Collected,
                Enabled = domain.Enabled,
                IssueNumberSuffix = domain.IssueNumberSuffix,
                FormatId = domain.FormatId,
                Reprint = domain.Reprint,
                DateAdded = domain.DateAdded,
                CoverDate = domain.CoverDate,
                DateLastUpdated = domain.DateLastUpdated
            };
        }
    }
}
