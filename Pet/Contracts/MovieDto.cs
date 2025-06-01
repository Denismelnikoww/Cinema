namespace Cinema.Contracts
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Author { get; set; }
        public float Rating { get; set; }
    }
}
