using ComicHoarder.Domain.Models;
using ComicHoarder.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure.Mappers
{
    public class VolumeDataMapper
    {
        public static Volume ToDomain(VolumeEntity data)
        {
            if (data == null)
                return null;

            return new Volume
            {
                id = data.Id,
                name = data.Name,
                description = data.Description,
                publisherId = data.PublisherId ?? 0,
                startYear = data.StartYear ?? 0,
                enabled = data.Enabled,
                dateLastUpdated = data.DateLastUpdated,
                dateAdded = data.DateAdded,
                collectable = data.Collectable,
                complete = data.Complete,
                countOfIssues = data.CountOfIssues
            };
        }

        public static VolumeEntity ToData(Volume domain)
        {
            if (domain == null)
                return null;

            return new VolumeEntity
            {
                Id = domain.id,
                Name = domain.name,
                Description = domain.description,
                PublisherId = domain.publisherId,
                StartYear = domain.startYear,
                Enabled = domain.enabled,
                DateLastUpdated = domain.dateLastUpdated,
                DateAdded = domain.dateAdded,
                Collectable = domain.collectable,
                Complete = domain.complete,
                CountOfIssues = domain.countOfIssues
            };
        }
    }
}
