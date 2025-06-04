using Cinema.Enums;

namespace Cinema.Models
{
    public class PermissionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RoleEntity> Roles { get; set; } = [];

        public static implicit operator PermissionEntity(Permission permission) {
            return new PermissionEntity
            {
                Id = (int)permission,
                Name = permission.ToString(),
            };
        }
    }
}
