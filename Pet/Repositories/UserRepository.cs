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

        public async Task<UserEntity?> GetById(int id)
        {
            return await _context.Users
                .FindAsync(id);
        }

        public async Task<UserEntity?> GetByEmail(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        

        public async Task Add(string userName, string passwordHash, string email, bool isAdmin)
        {
            var alreadyExists = await GetByEmail(email);

            if (alreadyExists != null) {
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
            await _context.SaveChangesAsync();
        }
    }
}
