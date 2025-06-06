using Cinema.Contracts;
using Cinema.Interfaces;
using ResultSharp.Core;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Booking
{
    public class GetBookingByUserIdUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByUserIdUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<List<BookingDto>>> ExecuteAsync(int userId,
                CancellationToken cancellationToken)
        {
            var bookingsEntity = await _bookingRepository.GetByUserId(userId, cancellationToken);

            if (bookingsEntity.Count == 0)
            {
                return Error.BadRequest("Booking on this user does not exist");
            }

            var bookingsDto = bookingsEntity.Select(Mapper.MapToDto).ToList();

            return Result.Success(bookingsDto);
        }
    }
}
