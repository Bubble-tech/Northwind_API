using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class Shipper
{
    public int ShippersId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Phone { get; set; } = null!;
}
