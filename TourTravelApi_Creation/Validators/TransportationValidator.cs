using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class TransportationValidator : AbstractValidator<TransportationModel>
    {
        public TransportationValidator() {
            RuleFor(t => t.TransportMode)
          .NotEmpty().WithMessage("Transport mode is required.")
          .MaximumLength(50).WithMessage("Transport mode must not exceed 50 characters.");

            RuleFor(t => t.TransportDetails)
                .MaximumLength(100).WithMessage("Transport details must not exceed 100 characters.");

            RuleFor(t => t.DepartureTime)
                .NotEmpty().WithMessage("Departure time is required.")
                .LessThan(t => t.ArrivalTime).WithMessage("Departure time must be before arrival time.");

            RuleFor(t => t.ArrivalTime)
                .GreaterThan(DateTime.Now).WithMessage("Arrival time must be in the future.");

            RuleFor(t => t.Cost)
                .GreaterThan(0).WithMessage("Cost must be a positive value.");


        }
    }
}
