using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class IssueFormatEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool Enabled { get; set; }

    public virtual ICollection<IssueEntity> Issues { get; set; } = new List<IssueEntity>();
}
