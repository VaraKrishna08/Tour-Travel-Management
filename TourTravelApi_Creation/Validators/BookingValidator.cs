using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class BookingValidator :AbstractValidator<BookingModel>
    {
        public BookingValidator() {

            RuleFor(b => b.BookingDate)
       .LessThanOrEqualTo(DateTime.Now).WithMessage("Booking date must be today or in the past.");

            RuleFor(b => b.TravelDate)
                .GreaterThanOrEqualTo(b => b.BookingDate).WithMessage("Travel date must be on or after the booking date.");

            RuleFor(b => b.NumberOfPeople)
                .GreaterThan(0).WithMessage("Number of people must be greater than 0.");

            //RuleFor(b => b.TotalAmount)
            //    .Equal(b => b.NumberOfPeople).WithMessage("Total amount must be cost multiplied by number of people.");

            // Status Validation
            RuleFor(t => t.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => status == "Pending" || status == "Confirmed" || status == "Cancelled")
                .WithMessage("Status must be 'Pending', 'Confirmed', or 'Cancelled'.");

        }
    }
}
