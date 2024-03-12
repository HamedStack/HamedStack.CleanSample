// ReSharper disable UnusedMember.Global
// ReSharper disable IdentifierTypo

using System.Text;
using CleanSample.Domain.Identity;
using CleanSample.Framework.Domain.AggregateRoots;
using CleanSample.Framework.Domain.Repositories;
using CleanSample.Framework.Infrastructure.Outbox;
using CleanSample.Framework.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CleanSample.Framework.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CleanSample.Framework.Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructureFramework<TDbContext, TIdentityUser, TIdentityRole>(this IServiceCollection services, Action<JsonWebTokenOption> jwtOption)
            where TDbContext : DbContextBase
            where TIdentityUser : ApplicationUser
            where TIdentityRole : IdentityRole
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<TDbContext>();
        services.AddScoped<DbContextBase>(provider => provider.GetRequiredService<TDbContext>());
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TDbContext>());
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddHostedService<OutboxBackgroundService>();

        services.AddIdentity<TIdentityUser, TIdentityRole>()
            .AddEntityFrameworkStores<TDbContext>()
            .AddDefaultTokenProviders();

        var jwtConfig = new JsonWebTokenOption();
        jwtOption(jwtConfig);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = jwtConfig.RequireHttpsMetadata;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = jwtConfig.ValidAudience,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SigningKey))
                };
            });

        services.AddScoped<IIdentityService, IdentityService>();
        
        return services;
    }
}