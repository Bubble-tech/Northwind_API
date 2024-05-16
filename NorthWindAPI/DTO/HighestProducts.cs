namespace NorthWindAPI.DTO
{
    public class HighestProducts
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal TotalSales { get; set; }
    }
}
