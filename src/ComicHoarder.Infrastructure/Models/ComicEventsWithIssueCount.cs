using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class ComicEventsWithIssueCount
{
    public int Id { get; set; }

    public string? EventName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Type { get; set; }

    public string? PublisherName { get; set; }

    public int Order { get; set; }

    public bool Enabled { get; set; }

    public int? NumberOfIssues { get; set; }
}
