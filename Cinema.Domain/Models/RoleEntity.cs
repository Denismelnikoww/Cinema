using Cinema.Enums;

namespace Cinema.Models
{
    public class RoleEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; } = [];
        public ICollection<PermissionEntity> Permissions { get; set; } = [];


        public static implicit operator RoleEntity(Role role)
        {
            return new RoleEntity
            {
                Name = role.ToString(),
                Id = (int)role
            };
        }
    }
}
