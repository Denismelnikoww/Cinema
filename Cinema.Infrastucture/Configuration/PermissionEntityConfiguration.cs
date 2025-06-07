using Cinema.Enums;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Configuration
{
    public class PermissionEntityConfiguration : IEntityTypeConfiguration<PermissionEntity>
    {
        public void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasData(Enum.GetValues<Permission>()
                .Select(permission => new PermissionEntity
                {
                    Id = (int)permission,
                    Name = permission.ToString()
                })
            );
        }
    }
}
