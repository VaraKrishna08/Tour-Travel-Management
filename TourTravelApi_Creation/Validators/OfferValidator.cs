using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class OfferValidator : AbstractValidator<OfferModel>
    {
        public OfferValidator() {
            RuleFor(o => o.OfferName)
              .NotEmpty().WithMessage("Offer name is required.")
              .MaximumLength(100).WithMessage("Offer name must not exceed 100 characters.");

            RuleFor(o => o.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(o => o.DiscountPercentage)
                .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");

            RuleFor(o => o.EndDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("End date must be in the future.");

            RuleFor(o => o.ApplicablePackages)
                .MaximumLength(200).WithMessage("Applicable packages text must not exceed 200 characters.");
        }
    }
}
