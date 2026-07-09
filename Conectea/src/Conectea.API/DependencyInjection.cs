using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Application.Services;
using Conectea.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Conectea.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Digite: Bearer {seu token JWT}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<ITherapistRepository, TherapistRepository>();
        services.AddScoped<IGuardianRepository, GuardianRepository>();

        return services;
    }
}