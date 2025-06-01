using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cinema.Contracts;
using Cinema.Models;
using Cinema.Infrastructure;
using Cinema.Interfaces;

namespace Cinema.Repositories
{
    public class MovieRepository : IMovieRepository
    {

        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task<List<MovieEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Movies
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        public async Task<MovieEntity?> GetById(int id, CancellationToken cancellationToken)
        {
            return await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<MovieEntity>> GetFilterTitle(string title, CancellationToken cancellationToken)
        {
            var query = _context.Movies.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(x => x.Title.ToLower().Contains(title));
            }

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<List<MovieEntity>> GetByPage(int page, int pageSize,
            CancellationToken cancellationToken)
        {
            return await _context.Movies
                                    .AsNoTracking()
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);
        }

        public async Task Add([FromBody] MovieDto movieDto, CancellationToken cancellationToken)
        {
            var movie = Mapper.MapToEntity(movieDto);

            await _context.AddAsync(movie, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
