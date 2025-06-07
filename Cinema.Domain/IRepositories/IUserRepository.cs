using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken);
        Task<UserEntity?> GetById(int id, CancellationToken cancellationToken);
        Task DeleteById(int id, CancellationToken cancellationToken);
        Task SuperDeleteById(int id, CancellationToken cancellationToken);
        Task Add(string userName, string passwordHash, string email, CancellationToken cancellationToken);
    }
}