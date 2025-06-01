using Cinema.Contracts;
using FluentValidation;

namespace Cinema.Validators
{
    public class HallDtoValidator : AbstractValidator<HallDto>
    {
        public HallDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hall name is required")
                .MaximumLength(100).WithMessage("Hall name cannot exceed 100 characters");

            RuleFor(x => x.CountSeats)
                .NotEmpty().WithMessage("Seat count is required")
                .GreaterThan(0).WithMessage("Seat count must be greater than zero");
        }
    }
}
