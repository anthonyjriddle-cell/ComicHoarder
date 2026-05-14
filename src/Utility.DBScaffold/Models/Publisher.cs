using System;
using System.Collections.Generic;

namespace Utility.DBScaffold.Models;

public partial class Publisher
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool Enabled { get; set; }

    public DateTime? DateLastUpdated { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Volume> Volumes { get; set; } = new List<Volume>();
}
