using Microsoft.AspNetCore.Mvc;
using System.Linq;

using NorthWindAPI.Models;
using NorthWindAPI.DTO;

namespace Northwind_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly NorthwindDataContext _context;

        public SuppliersController(NorthwindDataContext context)
        {
            _context = context;
        }

        [HttpGet("Traders")]
        public IActionResult GetSupplierList()
        {
            var suppliers = _context.Suppliers
                .OrderBy(s => s.CompanyName)
                .Select(s => new SupplierList
                {
                    CompanyName = s.CompanyName,
                    ContactName = s.ContactName,
                    ContactTitle = s.ContactTitle,
                    City = s.City,
                    Country = s.Country,
                    Address = s.Address,
                    Phone = s.Phone,
                   

                })
                .ToList();

            return Ok(suppliers);
        }
    }
}
