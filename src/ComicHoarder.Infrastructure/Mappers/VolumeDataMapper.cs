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
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                PublisherId = data.PublisherId ?? 0,
                StartYear = data.StartYear ?? 0,
                Enabled = data.Enabled,
                DateLastUpdated = data.DateLastUpdated,
                DateAdded = data.DateAdded,
                Collectable = data.Collectable,
                Complete = data.Complete,
                CountOfIssues = data.CountOfIssues,
                ImageLink = data.ImageLink
            };
        }

        public static VolumeEntity ToData(Volume domain)
        {
            if (domain == null)
                return null;

            return new VolumeEntity
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                PublisherId = domain.PublisherId,
                StartYear = domain.StartYear,
                Enabled = domain.Enabled,
                DateLastUpdated = domain.DateLastUpdated,
                DateAdded = domain.DateAdded,
                Collectable = domain.Collectable,
                Complete = domain.Complete,
                CountOfIssues = domain.CountOfIssues,
                ImageLink = domain.ImageLink
            };
        }
    }
}
