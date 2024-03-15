using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace CleanSample.WebApi.IntegrationTests
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Remove<T>(this IServiceCollection services)
        {
            if (services.IsReadOnly)
            {
                throw new ReadOnlyException($"{nameof(services)} is read only");
            }

            var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(T));
            if (serviceDescriptor != null) services.Remove(serviceDescriptor);

            return services;
        }

        public static IServiceCollection RemoveAll(this IServiceCollection services, params Type[] types)
        {
            if (services.IsReadOnly)
            {
                throw new ReadOnlyException($"{nameof(services)} is read only");
            }

            foreach (var type in types)
            {
                var serviceDescriptor = services.FirstOrDefault(descriptor => descriptor.ServiceType == type);
                if (serviceDescriptor != null) services.Remove(serviceDescriptor);
            }

            return services;
        }
    }
}
