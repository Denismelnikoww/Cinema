namespace Cinema.Models
{
    public class MovieEntity 
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public float Rating { get; set; }
        public string Description { get; set; }
        public TimeSpan Time { get; set; }
        public  ICollection<SessionEntity> Sessions { get; set; } = [];
        public bool IsDeleted { get; set; }
    }
}
