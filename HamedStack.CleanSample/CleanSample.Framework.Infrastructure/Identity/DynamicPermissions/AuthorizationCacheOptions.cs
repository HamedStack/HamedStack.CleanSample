namespace CleanSample.Framework.Infrastructure.Identity.DynamicPermissions;

public class AuthorizationCacheOptions
{
    public TimeSpan DefaultCacheDuration { get; set; }
    public bool DisableCache { get; set; }
}