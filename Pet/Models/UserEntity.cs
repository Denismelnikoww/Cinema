namespace Pet.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsAdmin { get; set; } = false;
        public required string Login { get; set; }
        public required string Password { get; set; }
        public ICollection<BookingEntity> Bookings { get; set; } = new List<BookingEntity>();
    }
}
