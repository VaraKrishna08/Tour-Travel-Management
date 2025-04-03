using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class PackageValidator : AbstractValidator<PackageModel>
    {
        public PackageValidator() {
            RuleFor(p => p.PackageName)
           .NotEmpty().WithMessage("Package name is required.")
           .MaximumLength(100).WithMessage("Package name must not exceed 100 characters.");

            
            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative value.");

            RuleFor(p => p.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than zero.");

            RuleFor(p => p.Status)
                .NotEmpty().WithMessage("Status is required.")
                .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");

        }
    }
}
