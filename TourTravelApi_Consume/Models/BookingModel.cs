using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class BookingModel
    {
        public int? BookingID { get; set; }
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerID { get; set; }
        public string? FullName { get; set; }
        [Required(ErrorMessage = "Package ID is required.")]
        public int PackageID { get; set; }
        public string? PackageName { get; set; }
        [Required(ErrorMessage = "Booking Date is required.")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }

        [Required(ErrorMessage = "Travel Date is required.")]
        [DataType(DataType.Date)]
        //[FutureDate(ErrorMessage = "Travel Date must be in the future.")]
        public DateTime TravelDate { get; set; }

        [Required(ErrorMessage = "Number of People is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of People must be at least 1.")]
        public int NumberOfPeople { get; set; }

        [Required(ErrorMessage = "Total Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total Amount cannot be negative.")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; }
    }
    public class CustomerDropDownModel
    {
        public int CustomerID { get; set; }
         public string FullName { get; set; }
    }
}

//public class FutureDateAttribute : ValidationAttribute
//{
//    public override bool IsValid(object value)
//    {
//        if (value is DateTime dateValue)
//        {
//            return dateValue > DateTime.Now;
//        }
//        return false;
//    }
//}