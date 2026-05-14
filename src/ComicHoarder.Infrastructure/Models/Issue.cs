using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class Issue
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

    public virtual ICollection<CollectedIssue> CollectedIssueChildren { get; set; } = new List<CollectedIssue>();

    public virtual ICollection<CollectedIssue> CollectedIssueParents { get; set; } = new List<CollectedIssue>();

    public virtual ICollection<EventIssue> EventIssues { get; set; } = new List<EventIssue>();

    public virtual IssueFormat? Format { get; set; }

    public virtual Volume? Volume { get; set; }
}
