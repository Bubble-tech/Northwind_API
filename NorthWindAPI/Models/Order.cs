using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public string CustomerId { get; set; } = null!;

    public int EmployeeId { get; set; }

    public DateOnly OrderDate { get; set; }

    public DateOnly RequiredDate { get; set; }

    public DateOnly? ShippedDate { get; set; }

    public int ShipVia { get; set; }

    public decimal Freight { get; set; }

    public string ShipName { get; set; } = null!;

    public string ShipAddress { get; set; } = null!;

    public string ShipCity { get; set; } = null!;

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string ShipCountry { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
