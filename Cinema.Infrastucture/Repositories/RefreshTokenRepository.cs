using Cinema.Domain.Models;
using Cinema.Infrastucture.Infrastructure;
using Cinema.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Cinema.Infrastucture.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshTokenEntity?> FindAsync(string jti,
            CancellationToken cancellationToken)
        {
            return await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(x => !x.IsUsed && x.Jti == jti,
                cancellationToken);
        }

        public async Task Add(int userId,
                              DateTime created,
                              DateTime expiry,
                              string jti,
                              string token,
                              CancellationToken cancellationToken)
        {
            var refreshToken = new RefreshTokenEntity
            {
                UserId = userId,
                IsUsed = false,
                CreatedAt = created,
                ExpiryAt = expiry,
                Jti = jti,
                Token = token
            };

            await _context.AddAsync(refreshToken, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UseToken(string jti, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens
                 .Where(x => x.Jti == jti)
                 .ExecuteUpdateAsync(setters => setters
                     .SetProperty(x => x.IsUsed, true),
                     cancellationToken);
        }
    }
}
