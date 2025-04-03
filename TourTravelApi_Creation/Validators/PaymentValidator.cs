using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class PaymentValidator : AbstractValidator<PaymentModel>
    {
        public PaymentValidator() {
            RuleFor(pay => pay.PaymentDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Payment date must not be in the future.");

            RuleFor(pay => pay.PaymentMode)
                .NotEmpty().WithMessage("Payment mode is required.")
                .MaximumLength(50).WithMessage("Payment mode must not exceed 50 characters.");

            RuleFor(pay => pay.AmountPaid)
                .GreaterThanOrEqualTo(0).WithMessage("Amount paid must be a non-negative value.");

            RuleFor(pay => pay.PaymentStatus)
                .NotEmpty().WithMessage("Payment status is required.")
                .MaximumLength(50).WithMessage("Payment status must not exceed 50 characters.");

        }
    }
}
