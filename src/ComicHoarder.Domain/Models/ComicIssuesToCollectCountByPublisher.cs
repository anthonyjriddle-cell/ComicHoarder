using System;
using System.Collections.Generic;

namespace ComicHoarder.Domain.Models;

public partial class ComicIssuesToCollectCountByPublisher
{
    public int Id { get; set; }

    public string? Publisher { get; set; }

    public int? UncollectedCount { get; set; }

    public int? CollectedCount { get; set; }
}
