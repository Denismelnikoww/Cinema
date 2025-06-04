using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IHallRepository
    {
        Task Add(string name, int countSeats, bool isWorking, CancellationToken cancellationToken);
        Task DeleteById(int id, CancellationToken cancellationToken);
        Task<List<HallEntity>> GetAll(CancellationToken cancellationToken);
        Task<HallEntity?> GetById(int id, CancellationToken cancellationToken);
        Task<List<HallEntity>> GetWorking(CancellationToken cancellationToken, bool isWorking = true);
        Task SuperDeleteById(int id, CancellationToken cancellationToken);
        Task UpdateById(int id, int countSeats, string name, bool isWorking, CancellationToken cancellationToken);
    }
}