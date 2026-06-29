using Conectea.Infrastructure.Authentication;
using Conectea.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Conectea.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );
        });


        services
            .AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        return services;
    }
}