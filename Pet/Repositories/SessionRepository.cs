using Microsoft.EntityFrameworkCore;
using Pet.Contracts;
using Pet.Models;
using System;

namespace Pet.Repositories
{
    public class SessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SessionEntity>> GetAllByHall(int hallId,
            CancellationToken cancellationToken)
        {
            return await _context.Sessions
                 .Where(session => session.HallId == hallId)
                 .ToListAsync(cancellationToken);
        }

        public async Task<List<SessionEntity>> GetAllByMovie(int movieId,
            CancellationToken cancellationToken)
        {
            return await _context.Sessions
                .Where(session => session.MovieId == movieId)
                .ToListAsync(cancellationToken);
        }

        public async Task<SessionEntity?> GetById(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Sessions.FindAsync(id, cancellationToken);
        }

        public async Task Create(SessionDto session, CancellationToken cancellationToken)
        {
            SessionEntity sessionEntity = new SessionEntity
            {
                MovieId = session.MovieId,
                DateTime = session.DateTime,
                HallId = session.HallId,
                Price = session.Price,
                Time = session.Time,
            };

            await _context.Sessions.AddAsync(sessionEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
