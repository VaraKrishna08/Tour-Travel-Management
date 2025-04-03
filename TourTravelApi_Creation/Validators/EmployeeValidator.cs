using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeModel>
    {
        public EmployeeValidator() {
            RuleFor(e => e.FullName)
           .NotEmpty().WithMessage("Full name is required.")
           .MaximumLength(100).WithMessage("Full name must not exceed 100 characters.");

            RuleFor(e => e.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(50).WithMessage("Position must not exceed 50 characters.");

            RuleFor(e => e.ContactNumber)
                .NotEmpty().WithMessage("Contact number is required.")
                .Matches(@"^[0-9]{10}$").WithMessage("Contact number must be exactly 10 digits.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(e => e.Salary)
                .GreaterThanOrEqualTo(0).WithMessage("Salary must be a positive value.");

        }
    }
}
