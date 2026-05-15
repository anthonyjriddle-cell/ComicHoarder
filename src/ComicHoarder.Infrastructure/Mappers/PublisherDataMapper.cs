using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure.Mappers
{
    public static class PublisherDataMapper
    {
        public static Publisher ToDomain(PublisherEntity data)
        {
            if (data == null)
                return null;

            return new Publisher
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Enabled = data.Enabled,
                DateLastUpdated = data.DateLastUpdated
            };
        }

        public static PublisherEntity ToData(Publisher domain)
        {
            if (domain == null)
                return null;

            return new PublisherEntity
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                Enabled = domain.Enabled,
                DateLastUpdated = domain.DateLastUpdated
            };
        }
    }
}
