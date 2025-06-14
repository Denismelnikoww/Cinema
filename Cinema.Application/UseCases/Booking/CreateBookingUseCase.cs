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
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IHallRepository _hallRepository;
        private readonly IValidator<BookingDto> _validator;

        public CreateBookingUseCase(IBookingRepository bookingRepository,
            IValidator<BookingDto> validator,
            IUserRepository userRepository,
            ISessionRepository sessionRepository,
            IHallRepository hallRepository)
        {
            _bookingRepository = bookingRepository;
            _validator = validator;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _hallRepository = hallRepository;
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

            var user = await _userRepository.FindAsync(bookingDto.UserId, cancellationToken);
            if (user == null || user.IsDeleted)
            {
                return Error.BadRequest("The user does not exist");
            }

            var session = await _sessionRepository.FindAsync(bookingDto.SessionId,
                cancellationToken);
            if (session == null || session.IsDeleted)
            {
                return Error.BadRequest("The session does not exist");
            }

            var hall = await _hallRepository.FindAsync(session.HallId, cancellationToken);
            if (hall.CountSeats < bookingDto.SeatNumber)
            {
                return Error.BadRequest("This place does not exist");
            }

            var bookings = await _bookingRepository.FindBySessionAsync(bookingDto.SessionId,
                cancellationToken);
            if (bookings.Any(b => b.SeatNumber == bookingDto.SeatNumber))
            {
                return Error.BadRequest("This place is already occupied");
            }

            await _bookingRepository.AddAsync(bookingDto.SessionId,
                                             bookingDto.UserId,
                                             bookingDto.SeatNumber,
                                             cancellationToken);

            return Result.Success("Booking successful create");
        }
    }
}
