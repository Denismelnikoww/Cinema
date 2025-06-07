using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Cinema.Interfaces;
using Cinema.Infrastucture.Infrastructure;

namespace Cinema.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Bookings
                 .Where(x => x.Id == id)
                 .ExecuteUpdateAsync(s => s
                 .SetProperty(m => m.IsDeleted, true),
                 cancellationToken);
        }

        public async Task SuperDeleteAsync(int id, CancellationToken cancellationToken)
        {
            var delete = await _context.Bookings
                .FindAsync(id, cancellationToken);

            _context.Bookings.Remove(delete);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<MovieEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Movies
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<MovieEntity?> FindAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<MovieEntity>> GetFilterTitleAsync(string title, CancellationToken cancellationToken)
        {
            var query = _context.Movies.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(x => x.Title.ToLower().Contains(title));
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<MovieEntity>> GetByPageAsync(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            return await _context.Movies
                                    .AsNoTracking()
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(string author,
                              string description,
                              float rating,
                              TimeSpan duration,
                              string title,
                              CancellationToken cancellationToken)
        {

            var movie = new MovieEntity
            {
                Author = author,
                Description = description,
                Rating = rating,
                Duration = duration,
                Title = title,
            };

            await _context.AddAsync(movie, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
