// ReSharper disable UnusedMember.Global

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CleanSample.Framework.Infrastructure.Identity.DynamicPermissions;
public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
{
    private readonly IMemoryCache _cache;
    private readonly AuthorizationCacheOptions _cacheOptions;
    private readonly ILogger<AuthorizationPolicyProvider> _logger;

    public AuthorizationPolicyProvider(
        IOptions<AuthorizationOptions> options,
        IMemoryCache cache,
        IOptions<AuthorizationCacheOptions> cacheOptions,
        ILogger<AuthorizationPolicyProvider> logger) : base(options)
    {
        _cache = cache;
        _cacheOptions = cacheOptions.Value;
        _logger = logger;
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {

        if (_cacheOptions.DisableCache)
        {
            return await GetAuthorizationPolicy(policyName);
        }
        return await _cache.GetOrCreateAsync(policyName, async entry =>
        {
            entry.SetAbsoluteExpiration(_cacheOptions.DefaultCacheDuration);
            return await GetAuthorizationPolicy(policyName);
        }).ConfigureAwait(false);

        async Task<AuthorizationPolicy> GetAuthorizationPolicy(string pn)
        {
            var policy = await base.GetPolicyAsync(pn);

            if (policy != null) return policy;

            policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(pn))
                .Build();
            _logger.LogInformation($"Generated policy {pn}");

            return policy;
        }
    }
}