using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWindAPI.DTO;
using NorthWindAPI.Models;
using static NuGet.Packaging.PackagingConstants;

namespace NorthWindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly NorthwindDataContext _context;

        public ShippersController(NorthwindDataContext context)
        {
            _context = context;
        }

        [HttpGet("ShippingVolumes")]
        public IActionResult GetShippingVolumes()
        {
            var _shippers = _context.Shippers
                .Select(s => new ShippersResponseDto
                {
                    CompanyName = s.CompanyName,
                    ShippersId = s.ShippersId,
                    TotalQty = _context.Orders
                        .Where(o => o.ShipVia == s.ShippersId)
                        .Sum(o => _context.OrderDetails.Where(od => od.OrderId == o.OrderId).Sum(od => (int?)od.Quantity)) ?? 0
                })
                .Where(shipper => shipper.TotalQty > 0)
                .OrderByDescending(shipper => shipper.TotalQty)
                .ToList();

            return Ok(_shippers);

        }


    }
    }


