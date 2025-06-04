using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IBookingRepository
    {
        Task Add(int sessionId, int userId, int seatNumber, CancellationToken cancellationToken);
        Task DeleteById(int id, CancellationToken cancellationToken);
        Task<BookingEntity?> GetById(int id, CancellationToken cancellationToken);
        Task<List<BookingEntity>> GetBySessionId(int id, CancellationToken cancellationToken);
        Task<List<BookingEntity>> GetByUserId(int id, CancellationToken cancellationToken);
        Task SuperDeleteById(int id, CancellationToken cancellationToken);
    }
}