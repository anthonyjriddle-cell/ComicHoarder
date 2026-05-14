using System;
using System.Collections.Generic;

namespace Utility.DBScaffold.Models;

public partial class EventIssue
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public int IssueId { get; set; }

    public int Order { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Issue Issue { get; set; } = null!;
}
