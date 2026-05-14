using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class ComicEventReadingOrderWithComicPath
{
    public int Id { get; set; }

    public string? EventName { get; set; }

    public int Order { get; set; }

    public string? VolumeName { get; set; }

    public string Issue { get; set; } = null!;

    public string Filename { get; set; } = null!;
}
