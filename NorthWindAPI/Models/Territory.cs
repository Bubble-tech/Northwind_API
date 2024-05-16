using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class Territory
{
    public int TerritoryId { get; set; }

    public string TerritoryDescription { get; set; } = null!;

    public int RegionId { get; set; }

    public virtual Region Region { get; set; } = null!;
}
