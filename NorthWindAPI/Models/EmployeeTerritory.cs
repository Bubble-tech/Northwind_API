using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class EmployeeTerritory
{
    public int EmployeeId { get; set; }

    public int TerritoryId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Territory Territory { get; set; } = null!;
}
