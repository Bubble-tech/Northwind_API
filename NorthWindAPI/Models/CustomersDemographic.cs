using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class CustomersDemographic
{
    public int CustomerTypeId { get; set; }

    public string? CustomerDesc { get; set; }
}
