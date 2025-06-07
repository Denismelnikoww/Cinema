using Cinema.Enums;
using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Cinema.Configuration
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        private readonly IConfiguration _configuration;

        public RoleEntityConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(r => r.Permissions)
                .WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermissions",
                    r => r.HasOne<PermissionEntity>().WithMany().HasForeignKey("PermissionId"),
                    p => p.HasOne<RoleEntity>().WithMany().HasForeignKey("RoleId"),
                    joinEntity =>
                    {
                        var rolePermissions = _configuration.GetSection("RolePermissions")
                            .Get<Dictionary<string, List<string>>>();

                        var data = new List<Dictionary<string, object>>();

                        foreach (var role in rolePermissions)
                        {
                            var roleName = role.Key;
                            var permissions = role.Value;

                            foreach (var permissionName in permissions)
                            {
                                data.Add(new Dictionary<string, object>
                                {
                                    { "RoleId", (int)Enum.Parse<Role>(roleName) },
                                    { "PermissionId", (int)Enum.Parse<Permission>(permissionName) }
                                });
                            }
                        }

                        joinEntity.HasData(data);
                    }
                );


            builder.HasData(
                Enum.GetValues<Role>().Select(role => new RoleEntity
                {
                    Id = (int)role,
                    Name = role.ToString()
                })
            );
        }
    }
}