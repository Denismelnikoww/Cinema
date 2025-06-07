using Cinema.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Cinema.Infrastucture.Auth
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly IAuthorizationPolicyProvider _fallback;

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _fallback = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return _fallback.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return _fallback.GetFallbackPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (string.IsNullOrEmpty(policyName))
            {
                return _fallback.GetPolicyAsync(policyName);
            }

            var permissions = policyName.Split(',')
                .Select(p => Enum.Parse<Permission>(p))
                .ToArray();

            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new PermissionRequirement(permissions));

            return Task.FromResult<AuthorizationPolicy?>(policy.Build());
        }
    }
}
