namespace Pet.Models
{
    public class SessionEntity
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public HallEntity Hall { get; set; }
        public int MovieId { get; set; }
        public MovieEntity Movie { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
    }
}
