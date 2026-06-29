using Conectea.Application.Abstractions.Authentication;
using Conectea.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Conectea.Infrastructure.Authentication;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityLoginResult> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return new IdentityLoginResult
            {
                Succeeded = false,
                Error = "Usuário ou senha inválidos."
            };
        }


        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            password,
            false);

        
        if (!result.Succeeded)
        {
            return new IdentityLoginResult
            {
                Succeeded = false,
                Error = "Usuário ou senha inválidos."
            };
        }


        return new IdentityLoginResult
        {
            Succeeded = true,
            UserId = user.Id,   
            Email = user.Email
        };
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
