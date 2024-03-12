using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CleanSample.Framework.Infrastructure.Identity.Permissions;

public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    public PermissionAuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options) : base(options) { }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName)
    {
        if (!policyName.StartsWith(PermissionAuthorizeAttribute.PolicyPrefix, StringComparison.OrdinalIgnoreCase))
            return await base.GetPolicyAsync(policyName);

        PermissionOperator @operator = PermissionAuthorizeAttribute.GetOperatorFromPolicy(policyName);

        string[] permissions = PermissionAuthorizeAttribute.GetPermissionsFromPolicy(policyName);

        var requirement = new PermissionRequirement(@operator, permissions);

        return new AuthorizationPolicyBuilder()
            .AddRequirements(requirement).Build();
    }
}