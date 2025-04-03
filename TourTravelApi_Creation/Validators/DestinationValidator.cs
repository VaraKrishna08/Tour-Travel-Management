using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class DestinationValidator : AbstractValidator<DestinationModel>
    {
        public DestinationValidator() {
            RuleFor(d => d.DestinationName)
                .NotEmpty().WithMessage("Destination name is required.")
                .MaximumLength(100).WithMessage("Destination name must not exceed 100 characters.");

            RuleFor(d => d.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(50).WithMessage("Country name must not exceed 50 characters.");

            RuleFor(d => d.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(50).WithMessage("State name must not exceed 50 characters.");

            RuleFor(d => d.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(d => d.BestTimeToVisit)
                .NotEmpty().WithMessage("Best time to visit is required.")
                .MaximumLength(100).WithMessage("Best time to visit must not exceed 100 characters.");

        }
    }
}
