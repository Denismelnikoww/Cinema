using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pet.Contracts;
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

        public async Task<List<HallEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Halls
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<List<HallEntity>> GetWorking(CancellationToken cancellationToken,
                                                       bool isWorking = true)
        {
            var query = _context.Halls.AsNoTracking();

            query.Where(x => x.IsWorking == isWorking);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<HallEntity?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Halls.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task Add([FromBody] HallDto hallDto,
            CancellationToken cancellationToken)
        {
            var hall = Mapper.MapToEntity(hallDto);

            _context.Halls.Add(hall);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteById(int id, CancellationToken cancellationToken)
        {
            HallEntity? hall = null;
            hall = await _context.Halls.FindAsync(id, cancellationToken);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteByName(string name, CancellationToken cancellationToken)
        {
            HallEntity? hall = null;
            hall = await _context.Halls.FirstOrDefaultAsync(x => x.Name.Contains(name), cancellationToken);
            _context.Halls.Remove(hall);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateById(int id, int countSeats, string name,
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

        public async Task UpdateByName(string nam, int countSeats, string name,
            bool isWorking, CancellationToken cancellationToken)
        {
            await _context.Halls
                .Where(x => x.Name.Contains(nam))
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(h => h.CountSeats, countSeats)
                    .SetProperty(h => h.Name, name)
                    .SetProperty(h => h.IsWorking, isWorking),
                    cancellationToken);
        }
    }
}
