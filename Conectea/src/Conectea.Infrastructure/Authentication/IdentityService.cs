using Conectea.Application.DTOs.Authentication;
using Conectea.Application.Features.Users.Me;
using Conectea.Application.Interfaces;
using Conectea.Domain.Enums;
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
        ApplicationUser? user = await _userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return new IdentityLoginResult
            {
                Succeeded = false,
                Error = "Usuário ou senha inválidos."
            };
        }


        SignInResult result = await _signInManager.CheckPasswordSignInAsync(
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

        IList<string> roles = await _userManager.GetRolesAsync(user);

        string? role = roles.FirstOrDefault();

        UserRole parsed = Enum.Parse<UserRole>(role);

        return new IdentityLoginResult
        {
            Succeeded = true,
            UserId = user.Id,   
            Email = user.Email,
            Role = (int)parsed
        };
    }

    public async Task<IdentityOperationResult> RegisterAsync(
        string fullName,
        string email,
        string password,
        UserRole role,
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
            Role = role,
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

        await _userManager.AddToRoleAsync(
            user,
            role.ToString()
        );

        return new IdentityOperationResult
        {
            Succeeded = true,
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role
        };
    }
    public async Task<CurrentUserResponse> GetCurrentUserAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            throw new Exception("Usuário não encontrado.");

        var roles = await _userManager.GetRolesAsync(user);

        return new CurrentUserResponse
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email!,
            Role = Enum.Parse<UserRole>(roles.First())
        };
    }
}
