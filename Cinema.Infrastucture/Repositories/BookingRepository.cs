using Cinema.Infrastucture.Infrastructure;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;
        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _context.Bookings
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.IsDeleted, false),
                cancellationToken);
        }

        public async Task SuperDeleteAsync(int id, CancellationToken cancellationToken)
        {
            var delete = await _context.Bookings
                .FindAsync(id, cancellationToken);

            _context.Bookings.Remove(delete);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<BookingEntity?> FindAdync(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted, cancellationToken);
        }

        public async Task<List<BookingEntity>> FindBySessionAsync(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(x => x.SessionId == id && !x.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<BookingEntity>> FindByUserAsync(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(x => x.UserId == id && !x.IsDeleted)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(int sessionId,
                              int userId,
                              int seatNumber,
                              CancellationToken cancellationToken)
        {
            var bookingEntity = new BookingEntity
            {
                SessionId = sessionId,
                UserId = userId,
                SeatNumber = seatNumber
            };


            await _context.Bookings.AddAsync(bookingEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
