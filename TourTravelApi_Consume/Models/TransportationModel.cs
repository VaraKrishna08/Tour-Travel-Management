using System.ComponentModel.DataAnnotations;

namespace TourTravelApi_Consume.Models
{
    public class TransportationModel
    {
        public int? TransportID { get; set; }
        [Required(ErrorMessage = "Booking ID is required.")]
        public int BookingID { get; set; }
        public DateTime? BookingDate { get; set; }

        [Required(ErrorMessage = "Transport Mode is required.")]
        [StringLength(50, ErrorMessage = "Transport Mode cannot exceed 50 characters.")]
        public string TransportMode { get; set; }

        [Required(ErrorMessage = "Transport Details are required.")]
        [StringLength(200, ErrorMessage = "Transport Details cannot exceed 200 characters.")]
        public string TransportDetails { get; set; }

        [Required(ErrorMessage = "Departure Time is required.")]
        [DataType(DataType.DateTime)]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Arrival Time is required.")]
        [DataType(DataType.DateTime)]
        //[ArrivalTimeValidation("DepartureTime", ErrorMessage = "Arrival time must be after the departure time.")]
        public DateTime ArrivalTime { get; set; }

        [Required(ErrorMessage = "Cost is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be a non-negative value.")]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }
    }
   
}

//public class ArrivalTimeValidationAttribute : ValidationAttribute
//{
//    private readonly string _departureProperty;

//    public ArrivalTimeValidationAttribute(string departureProperty)
//    {
//        _departureProperty = departureProperty;
//    }

//    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//    {
//        var departureTimeProperty = validationContext.ObjectType.GetProperty(_departureProperty);
//        if (departureTimeProperty == null)
//            return new ValidationResult($"Unknown property: {_departureProperty}");

//        var departureValue = departureTimeProperty.GetValue(validationContext.ObjectInstance);
//        if (value is DateTime arrivalTime && departureValue is DateTime departureTime)
//        {
//            if (arrivalTime <= departureTime)
//            {
//                return new ValidationResult(ErrorMessage);
//            }
//        }

//        return ValidationResult.Success;
//    }
//}