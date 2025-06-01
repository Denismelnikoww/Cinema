namespace Cinema.Models
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
