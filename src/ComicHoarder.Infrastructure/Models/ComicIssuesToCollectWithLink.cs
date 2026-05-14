using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class ComicIssuesToCollectWithLink
{
    public int IssueId { get; set; }

    public string? IssueName { get; set; }

    public int VolumeId { get; set; }

    public double IssueNumber { get; set; }

    public string IssueNumberSuffix { get; set; } = null!;

    public string? Volume { get; set; }

    public int? PublishMonth { get; set; }

    public int? PublishYear { get; set; }

    public string? PublisherName { get; set; }

    public string Link { get; set; } = null!;

    public string? Description { get; set; }
}
