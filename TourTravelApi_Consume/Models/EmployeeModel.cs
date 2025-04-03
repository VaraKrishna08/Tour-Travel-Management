using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class EmployeeModel
    {
        public int? EmployeeID { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        //[StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        [StringLength(50, ErrorMessage = "Position cannot exceed 50 characters")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact number must be exactly 10 digits")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Contact number must contain only digits")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(0, 1000000, ErrorMessage = "Salary must be between 0 and 1,000,000")]
        public decimal Salary { get; set; }
    }
}
