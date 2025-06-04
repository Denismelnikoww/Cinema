namespace Cinema.Models
{
    public class SessionEntity
    {
        public int Id { get; set; }
        public int HallId { get; set; }
        public virtual HallEntity Hall { get; set; }
        public int MovieId { get; set; }
        public virtual MovieEntity Movie { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
