using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class CollectedIssueEntity
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public int ChildId { get; set; }

    public virtual IssueEntity Child { get; set; } = null!;

    public virtual IssueEntity Parent { get; set; } = null!;
}
