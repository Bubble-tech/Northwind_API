using System;
using System.Collections.Generic;

namespace NorthWindAPI.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string TitleOfCourtesy { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public DateOnly? HireDate { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string? Region { get; set; }

    public string PostalCode { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string HomePhone { get; set; } = null!;

    public string? Extension { get; set; }

    public string? Photo { get; set; }

    public string Notes { get; set; } = null!;

    public int? ReportsTo { get; set; }

    public string PhotoPath { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
