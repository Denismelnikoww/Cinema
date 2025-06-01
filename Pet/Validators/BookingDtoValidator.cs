using Cinema.Contracts;
using FluentValidation;

namespace Cinema.Validators
{
    public class BookingDtoValidator : AbstractValidator<BookingDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User Id is required")
                .GreaterThan(0).WithMessage("User Id must be a positive number");

            RuleFor(x => x.SeatNumber)
                .NotEmpty().WithMessage("Seat number is required")
                .GreaterThan(0).WithMessage("User Id must be a positive number");

            RuleFor(x => x.SessionId)
                .NotEmpty().WithMessage("Session Id is required")
                .GreaterThan(0).WithMessage("Sesion Id must be a positive number");
        }
    }
}
