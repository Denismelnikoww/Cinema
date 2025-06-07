using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface ISessionRepository
    {
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task SuperDeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<SessionEntity>> GetAllByHallAsync(int hallId, CancellationToken cancellationToken);
        Task<List<SessionEntity>> GetAllByMovieAsync(int movieId, CancellationToken cancellationToken);
        Task<SessionEntity?> FindAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(int movieId, DateTime dateTime, int hallId, decimal price, TimeSpan duration, CancellationToken cancellationToken);
    }
}