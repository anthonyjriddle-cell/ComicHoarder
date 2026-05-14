using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class IssueFormat
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Enabled { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
