using Conectea.Application.Features.Auth.Login;
using Conectea.Application.Features.Auth.Register;
using Conectea.Application.Interfaces;
using Conectea.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Conectea.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IGuardianInvitationService, GuardianInvitationService>();

        services.AddScoped<RegisterHandler>();
        services.AddScoped<LoginHandler>();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}