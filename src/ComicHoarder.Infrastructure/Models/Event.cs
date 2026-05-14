using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class Event
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int Type { get; set; }

    public int Order { get; set; }

    public int? PublisherId { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<EventIssue> EventIssues { get; set; } = new List<EventIssue>();

    public virtual Publisher? Publisher { get; set; }

    public virtual EventType TypeNavigation { get; set; } = null!;
}
