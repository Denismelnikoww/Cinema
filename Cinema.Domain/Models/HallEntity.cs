namespace Cinema.Models
{
    public class HallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public bool IsWorking { get; set; }
        public required int CountSeats { get; set; }
        public ICollection<SessionEntity> Sessions { get; set; } = [];
        public bool IsDeleted { get; set; }
    }
}
