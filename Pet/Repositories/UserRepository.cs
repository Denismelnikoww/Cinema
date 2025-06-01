using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Cinema.Infrastructure;
using Cinema.Interfaces;

namespace Cinema.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<UserEntity?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FindAsync(id, cancellationToken);
        }

        public async Task<UserEntity?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }



        public async Task Add(string userName, string passwordHash, string email,
            bool isAdmin, CancellationToken cancellationToken)
        {
            var alreadyExists = await GetByEmail(email, cancellationToken);

            if (alreadyExists != null)
            {
                throw new BadHttpRequestException("This user is already registered");
            }

            var User = new UserEntity
            {
                Email = email,
                Name = userName,
                PasswordHash = passwordHash
            };

            await _context.Users.AddAsync(User, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
