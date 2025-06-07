using Cinema.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.API.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RequirementsPermissionAttribute : AuthorizeAttribute
    {
        public RequirementsPermissionAttribute(params Permission[] permissions) {
            Policy = string.Join(" ", permissions.Select(p => p.ToString()));
        }
    }
}
