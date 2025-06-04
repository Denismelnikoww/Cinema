using Cinema.Contracts;
using FluentValidation;

namespace Cinema.Validators
{
    public class RegisterRequestValidator : AbstractValidator<Contracts.RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("Email address is required")
                 .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");
        }
    }
}
