using Cinema.Contracts;
using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface ISessionRepository
    {
        Task DeleteById(int id, CancellationToken cancellationToken);
        Task SuperDeleteById(int id, CancellationToken cancellationToken);
        Task Create(SessionDto session, CancellationToken cancellationToken);
        Task<List<SessionEntity>> GetAllByHall(int hallId, CancellationToken cancellationToken);
        Task<List<SessionEntity>> GetAllByMovie(int movieId, CancellationToken cancellationToken);
        Task<SessionEntity?> GetById(int id, CancellationToken cancellationToken);
    }
}