using System;
using System.Collections.Generic;

namespace Utility.DBScaffold.Models;

public partial class CollectedIssue
{
    public int Id { get; set; }

    public int ParentId { get; set; }

    public int ChildId { get; set; }

    public virtual Issue Child { get; set; } = null!;

    public virtual Issue Parent { get; set; } = null!;
}
