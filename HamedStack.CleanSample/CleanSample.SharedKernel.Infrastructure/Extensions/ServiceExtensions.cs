// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using CleanSample.SharedKernel.Domain.Repositories;
using CleanSample.SharedKernel.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanSample.SharedKernel.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFrameworkDbContext<T>(this IServiceCollection services)
            where T : DbContextBase
        {
            services.AddSingleton(TimeProvider.System);
            services.AddScoped<T>();
            services.AddScoped<DbContextBase>(provider => provider.GetRequiredService<T>());
            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<T>());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            return services;
        }

    }
}
