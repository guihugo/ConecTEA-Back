using Conectea.Application.Abstractions.Authentication;
using Conectea.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Conectea.Infrastructure.Authentication;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<IdentityOperationResult> RegisterAsync(
        string fullName,
        string email,
        string password,
        DateTime dateOfBirth)
    {
        var existingUser = await _userManager.FindByEmailAsync(email);

        if (existingUser != null)
        {
            return new IdentityOperationResult
            {
                Succeeded = false,
                Errors = ["Usuário já cadastrado."]
            };
        }


        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName,
            DateOfBirth = DateOnly.FromDateTime(dateOfBirth),
            IsActive = true
        };


        var result = await _userManager.CreateAsync(
            user,
            password
        );


        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(x => x.Description)
            );

            return new IdentityOperationResult
            {
                Succeeded = false,
                Errors = [errors]
            };
        }


        return new IdentityOperationResult
        {
            Succeeded = true,
            UserId = user.Id
        };
    }
}
