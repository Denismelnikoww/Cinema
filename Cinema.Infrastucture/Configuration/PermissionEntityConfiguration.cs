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

            builder.HasData(Permission.Create,
                            Permission.Delete,
                            Permission.Read,
                            Permission.SuperCreate,
                            Permission.SuperRead,
                            Permission.SuperDelete);
        }
    }
}
