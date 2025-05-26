using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Pet.Models
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
