using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class IssueEntity
{
    public int Id { get; set; }

    public int? VolumeId { get; set; }

    public string? Name { get; set; }

    public double IssueNumber { get; set; }

    public int? PublishMonth { get; set; }

    public int? PublishYear { get; set; }

    public bool Collected { get; set; }

    public bool Enabled { get; set; }

    public string? IssueNumberSuffix { get; set; }

    public int? FormatId { get; set; }

    public bool? Reprint { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? CoverDate { get; set; }

    public DateTime? DateLastUpdated { get; set; }
    public string? ImageLink { get; set; }
    public virtual ICollection<CollectedIssueEntity> CollectedIssueChildren { get; set; } = new List<CollectedIssueEntity>();

    public virtual ICollection<CollectedIssueEntity> CollectedIssueParents { get; set; } = new List<CollectedIssueEntity>();

    public virtual ICollection<EventIssueEntity> EventIssues { get; set; } = new List<EventIssueEntity>();

    public virtual IssueFormatEntity? Format { get; set; }

    public virtual VolumeEntity? Volume { get; set; }
}
