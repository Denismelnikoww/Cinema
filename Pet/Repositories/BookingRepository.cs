using Cinema.Contracts;
using Cinema.Infrastructure;
using Cinema.Interfaces;
using Cinema.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task DeleteById(int id, CancellationToken cancellationToken)
        {
            await _context.Bookings
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(s => s
                .SetProperty(b => b.IsDeleted, false),
                cancellationToken);
        }

        public async Task SuperDeleteById(int id, CancellationToken cancellationToken)
        {
            var delete = await _context.Bookings
                .FindAsync(id, cancellationToken);

             _context.Bookings.Remove(delete); 
            
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<BookingEntity?> GetById(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<BookingEntity>> GetBySessionId(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(x => x.SessionId == id)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<BookingEntity>> GetByUserId(int id,
            CancellationToken cancellationToken)
        {
            return await _context.Bookings
                .AsNoTracking()
                .Where(x => x.UserId == id)
                .ToListAsync(cancellationToken);
        }

        public async Task Add([FromBody] BookingDto bookingDto,
            CancellationToken cancellationToken)
        {
            var bookingEntity = Mapper.MapToEntity(bookingDto);

            await _context.Bookings.AddAsync(bookingEntity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
