using System;
using System.Collections.Generic;

namespace ComicHoarder.Infrastructure.Models;

public partial class SettingEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }
}
