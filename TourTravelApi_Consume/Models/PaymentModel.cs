using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class PaymentModel
    {
        public int? PaymentID { get; set; }
        [Required(ErrorMessage = "Booking ID is required.")]
        public int BookingID { get; set; }
        public DateTime? BookingDate { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment Mode is required.")]
        [StringLength(50, ErrorMessage = "Payment Mode cannot exceed 50 characters.")]
        public string PaymentMode { get; set; }

        [Required(ErrorMessage = "Amount Paid is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Amount Paid must be a non-negative value.")]
        [DataType(DataType.Currency)]
        public decimal AmountPaid { get; set; }

        [Required(ErrorMessage = "Payment Status is required.")]
        [StringLength(50, ErrorMessage = "Payment Status cannot exceed 50 characters.")]
        public string PaymentStatus { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaymentDate > DateTime.Now)
            {
                yield return new ValidationResult("Payment date must not be in the future.", new[] { nameof(PaymentDate) });
            }
        }
    }
    public class BookingDropDownModel
    {
        public int BookingID { get; set; }
        public string BookingDate { get; set; }
    }
}
