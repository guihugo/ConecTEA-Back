using Conectea.Application.Abstractions.Authentication;
using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;

namespace Conectea.Application.Features.Auth.Register;

public class RegisterHandler
{
    private readonly IIdentityService _identityService;

    public RegisterHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<RegisterResponse> Handle(RegisterCommand command)
    {
        var result = await _identityService.RegisterAsync(
            command.FullName,
            command.Email,
            command.Password,
            command.DateOfBirth
        );

        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors);
        }

        return new RegisterResponse
        {
            Id = result.UserId!.Value,
            Email = command.Email
        };
    }
}