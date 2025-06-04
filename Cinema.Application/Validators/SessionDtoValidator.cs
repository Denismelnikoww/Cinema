using Cinema.Contracts;
using FluentValidation;

namespace Cinema.Validators
{
    public class SessionDtoValidator : AbstractValidator<SessionDto>
    {
        public SessionDtoValidator()
        {
            RuleFor(x => x.Price)
        .NotEmpty().WithMessage("Price is required");

            RuleFor(x => x.HallId)
                .NotEmpty().WithMessage("Hall Id is required")
                .GreaterThan(0).WithMessage("Hall Id must be a positive number");

            RuleFor(x => x.DateTime)
                .NotEmpty().WithMessage("Session date and time is required")
                .GreaterThan(DateTime.UtcNow).WithMessage("Session must be scheduled in the future");
        }
    }
}
