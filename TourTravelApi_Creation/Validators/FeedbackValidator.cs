using FluentValidation;
using TourTravelApi_Creation.Models;

namespace TourTravelApi_Creation.Validators
{
    public class FeedbackValidator : AbstractValidator<FeedbackModel>
    {
        public FeedbackValidator() {
            RuleFor(f => f.Rating)
               .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(f => f.Comments)
                .MaximumLength(300).WithMessage("Comments must not exceed 300 characters.");

            RuleFor(f => f.FeedbackDate)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Feedback date must not be in the future.");

        }
    }
}
