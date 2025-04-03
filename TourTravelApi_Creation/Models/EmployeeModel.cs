using Microsoft.AspNetCore.Http.HttpResults;

namespace TourTravelApi_Creation.Models
{
    public class EmployeeModel
    {
        public int? EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
    }
}
