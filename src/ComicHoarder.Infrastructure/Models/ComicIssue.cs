using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class ComicIssue
{
    public int Id { get; set; }

    public double IssueNumber { get; set; }

    public string IssueNumberSuffix { get; set; } = null!;

    public string? IssueName { get; set; }

    public int? PublishMonth { get; set; }

    public int? PublishYear { get; set; }

    public int? VolumeId { get; set; }

    public string? VolumeName { get; set; }

    public string? PublisherName { get; set; }

    public bool? Reprint { get; set; }

    public bool Collected { get; set; }

    public bool Collectable { get; set; }

    public string? Format { get; set; }

    public string? Description { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateLastUpdated { get; set; }
}
