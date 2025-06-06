using Cinema.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ResultSharp.Core;
using ResultSharp.Errors;

namespace Cinema.Application.UseCases.Booking
{
    public class SuperDeleteBookingById
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly GetBookingByUserIdUseCase _getBookingByUser;

        public SuperDeleteBookingById(IBookingRepository bookingRepository,
             GetBookingByUserIdUseCase getBookingByUser)
        {
            _bookingRepository = bookingRepository;
            _getBookingByUser = getBookingByUser;
        }

        public async Task<Result<string>> ExecuteAsync(int id,
            CancellationToken cancellationToken)
        {
            var delete = await _getBookingByUser.ExecuteAsync(id, cancellationToken);

            if (delete == null)
            {
                return Error.BadRequest("Incorrect ID");
            }

            await _bookingRepository.SuperDeleteById(id, cancellationToken);

            return Result.Success("The booking has been deleted");
        }
    }
}
