using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class ComicIssuesToCollect
{
    public int IssueId { get; set; }

    public int VolumeId { get; set; }

    public string? Volume { get; set; }

    public double IssueNumber { get; set; }

    public string IssueNumberSuffix { get; set; } = null!;

    public int? PublishMonth { get; set; }

    public int? PublishYear { get; set; }

    public string? PublisherName { get; set; }

    public bool Collected { get; set; }

    public bool Collectable { get; set; }

    public bool? Reprint { get; set; }

    public string? Format { get; set; }

    public string? Description { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateLastUpdated { get; set; }

    public string? IssueName { get; set; }
}
