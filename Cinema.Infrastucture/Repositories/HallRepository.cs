using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Cinema.Interfaces;
using Cinema.Infrastucture.Infrastructure;

namespace Cinema.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly AppDbContext _context;

        public HallRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public async Task DeleteAsync(int id, CancellationToken
            cancellationToken)
        {
            await _context.Halls
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.IsDeleted, true),
                    cancellationToken);

        }

        public async Task<List<HallEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Halls
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<List<HallEntity>> GetWorkingAsync(CancellationToken cancellationToken,
                                                       bool isWorking = true)
        {
            var query = _context.Halls.AsNoTracking();

            query.Where(x => x.IsWorking == isWorking);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<HallEntity?> FindAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Halls.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(string name,
                              int countSeats,
                              bool isWorking,
                              CancellationToken cancellationToken)
        {
            var hall = new HallEntity
            {
                Name = name,
                CountSeats = countSeats,
                IsWorking = isWorking,
            };

            await _context.Halls.AddAsync(hall, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task SuperDeleteAsync(int id, CancellationToken cancellationToken)
        {
            HallEntity? hall = null;
            hall = await _context.Halls.FindAsync(id, cancellationToken);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, int countSeats, string name,
            bool isWorking, CancellationToken cancellationToken)
        {
            await _context.Halls
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.CountSeats, countSeats)
                    .SetProperty(h => h.Name, name)
                    .SetProperty(h => h.IsWorking, isWorking),
                    cancellationToken);

        }
    }
}
