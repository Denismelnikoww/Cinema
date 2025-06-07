using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserEntity?> FindAsync(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        Task SuperDeleteAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(string userName, string passwordHash, string email, CancellationToken cancellationToken);
    }
}