using Conectea.Application.Features.Auth.Login;
using Conectea.Application.Features.Auth.Register;
using Microsoft.Extensions.DependencyInjection;

namespace Conectea.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<RegisterHandler>();
        services.AddScoped<LoginHandler>();

        return services;
    }
}