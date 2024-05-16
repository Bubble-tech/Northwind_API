using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class UsState
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    public string StateAbbr { get; set; } = null!;

    public string? StateRegion { get; set; }
}
