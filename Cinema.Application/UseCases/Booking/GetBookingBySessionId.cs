using Cinema.Contracts;
using Cinema.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace Cinema.Application.UseCases.Booking
{
    public class GetBookingBySessionId
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingBySessionId(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IActionResult> Execute(int sessionId,
                                                 CancellationToken cancellationToken)
        {

            var bookingsEntity = await _bookingRepository.GetBySessionId(sessionId, cancellationToken);

            if (bookingsEntity.Count == 0)
            {
                return new BadRequestObjectResult("Booking on this session does not exist");
            }

            List<BookingDto> bookingsDto = [];

            foreach (var bookingEntity in bookingsEntity)
            {
                bookingsDto.Add(Mapper.MapToDto(bookingEntity));
            }

            return new OkObjectResult(bookingsDto);
        }
    }
}
