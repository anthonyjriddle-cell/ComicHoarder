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
                id = data.Id,
                name = data.Name,
                description = data.Description,
                enabled = data.Enabled,
                dateLastUpdated = data.DateLastUpdated
            };
        }

        public static PublisherEntity ToData(Publisher domain)
        {
            if (domain == null)
                return null;

            return new PublisherEntity
            {
                Id = domain.id,
                Name = domain.name,
                Description = domain.description,
                Enabled = domain.enabled,
                DateLastUpdated = domain.dateLastUpdated
            };
        }
    }
}
