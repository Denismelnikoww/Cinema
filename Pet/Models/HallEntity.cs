namespace Pet.Models
{
    public class HallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public bool IsWorking { get; set; }
        public required int CountSeats { get; set; }
        public virtual ICollection<SessionEntity> Sessions { get; set; } = new List<SessionEntity>();
    }
}
