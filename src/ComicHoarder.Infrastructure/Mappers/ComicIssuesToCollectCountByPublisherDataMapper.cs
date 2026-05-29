using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Models;

namespace ComicHoarder.Infrastructure.Mappers
{
    public static class ComicIssuesToCollectCountByPublisherDataMapper
    {
        public static ComicIssuesToCollectCountByPublisher ToDomain(ComicIssuesToCollectCountByPublisherEntity data)
        {
            if (data == null)
                return null;

            return new ComicIssuesToCollectCountByPublisher
            {
                Id = data.Id,
                Publisher = data.Publisher,
                UncollectedCount = data.UncollectedCount,
                CollectedCount = data.CollectedCount
            };
        }
    }
}
