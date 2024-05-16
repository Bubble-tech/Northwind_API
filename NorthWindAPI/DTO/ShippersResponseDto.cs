namespace NorthWindAPI.DTO
{
    public class ShippersResponseDto
    {
        public int ShippersId { get; set; }
        public string CompanyName { get; set; } = null!;
        public int TotalQty { get; set;}
    }
}
