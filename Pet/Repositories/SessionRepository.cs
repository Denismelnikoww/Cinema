using Microsoft.EntityFrameworkCore;
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

        public async Task<List<SessionEntity>> GetAllByHall(HallEntity hall)
        {
            return await _context.Sessions
                 .Where(session => session.HallId == hall.Id)
                 .ToListAsync();
        }

        public async Task<List<SessionEntity>> GetAllByMovie(MovieEntity movie)
        {
            return await _context.Sessions
                .Where(session => session.MovieId == movie.Id)
                .ToListAsync();
        }

        public async Task<SessionEntity?> GetById(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }
    }
}
