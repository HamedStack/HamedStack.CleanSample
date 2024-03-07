using System.Collections.Concurrent;

namespace CleanSample.Framework.Infrastructure.Extensions;

internal static class ObjectExtensions
{
    private static readonly ConcurrentDictionary<string, object> CachedData = new();

    public static T Cache<T>(this object obj, string cacheKey)
    {
        return (T)CachedData.GetOrAdd(cacheKey, _ => (T)obj);
    }

    public static T Cache<T>(this T obj, string cacheKey)
    {
        return (T)CachedData.GetOrAdd(cacheKey, _ => obj!);
    }
}