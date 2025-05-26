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

        public async Task<List<MovieEntity>> GetAll()
        {
            return await _context.Movies
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<MovieEntity?> GetById(int id)
        {
            return await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<MovieEntity>> GetFilterTitle(string title)
        {
            var query = _context.Movies.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(x => x.Title.ToLower().Contains(title));
            }

            return await query.ToListAsync();
        }

        public async Task<List<MovieEntity>> GetByPage(int page, int pageSize)
        {
            return await _context.Movies
                                    .AsNoTracking()
                                    .Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(); 
        }

        public async Task Add(string title, string author, float rating,
            string description, TimeSpan time)
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
            await _context.SaveChangesAsync();
        }

    }
}
