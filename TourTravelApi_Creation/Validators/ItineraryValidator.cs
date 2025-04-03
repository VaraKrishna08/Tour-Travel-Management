using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class ItineraryValidator : AbstractValidator<ItineraryModel>
    {
        public ItineraryValidator()
        {
            //RuleFor(i => i.DayNumber)
            //    .InclusiveBetween(1, 7).WithMessage("Day number must be between 1 (Monday) and 7 (Sunday).");

            RuleFor(i => i.Activity)
                .NotEmpty().WithMessage("Activity is required.")
                .MaximumLength(100).WithMessage("Activity must not exceed 100 characters.");

            RuleFor(i => i.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(50).WithMessage("Location must not exceed 50 characters.");

            //RuleFor(i => i.Time)
            //    .GreaterThan(DateTime.Now).WithMessage("Time must be in the future.")
            //    .NotEmpty().WithMessage("Time is required.");
        }
    }
}
