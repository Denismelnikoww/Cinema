using Microsoft.EntityFrameworkCore;
using Pet.Models;

namespace Pet.Repositories
{
    public class HallRepository
    {
        private readonly AppDbContext _context;

        public HallRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<HallEntity>> GetAll()
        {
            return await _context.Halls
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<HallEntity>> GetWorking(bool isWorking = true)
        {
            var query = _context.Halls.AsNoTracking();

            query.Where(x => x.IsWorking == isWorking);

            return await query.ToListAsync();
        }

        public async Task<HallEntity>? GetById(int id)
        {
            return await _context.Halls.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Add(int countSeats, string name, bool isWorking)
        {
            var hall = new HallEntity
            {
                CountSeats = countSeats,
                Name = name,
                IsWorking = isWorking
            };

            _context.Halls.Add(hall);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            HallEntity? hall = null;
            hall = await _context.Halls.FindAsync(id);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteByName(string name)
        {
            HallEntity? hall = null;
            hall = await _context.Halls.FirstOrDefaultAsync(x => x.Name.Contains(name));
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateById(int id, int countSeats, string name, bool isWorking)
        {
            await _context.Halls
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.CountSeats, countSeats)
                    .SetProperty(h => h.Name, name)
                    .SetProperty(h => h.IsWorking, isWorking));

        }
        
        public async Task UpdateByName(string nam, int countSeats, string name, bool isWorking)
        {
            await _context.Halls
                .Where(x => x.Name.Contains(nam))
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.CountSeats, countSeats)
                    .SetProperty(h => h.Name, name)
                    .SetProperty(h => h.IsWorking, isWorking));
        }
    }
}
