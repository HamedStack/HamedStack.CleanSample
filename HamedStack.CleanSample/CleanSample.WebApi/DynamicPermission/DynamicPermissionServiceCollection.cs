// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;

namespace CleanSample.WebApi.DynamicPermission;

public static class DynamicPermissionServiceCollection
{
    public static IServiceCollection AddDynamicPermission(this IServiceCollection services, AuthorizationCacheOptions? cacheOptions = null)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddMemoryCache();
        services.Configure<AuthorizationCacheOptions>(options => options.DefaultCacheDuration = cacheOptions?.DefaultCacheDuration ?? TimeSpan.FromMinutes(60));
        return services;
    }
}
