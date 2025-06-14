using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using System.Reflection;
using Cinema.Domain.Models;

namespace Cinema.Infrastucture.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<HallEntity> Halls { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    }
}
