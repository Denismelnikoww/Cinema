using Microsoft.EntityFrameworkCore;

namespace Pet.Models
{
    public class AppDbContext : DbContext
    {      
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<HallEntity> Halls { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
    }
}
