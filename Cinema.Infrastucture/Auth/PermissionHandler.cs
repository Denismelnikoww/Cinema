using Cinema.Enums;
using Cinema.Infrastucture.Repositories;
using Cinema.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Infrastucture.Auth
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public PermissionHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            var cancellationToken = context.Resource is HttpContext httpContext
               ? httpContext.RequestAborted
               : CancellationToken.None;

            var userIdClaim = context.User.FindFirst("userId")?.Value;

            int.TryParse(userIdClaim, out var userId);

            var user = await _userRepository.GetById(userId, cancellationToken);

            if (user == null)
            {
                context.Fail();
                return;
            }

            var permissionsId = await _roleRepository.GetPermission(user.RoleId,
                cancellationToken);

            if (requirement.Permissions.All(p =>
                permissionsId.Contains((int)p)))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
