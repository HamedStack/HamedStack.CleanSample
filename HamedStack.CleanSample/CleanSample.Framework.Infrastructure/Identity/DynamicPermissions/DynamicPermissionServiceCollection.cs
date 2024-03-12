// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.Framework.Infrastructure.Identity.DynamicPermissions;

public static class DynamicPermissionServiceCollection
{
    public static IServiceCollection AddDynamicPermission(
        this IServiceCollection services,
        Action<AuthorizationCacheOptions>? configureOptions = null)
    {
        services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();
        services.AddMemoryCache();

        if (configureOptions != null)
        {
            services.Configure(configureOptions);
        }
        else
        {
            services.Configure<AuthorizationCacheOptions>(options =>
                options.DefaultCacheDuration = TimeSpan.FromMinutes(60));
        }
        return services;
    }

}
