using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class EventIssueEntity
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int IssueId { get; set; }

    public int Order { get; set; }

    public virtual EventEntity Event { get; set; } = null!;

    public virtual IssueEntity Issue { get; set; } = null!;
}
