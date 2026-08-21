using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class PublisherEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool Enabled { get; set; }

    public DateTime? DateLastUpdated { get; set; }
    public string? ImageLink { get; set; }

    public virtual ICollection<EventEntity> Events { get; set; } = new List<EventEntity>();

    public virtual ICollection<VolumeEntity> Volumes { get; set; } = new List<VolumeEntity>();
}
