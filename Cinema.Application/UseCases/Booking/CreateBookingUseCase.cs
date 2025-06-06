using Cinema.Contracts;
using Cinema.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ResultSharp.Core;
using ResultSharp.Errors;
using System.Collections.Generic;
using System.Text.Json;

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

        public async Task<Result<string>> ExecuteAsync(BookingDto bookingDto, 
            CancellationToken cancellationToken)
        {
            var validateResult = await _validator.ValidateAsync(bookingDto,
                cancellationToken);
            
            if (!validateResult.IsValid)
            {
                return Error.BadRequest(JsonSerializer
                    .Serialize(validateResult.ToDictionary()));
            }

            await _bookingRepository.Add(bookingDto.SessionId,
                                         bookingDto.UserId,
                                         bookingDto.SeatNumber,
                                         cancellationToken);

            return Result.Success("Booking successful create");
        }
    }
}
