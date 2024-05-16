using System.ComponentModel.DataAnnotations;

namespace NorthWindAPI.DTO
{

    public class ProductsSummary
    {

        [Key]
        public int Id { get; set; }

        public string CategoryName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string QuantityPerUnit { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public decimal StockValue { get; set; }
        public int UnitsOnOrder { get; set; }
        public decimal OrderValue { get; set; }
        public int ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
    }
}
