using Cinema.Infrastucture.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastucture.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<int>> GetPermissionAsync(int roleId,
            CancellationToken cancellationToken)
        {
            return await _context.Roles
               .AsNoTracking()
               .Where(r => r.Id == roleId)
               .SelectMany(r => r.Permissions)
               .Select(p => p.Id)
               .ToListAsync(cancellationToken);
        }
    }
}
