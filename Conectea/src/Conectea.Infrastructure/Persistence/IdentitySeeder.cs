using Conectea.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Conectea.Infrastructure.Authentication;

public static class IdentitySeeder
{
    public static async Task SeedAsync(
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        foreach (var role in Enum.GetNames<UserRole>())
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new IdentityRole<Guid>(role)
                );
            }
        }
    }
}