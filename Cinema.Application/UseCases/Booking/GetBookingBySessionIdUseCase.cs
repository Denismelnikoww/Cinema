using Cinema.Contracts;
using Cinema.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.Errors;
using System;
using System.Threading;

namespace Cinema.Application.UseCases.Booking
{
    public class GetBookingBySessionIdUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingBySessionIdUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<List<BookingDto>>> ExecuteAsync(int sessionId,
                CancellationToken cancellationToken)
        {
            var bookingsEntity = await _bookingRepository.FindBySessionAsync(sessionId, cancellationToken);

            if (bookingsEntity.Count == 0)
            {
                return Error.BadRequest("Booking on this session does not exist");
            }

            var bookingsDto = bookingsEntity.Select(Mapper.MapToDto).ToList();

            return Result.Success(bookingsDto);
        }
    }
}
