using Microsoft.AspNetCore.Identity;

namespace Cinema.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int RoleId { get; set; }
        public RoleEntity Role { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public ICollection<BookingEntity> Bookings { get; set; } = [];
    }
}
