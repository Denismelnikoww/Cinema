using Cinema.Contracts;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Interfaces
{
    public interface IBookingRepository
    {
        Task Add([FromBody] BookingDto bookingDto, CancellationToken cancellationToken);
        Task<BookingEntity?> GetById(int id, CancellationToken cancellationToken);
        Task<List<BookingEntity>> GetBySessionId(int id, CancellationToken cancellationToken);
        Task<List<BookingEntity>> GetByUserId(int id, CancellationToken cancellationToken);
    }
}