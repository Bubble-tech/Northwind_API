using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class CustomerCustomerDemo
{
    public string CustomerId { get; set; } = null!;

    public int CustomerTypeId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomersDemographic CustomerType { get; set; } = null!;
}
