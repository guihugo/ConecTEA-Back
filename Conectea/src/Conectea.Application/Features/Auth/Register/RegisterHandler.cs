using Conectea.Application.DTOs.Authentication;
using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;

namespace Conectea.Application.Features.Auth.Register;

public class RegisterHandler
{
    private readonly IIdentityService _identityService;
    private readonly IUserProfileService _profileService;


    public RegisterHandler(IIdentityService identityService, IUserProfileService profileService)
    {
        _identityService = identityService;
        _profileService = profileService;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand command)
    {
        IdentityOperationResult result = await _identityService.RegisterAsync(
            command.FullName,
            command.Email,
            command.Password,
            command.Role,
            command.DateOfBirth
        );

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors);
        }

        await _profileService.CreateAsync(
            result.UserId!.Value,
            command.Role
        );
        
        return new RegisterResponse
        {
            Id = result.UserId!.Value,
            Email = command.Email,
            Role = command.Role
        };
    }
}