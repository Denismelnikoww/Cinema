namespace Cinema.Models
{
    public class BookingEntity
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public UserEntity User { get; set; }
        public required int SessionId { get; set; }
        public SessionEntity Session { get; set; }
        public required int SeatNumber { get; set; }
    }
}
