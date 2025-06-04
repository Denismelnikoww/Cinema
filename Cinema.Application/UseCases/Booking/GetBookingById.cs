using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Application.UseCases.Booking
{
    public class GetBookingById
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingById(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> Execute(int id,
            CancellationToken cancellationToken)
        {
            var bookingEntity = await _bookingRepository.GetById(id, cancellationToken);

            if (bookingEntity == null)
            {
                return new BadRequestObjectResult("Booking with this Id does not exist");
            }

            return new OkObjectResult(Mapper.MapToDto(bookingEntity));
        }
    }
}
