using Cinema.Contracts;
using FluentValidation;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace Cinema.Validators
{
    public class LoginRequestValidator : AbstractValidator<Contracts.LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required")
                .EmailAddress().WithMessage("A valid email address is required");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long");
        }

    }
}
