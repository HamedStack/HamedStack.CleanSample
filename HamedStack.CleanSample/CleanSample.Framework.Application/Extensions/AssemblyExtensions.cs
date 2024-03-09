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

    internal static IEnumerable<Type> FindImplementationsOfInterface(Type interfaceType)
    {
        var appAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        var implementations = new HashSet<Type>();

        foreach (var assembly in appAssemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract) continue;

                var interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface == interfaceType ||
                        (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == interfaceType))
                    {
                        implementations.Add(type);
                        break;
                    }
                }
            }
        }

        return implementations;
    }
    internal static IEnumerable<Assembly> FindAssembliesOfInterface(Type interfaceType)
    {
        var appAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        var assemblies = new HashSet<Assembly>();

        foreach (var assembly in appAssemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract) continue;

                var interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface == interfaceType ||
                        (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == interfaceType))
                    {
                        assemblies.Add(assembly);
                        break;
                    }
                }
            }
        }

        return assemblies;
    }
}