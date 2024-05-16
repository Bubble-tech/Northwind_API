using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string Desciption { get; set; } = null!;

    public byte[] Picture { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
