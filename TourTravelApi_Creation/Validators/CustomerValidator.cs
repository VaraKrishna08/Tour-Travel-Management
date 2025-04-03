using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerModel>
    {
        public CustomerValidator() {
            RuleFor(t => t.FullName)
               .NotEmpty().WithMessage("Full name is required.")
               .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(t => t.Phone)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^[0-9]{10}$").WithMessage("Phone number must be 10 digits.");

            RuleFor(t => t.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(t => t.RegistrationDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Registration date must be today or in the past.");

        }
    }
}
