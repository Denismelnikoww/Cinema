using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Booking
{
    public class GetBookingByIdUseCase
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByIdUseCase(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Result<BookingDto>> ExecuteAsync(int id,
            CancellationToken cancellationToken)
        {
            var bookingEntity = await _bookingRepository.GetById(id, cancellationToken);

            if (bookingEntity == null)
            {
                return Error.BadRequest("Booking with this Id does not exist");
            }

            return Result.Success(Mapper.MapToDto(bookingEntity));
        }
    }
}
