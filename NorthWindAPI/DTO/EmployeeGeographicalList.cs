namespace NorthWindAPI.DTO
{
    public class EmployeeGeographicalList
    {
        public string Region { get; set; } = null!;
        public string Territory { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? HireDate { get; set; } = null;
    }
}
