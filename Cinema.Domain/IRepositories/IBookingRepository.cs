using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(int sessionId, int userId, int seatNumber, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<BookingEntity?> FindAdync(int id, CancellationToken cancellationToken);
        Task<List<BookingEntity>> FindBySessionAsync(int id, CancellationToken cancellationToken);
        Task<List<BookingEntity>> FindByUserAsync(int id, CancellationToken cancellationToken);
        Task SuperDeleteAsync(int id, CancellationToken cancellationToken);
    }
}