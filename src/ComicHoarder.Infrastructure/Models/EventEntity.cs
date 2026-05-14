using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class EventEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Type { get; set; }

    public int Order { get; set; }

    public int? PublisherId { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<EventIssueEntity> EventIssues { get; set; } = new List<EventIssueEntity>();

    public virtual PublisherEntity? Publisher { get; set; }

    public virtual EventTypeEntity TypeNavigation { get; set; } = null!;
}
