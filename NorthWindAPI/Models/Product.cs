using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int SupplierId { get; set; }

    public int CategoryId { get; set; }

    public string QuantityPerUnit { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int UnitsInStock { get; set; }

    public int UnitsOnOrder { get; set; }

    public int ReorderLevel { get; set; }

    public int Discontinued { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
