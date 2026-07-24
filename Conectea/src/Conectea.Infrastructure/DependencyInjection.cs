using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Infrastructure.Authentication;
using Conectea.Infrastructure.Persistence;
using Conectea.Infrastructure.Persistence.Repositories;
using Conectea.Infrastructure.Repositories;
using Conectea.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Conectea.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // JWT Settings
        var jwtSettings = new JwtSettings();

        configuration
            .GetSection("Jwt")
            .Bind(jwtSettings);

        // Database
        var connectionString = configuration
            .GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });


        // Identity
        services
            .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        if (string.IsNullOrWhiteSpace(jwtSettings.Key))
        {
            throw new InvalidOperationException(
                "Jwt:Key is not configured. Configure it using 'dotnet user-secrets' or an environment variable.");
        }

        services.AddSingleton(jwtSettings);


        // Authentication JWT
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
            });


        //Encryption Settings
        var encryptionSettings = new EncryptionSettings();

        configuration
            .GetSection("Encryption")
            .Bind(encryptionSettings);

        if (string.IsNullOrWhiteSpace(encryptionSettings.Key))
        {
            throw new InvalidOperationException(
                "Encryption:Key is not configured. Configure it using 'dotnet user-secrets' or an environment variable.");
        }

        services.AddSingleton(encryptionSettings);

        // Services
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<IInvitationCodeGenerator, InvitationCodeGenerator>();
        services.AddScoped<IEncryptionService, AesEncryptionService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<ITherapistService, TherapistService>();

        // Repositories
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<ITherapistRepository, TherapistRepository>();
        services.AddScoped<IGuardianRepository, GuardianRepository>();
        services.AddScoped<IGuardianInvitationRepository, GuardianInvitationRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUser>();

        return services;
    }
}