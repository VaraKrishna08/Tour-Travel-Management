using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class CustomerModel
    {
        public int? CustomerID { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        //[StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Phone number must contain only digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Registration Date is required")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }
         [JsonProperty("password")] 
        public string? Password { get; set; }
    }
}
