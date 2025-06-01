using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Cinema.Models;
using System.Reflection;

namespace Cinema.Infrastructure
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql(_configuration.GetConnectionString("Database"))
                .UseLoggerFactory(CreateLoggerFactory())
                .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly());
        }

        public ILoggerFactory CreateLoggerFactory()
        {
            return LoggerFactory.Create(builder => { builder.AddConsole(); });
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
        public DbSet<HallEntity> Halls { get; set; }
        public DbSet<SessionEntity> Sessions { get; set; }
    }
}
