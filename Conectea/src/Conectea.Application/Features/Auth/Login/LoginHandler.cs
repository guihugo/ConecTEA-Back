using Conectea.Application.Interfaces;

namespace Conectea.Application.Features.Auth.Login;

public class LoginHandler
{
    private readonly IIdentityService _identityService;

    public LoginHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }


    public async Task<LoginResponse> Handle(LoginCommand command)
    {
        var result = await _identityService.LoginAsync(
            command.Email,
            command.Password);


        if (!result.Succeeded)
        {
            throw new Exception(result.Error);
        }


        return new LoginResponse
        {
            UserId = result.UserId!.Value,
            Email = command.Email
        };
    }
}