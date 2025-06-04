using Cinema.Contracts;
using FluentValidation;

namespace Cinema.Validators
{
    public class MovieDtoValidator : AbstractValidator<MovieDto>
    {
        public MovieDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Movie title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.Duration)
                .NotEmpty().WithMessage("Duration is required")
                .GreaterThan(TimeSpan.Zero).WithMessage("Duration must be greater than zero");

            RuleFor(x => x.Rating)
                .NotEmpty().WithMessage("Rating is required")
                .GreaterThan(0).WithMessage("Rating must be greater than 0")
                .LessThan(10).WithMessage("Rating must be less than 10");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author name is required")
                .MaximumLength(100).WithMessage("Author name cannot exceed 100 characters");
        }
    }
}
