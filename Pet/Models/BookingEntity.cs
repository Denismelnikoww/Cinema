namespace Pet.Models
{
    public class BookingEntity
    {
        public int Id { get; set; }
        public required int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public required int SessionId { get; set; }
        public virtual SessionEntity Session { get; set; }
        public required int SeatNumber { get; set; }
    }
}
