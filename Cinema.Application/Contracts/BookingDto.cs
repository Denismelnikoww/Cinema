namespace Cinema.Contracts
{
    public record BookingDto
    {
        public int UserId { get; set; }
        public int SessionId { get; set; }
        public int SeatNumber { get; set; }
    }
}
