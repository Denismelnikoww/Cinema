using Cinema.Models;

namespace Cinema.Interfaces
{
    public interface IUserRepository
    {
        Task Add(string userName, string passwordHash, string email, bool isAdmin, CancellationToken cancellationToken);
        Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken);
        Task<UserEntity?> GetById(int id, CancellationToken cancellationToken);
    }
}