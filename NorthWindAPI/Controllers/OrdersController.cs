using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NorthWindAPI.DTO;
using NorthWindAPI.Models;

namespace NorthWindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly NorthwindDataContext _context;

        public OrdersController(NorthwindDataContext context)
        {
            _context = context;
        }


        [HttpGet("QuarterlySalesReport")]
        public IActionResult GetQuarterlySalesReport()
        {
            try
            {
                var query = _context.Orders
                    .GroupBy(o => new
                    {
                        Quarter = o.OrderDate.Month >= 1 && o.OrderDate.Month <= 3 ? "JAN-MAR" :
                                  o.OrderDate.Month >= 4 && o.OrderDate.Month <= 6 ? "APR-JUN" :
                                  o.OrderDate.Month >= 7 && o.OrderDate.Month <= 9 ? "JUL-SEP" :
                                  "OCT-DEC",
                        Year = o.OrderDate.Year
                    })
                    .OrderBy(g => g.Key.Year)
                    .ThenBy(g => g.Select(x => x.OrderDate.Month).FirstOrDefault())
                    .Select(g => new
                    {
                        Quarter = g.Key.Quarter + " " + g.Key.Year,
                        TotalSales = g.Sum(x => x.Freight)
                    })
                    .ToList();

                return Ok(query);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

       






    }

}

