// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;

namespace CleanSample.Framework.Infrastructure.Identity.DynamicPermissions;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class PermissionAttribute : AuthorizeAttribute
{
    public PermissionAttribute() { }
    public PermissionAttribute(string policy) : base(policy) { }
    public PermissionAttribute(string policy, string? description) : base(policy)
    {
        Description = description;
    }

    public string? Description { get; set; }
}
