using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Cinema.Interfaces;
using Cinema.Infrastucture.Infrastructure;
using Cinema.Enums;

namespace Cinema.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<UserEntity?> FindAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Users
                 .Where(x => x.Id == id)
                 .ExecuteUpdateAsync(s => s
                 .SetProperty(m => m.IsDeleted, true),
                 cancellationToken);
        }

        public async Task SuperDeleteAsync(int id, CancellationToken cancellationToken)
        {
            var delete = await _context.Users
                .FindAsync(id, cancellationToken);

            _context.Users.Remove(delete);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted, cancellationToken);
        }

        public async Task AddAsync(string userName,
                              string passwordHash,
                              string email,
                              CancellationToken cancellationToken)
        {
            var User = new UserEntity
            {
                Email = email,
                Name = userName,
                PasswordHash = passwordHash,
                RoleId = (int)Role.User,
            };

            await _context.Users.AddAsync(User, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> GetRoleId(int userId, CancellationToken cancellationToken)
        {
            var user = await FindAsync(userId, cancellationToken);

            return user != null ? user.RoleId : 0;
        }
    }
}
