using Microsoft.AspNetCore.Mvc;
using NorthWindAPI.DTO;
using NorthWindAPI.Models;
using System.Linq;
using static NuGet.Packaging.PackagingConstants;

namespace Northwind_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly NorthwindDataContext _context;

        public ProductsController(NorthwindDataContext context)
        {
            _context = context;
        }


        [HttpGet("Summary")]
        public IActionResult GetInventorySummary()
        {
            var inventorySummary = _context.Products
                .Join(_context.Categories,
                    p => p.CategoryId,
                    c => c.CategoryId,
                    (p, c) => new
                    {
                        c.CategoryName,
                        p.ProductName,
                        p.QuantityPerUnit,
                        p.UnitPrice,
                        p.UnitsInStock,
                        p.UnitsOnOrder,
                        p.ReorderLevel,
                        p.Discontinued,
                        StockValue = p.UnitPrice * p.UnitsInStock,
                        OrderValue = p.UnitPrice * p.UnitsOnOrder
                    })
                .ToList();

            return Ok(inventorySummary);
        }
        [HttpGet("HighestPerforming")]
        public IActionResult GetTopSellingProducts(decimal threshold, DateTime startDate, DateTime endDate)
        {
            var join1 = _context.Products
                    .Join(_context.OrderDetails, p => p.ProductId, od => od.ProductId, (p, od) => new { Product = p, OrderDetail = od });

            var join2 = join1
                    .Join(_context.Orders,
                          j => j.OrderDetail.OrderId,
                          o => o.OrderId,
                          (j, o) => new { j.Product, j.OrderDetail, Order = o })
                    .Where(jo => jo.Order.OrderDate >= new DateOnly(startDate.Date.Year, startDate.Date.Month, startDate.Date.Day) && jo.Order.OrderDate <= new DateOnly(endDate.Date.Year, endDate.Date.Month, endDate.Date.Day));


            var groupBy = join2
                .GroupBy(j => new { j.Product.ProductId, j.Product.ProductName })
                .Select(group => new
                {
                    ProductId = group.Key.ProductId,
                    ProductName = group.Key.ProductName,
                    TotalSales = group.Sum(x => x.OrderDetail.Quantity * x.OrderDetail.UnitPrice)
                });


            var filter = groupBy.Where(g => g.TotalSales > threshold);

            var orderBy = filter.OrderByDescending(g => g.TotalSales);

            return Ok(orderBy);
        }

        [HttpGet("LowestPerforming")]
        public IActionResult GetLowSellingProducts(decimal threshold, DateTime startDate, DateTime endDate)
        {
            var join1 = _context.Products
                .Join(_context.OrderDetails, p => p.ProductId, od => od.ProductId, (p, od) => new { Product = p, OrderDetail = od });

            var join2 = join1
                .Join(_context.Orders,
                      j => j.OrderDetail.OrderId,
                      o => o.OrderId,
                      (j, o) => new { j.Product, j.OrderDetail, Order = o })
                .Where(jo => jo.Order.OrderDate >= new DateOnly(startDate.Date.Year, startDate.Date.Month, startDate.Date.Day) && jo.Order.OrderDate <= new DateOnly(endDate.Date.Year, endDate.Date.Month, endDate.Date.Day));

            var groupBy = join2
                .GroupBy(j => new { j.Product.ProductId, j.Product.ProductName })
                .Select(group => new
                {
                    ProductId = group.Key.ProductId,
                    ProductName = group.Key.ProductName,
                    TotalSales = group.Sum(x => x.OrderDetail.Quantity * x.OrderDetail.UnitPrice)
                });

            var filter = groupBy.Where(g => g.TotalSales < threshold);

            var orderBy = filter.OrderBy(g => g.TotalSales);

            return Ok(orderBy);
        }

    }
}









