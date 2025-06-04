using Cinema.Contracts;
using Cinema.Interfaces;
using FluentResults;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Application.UseCases.Booking
{
    public class CreateBookingUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IValidator<BookingDto> _validator;

        public CreateBookingUseCase(IBookingRepository bookingRepository,
            IValidator<BookingDto> validator)
        {
            _bookingRepository = bookingRepository;
            _validator = validator; 
        }

        public async Task<IActionResult> Execute(BookingDto bookingDto, 
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(bookingDto, cancellationToken);

            if (!validateResult.IsValid)
            {
                return new BadRequestObjectResult(validateResult.ToDictionary());
            }

            await _bookingRepository.Add(bookingDto.SessionId,
                                         bookingDto.UserId,
                                         bookingDto.SeatNumber,
                                         cancellationToken);

            return new OkObjectResult("Booking successful create");
        }
    }
}
