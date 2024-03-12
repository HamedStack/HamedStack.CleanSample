// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace CleanSample.Framework.Infrastructure.Identity.DynamicPermissions;

public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ILogger<PermissionRequirementHandler> _logger;
    public const string ClaimType = "DynamicPermission";

    public PermissionRequirementHandler(ILogger<PermissionRequirementHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var permissions = context.User.Claims.Where(
            x => x.Type == ClaimType && x.Value == requirement.Permission);
        
        if (permissions.Any())
        {
            _logger.LogInformation($"Authorization succeeded for requirement {requirement.Permission}");
            context.Succeed(requirement);
            await Task.CompletedTask.ConfigureAwait(false);
        }
        else
        {
            _logger.LogWarning($"Authorization failed for requirement {requirement.Permission}");
        }
    }
}

