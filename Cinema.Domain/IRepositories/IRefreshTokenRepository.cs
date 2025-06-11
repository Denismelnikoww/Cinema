using Cinema.Domain.Models;

namespace Cinema.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task Add(int userId, DateTime created, DateTime expiry, string jti, string token, CancellationToken cancellationToken);
        Task<RefreshTokenEntity?> FindAsync(string jti, CancellationToken cancellationToken);
        Task UseToken(string jti, CancellationToken cancellationToken);
    }
}