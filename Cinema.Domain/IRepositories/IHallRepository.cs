using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IHallRepository
    {
        Task AddAsync(string name, int countSeats, bool isWorking, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task<List<HallEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<HallEntity?> FindAsync(int id, CancellationToken cancellationToken);
        Task<List<HallEntity>> GetWorkingAsync(CancellationToken cancellationToken, bool isWorking = true);
        Task SuperDeleteAsync(int id, CancellationToken cancellationToken);
        Task UpdateAsync(int id, int countSeats, string name, bool isWorking, CancellationToken cancellationToken);
    }
}