using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using Cinema.Interfaces;
using Cinema.Infrastucture.Infrastructure;

namespace Cinema.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Sessions
                 .Where(x => x.Id == id)
                 .ExecuteUpdateAsync(s => s
                 .SetProperty(m => m.IsDeleted, true),
                 cancellationToken);
        }

        public async Task SuperDeleteAsync(int id, CancellationToken cancellationToken)
        {
            var delete = await _context.Sessions
                .FindAsync(id, cancellationToken);

            _context.Sessions.Remove(delete);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<SessionEntity>> GetAllByHallAsync(int hallId,
            CancellationToken cancellationToken)
        {
            return await _context.Sessions
                 .Where(session => session.HallId == hallId)
                 .ToListAsync(cancellationToken);
        }

        public async Task<List<SessionEntity>> GetAllByMovieAsync(int movieId,
            CancellationToken cancellationToken)
        {
            return await _context.Sessions
                .Where(session => session.MovieId == movieId)
                .ToListAsync(cancellationToken);
        }

        public async Task<SessionEntity?> FindAsync(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Sessions.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(int movieId,
                                 DateTime dateTime,
                                 int hallId,
                                 decimal price,
                                 TimeSpan duration,
                                 CancellationToken cancellationToken)
        {

            SessionEntity sessionEntity = new SessionEntity
            {
                MovieId = movieId,
                DateTime = dateTime,
                HallId = hallId,
                Price = price,
                Duration = duration,
            };

            await _context.Sessions.AddAsync(sessionEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
