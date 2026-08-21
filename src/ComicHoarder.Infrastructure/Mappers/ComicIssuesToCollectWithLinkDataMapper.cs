using ComicHoarder.Infrastructure.Models;
using ComicHoarder.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicHoarder.Infrastructure.Mappers
{
    public static class ComicIssuesToCollectWithLinkDataMapper
    {
        public static ComicIssuesToCollectWithLink ToDomain(ComicIssuesToCollectWithLinkEntity data)
        {
            if (data == null)
                return null;

            return new ComicIssuesToCollectWithLink
            {
                IssueId = data.IssueId,
                Description = data.Description,
                IssueName = data.IssueName ?? "",
                IssueNumber = data.IssueNumber,
                IssueNumberSuffix = data.IssueNumberSuffix ?? "",
                Link = data.Link,
                PublisherName = data.PublisherName,
                PublishMonth = data.PublishMonth ?? 0,
                PublishYear = data.PublishYear ?? 0,
                Volume = data.Volume,
                VolumeId = data.VolumeId
            };
        }
    }
}
