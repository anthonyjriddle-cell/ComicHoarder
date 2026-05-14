using System;
using System.Collections.Generic;

namespace Utility.DBScaffold.Models;

public partial class Volume
{
    public int Id { get; set; }

    public int? PublisherId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateLastUpdated { get; set; }

    public bool Collectable { get; set; }

    public int CountOfIssues { get; set; }

    public int? StartYear { get; set; }

    public bool Enabled { get; set; }

    public bool Complete { get; set; }

    public bool Digital { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Publisher? Publisher { get; set; }
}
