using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthWindAPI.DTO;
using NorthWindAPI.Models;
using System;
using System.Globalization;

namespace Northwind_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly NorthwindDataContext _context;

        public EmployeesController(NorthwindDataContext context)
        {
            _context = context;
        }

        [HttpGet("GeographicalDetails ")]
        public IActionResult GetEmployeeGeographicDetails()
        {
            List<EmployeeGeographicalList> employeeDetails = _context.Employees
                .Join(_context.EmployeeTerritories,
                    e => e.EmployeeId,
                    et => et.EmployeeId,
                    (e, et) => new EmployeeGeographicalList
                    {
                        Region = et.Territory.Region.RegionDescription,
                        Territory = et.Territory.TerritoryDescription,
                        Title = e.Title,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        HireDate = e.HireDate!.Value.ToString("yyyy-MM-dd"),
                    })
                .ToList();

            return Ok(employeeDetails);
        }

       

        [HttpGet("TopSalesPersonnels")]

        public IActionResult GetTopSalesPersonnel(decimal threshold, DateTime startDate, DateTime endDate)
        {

            var join1 = _context.Employees
      .Join(_context.Orders, e => e.EmployeeId, o => o.EmployeeId, (e, o) => new { Employee = e, Order = o })
      .Join(_context.OrderDetails, eo => eo.Order.OrderId, od => od.OrderId, (eo, od) => new { eo.Employee, eo.Order, OrderDetail = od });


            var filteredOrders = join1
                   .Where(jo => jo.Order.OrderDate >= new DateOnly(startDate.Date.Year, startDate.Date.Month, startDate.Date.Day) && jo.Order.OrderDate <= new DateOnly(endDate.Date.Year, endDate.Date.Month, endDate.Date.Day));
                    var groupByEmployee = filteredOrders
             .GroupBy(j => new { j.Employee.EmployeeId, EmployeeName = j.Employee.FirstName + " " + j.Employee.LastName })
             .Select(group => new
             {
                 EmployeeId = group.Key.EmployeeId,
                 EmployeeName = group.Key.EmployeeName,
                 TotalSales = group.Sum(x => x.OrderDetail.Quantity * x.OrderDetail.UnitPrice)

             });

            var top10Employees = groupByEmployee
        .Where(x => x.TotalSales >= threshold)
        .OrderByDescending(x => x.TotalSales)
        .Take(10)
        .ToList();


            return Ok(top10Employees);

        }


        [HttpGet("SalesPersonnelRegional")]
        public IActionResult GetTop10SalesPersonnelRegional()
        {
            try
            {
                var query = (from e in _context.Employees
                             join o in _context.Orders on e.EmployeeId equals o.EmployeeId
                             join od in _context.OrderDetails on o.OrderId equals od.OrderId
                             group new { e, od } by new { e.EmployeeId, e.LastName, e.FirstName, e.Region } into g
                             select new
                             {
                                 EmployeeId = g.Key.EmployeeId,
                                 LastName = g.Key.LastName,
                                 FirstName = g.Key.FirstName,
                                 Region = g.Key.Region,
                                 TotalSales = g.Sum(x => (decimal?)x.od.Quantity * x.od.UnitPrice * (1 - x.od.Discount)) ?? 0
                             })
                     .ToList()
                     .GroupBy(x => x.Region)
                     .SelectMany(g =>
                         g.OrderByDescending(x => x.TotalSales)
                          .Select((x, index) => new { x, row_num = index + 1 })
                          .Where(x => x.row_num <= 10)
                          .Select(x => x.x))
                     .ToList();

                return Ok(query);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }




        [HttpGet("HiringTrends")]
        public IActionResult GetEmployeeHiringTrends()
        {
            try
            {
                var hiringTrends = _context.Employees
                    .GroupBy(e => new
                    {
                        Year = e.HireDate.Value.Year,
                        Month = e.HireDate.Value.Month
                    })
                    .ToList()
                    .Select(g => new EmployeeHiringTrends
                    {
                        MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"),
                        HiringCount = g.Count()
                    })
                    .OrderBy(g => g.MonthName)
                    .ToList();

                return Ok(hiringTrends);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }



    }


}

