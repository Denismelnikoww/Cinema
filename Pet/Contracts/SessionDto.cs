using Pet.Models;

namespace Pet.Contracts
{
    public record SessionDto
    {
        public int HallId { get; set; }
        public int MovieId { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
    }
}
