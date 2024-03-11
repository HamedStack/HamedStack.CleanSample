// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;

namespace CleanSample.WebApi.DynamicPermission;

public class PermissionRequirement : IAuthorizationRequirement
{
    public PermissionRequirement(string permission)
    {
        Permission = !string.IsNullOrEmpty(permission) ? permission : throw new ArgumentNullException(nameof(permission));
    }
    public string Permission { get; }
}