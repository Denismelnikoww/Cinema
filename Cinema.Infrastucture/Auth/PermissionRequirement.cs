using Cinema.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Infrastucture.Auth
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public Permission[] Permissions {  get;  }

        public PermissionRequirement(Permission[] permissions)
        {
            Permissions = permissions;
        }
    }
}
