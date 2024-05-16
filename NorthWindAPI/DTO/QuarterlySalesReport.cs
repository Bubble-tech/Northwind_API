namespace NorthWindAPI.DTO
{
    public class QuarterlySalesReport
    {
        public string Quarter { get; set; } = null!;
        public decimal TotalSales { get; set; }
    }
}
