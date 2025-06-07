using Cinema.Application.UseCases.Booking;
using Cinema.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using ResultSharp.Core;
using ResultSharp.Errors;
using System.Threading;

namespace Cinema.API.Controllers
{
    public class DeleteBookingByIdUseCase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly GetBookingByUserIdUseCase _getBookingByUser;
        public DeleteBookingByIdUseCase(IBookingRepository bookingRepository,
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

            await _bookingRepository.DeleteAsync(id, cancellationToken);

            return Result.Success("The booking has been deleted");
        }
    }
}
