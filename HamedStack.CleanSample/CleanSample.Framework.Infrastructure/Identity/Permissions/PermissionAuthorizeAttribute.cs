using Microsoft.AspNetCore.Authorization;

namespace CleanSample.Framework.Infrastructure.Identity.Permissions;

public class PermissionAuthorizeAttribute : AuthorizeAttribute
{
    internal const string PolicyPrefix = "PERMISSION_";
    private const string Separator = "_";

    public PermissionAuthorizeAttribute(
        PermissionOperator permissionOperator, params string[] permissions)
    {

        Policy = CreatePolicy(permissionOperator, permissions);
    }

    public PermissionAuthorizeAttribute(string permission)
    {
        Policy = CreatePolicy(permission);
    }

    public static PermissionOperator GetOperatorFromPolicy(string policyName)
    {
        var @operator = int.Parse(policyName.AsSpan(PolicyPrefix.Length, 1));
        return (PermissionOperator)@operator;
    }

    public static string[] GetPermissionsFromPolicy(string policyName)
    {
        return policyName.Substring(PolicyPrefix.Length + 2)
            .Split(new[] { Separator }, StringSplitOptions.RemoveEmptyEntries);
    }
    public static string CreatePolicy(string permission)
    {
        return CreatePolicy(PermissionOperator.And, permission);
    }
    public static string CreatePolicy(PermissionOperator permissionOperator, params string[] permissions)
    {
        return $"{PolicyPrefix}{(int)permissionOperator}{Separator}{string.Join(Separator, permissions)}";
    }
}