using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class EventTypeEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();
}
