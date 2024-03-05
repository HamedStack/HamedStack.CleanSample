using System.Reflection;

namespace CleanSample.Framework.Application.Extensions;

internal static class AssemblyExtensions
{
    internal static bool Contains(this Assembly assembly, params Type[] types)
    {
        var assemblyTypes = assembly.GetTypes().SelectMany(t => new[] { t }.Concat(t.GetNestedTypes()));
        return types.Any(type => assemblyTypes.Contains(type));
    }

    internal static IEnumerable<Assembly> AppDomainContains(params Type[] types)
    {
        return AppDomain.CurrentDomain.GetAssemblies().Where(a => a.Contains(types));
    }
}