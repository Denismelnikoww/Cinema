using Cinema.Contracts;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.Interfaces
{
    public interface IHallRepository
    {
        Task Add([FromBody] HallDto hallDto, CancellationToken cancellationToken);
        Task DeleteById(int id, CancellationToken cancellationToken);
        Task<List<HallEntity>> GetAll(CancellationToken cancellationToken);
        Task<HallEntity?> GetById(int id, CancellationToken cancellationToken);
        Task<List<HallEntity>> GetWorking(CancellationToken cancellationToken, bool isWorking = true);
        Task SuperDeleteById(int id, CancellationToken cancellationToken);
        Task UpdateById(int id, int countSeats, string name, bool isWorking, CancellationToken cancellationToken);
    }
}