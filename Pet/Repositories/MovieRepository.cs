using Microsoft.EntityFrameworkCore;
using Pet.Models;

namespace Pet.Repositories
{
    public class MovieRepository
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

        public async Task Add(string title, string author, float rating,
            string description, TimeSpan time, CancellationToken cancellationToken)
        {
            var movie = new MovieEntity()
            {
                Author = author,
                Title = title,
                Rating = rating,
                Description = description,
                Time = time,
            };

            await _context.AddAsync(movie);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
