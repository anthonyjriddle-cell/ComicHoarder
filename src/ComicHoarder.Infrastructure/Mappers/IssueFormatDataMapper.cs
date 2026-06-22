using ComicHoarder.Infrastructure.Models;
using ComicHoarder.Domain.Models;

namespace ComicHoarder.Infrastructure.Mappers
{
    public static class IssueFormatDataMapper
    {
        public static IssueFormat ToDomain(IssueFormatEntity entity)
        {
            if (entity == null)
                return new IssueFormat();

            return new IssueFormat
            {
                Id = entity.Id,
                Name = entity.Name ?? string.Empty,
                Enabled = entity.Enabled            
            };
        }

        public static IssueFormatEntity ToEntity(IssueFormat domain)
        {
            if (domain == null)
                return new IssueFormatEntity();

            return new IssueFormatEntity
            {
                Id = domain.Id,
                Name = domain.Name,
                Enabled = domain.Enabled
            };
        }
    }
}
