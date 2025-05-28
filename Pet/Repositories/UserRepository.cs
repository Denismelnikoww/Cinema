using Microsoft.EntityFrameworkCore;
using Pet.Models;

namespace Pet.Repositories
{
    public class UserRepository
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
                IsAdmin = isAdmin,
                Name = userName,
                PasswordHash = passwordHash
            };

            await _context.Users.AddAsync(User);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
