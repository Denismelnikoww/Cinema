namespace Pet.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsAdmin { get; set; } = false;
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public virtual ICollection<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
    }
}
