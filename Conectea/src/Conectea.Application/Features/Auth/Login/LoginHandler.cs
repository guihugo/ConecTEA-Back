using Conectea.Application.DTOs.Authentication;
using Conectea.Application.Interfaces;

namespace Conectea.Application.Features.Auth.Login;

public class LoginHandler
{
    private readonly IIdentityService _identityService;
    private readonly IJwtTokenService _jwtTokenService;

    public LoginHandler(IIdentityService identityService, IJwtTokenService jwtTokenService)
    {
        _identityService = identityService;
        _jwtTokenService = jwtTokenService;
    }


    public async Task<LoginResponse> Handle(LoginCommand command)
    {
        IdentityLoginResult result = await _identityService.LoginAsync(
            command.Email,
            command.Password);


        if (!result.Succeeded)
        {
            throw new Exception(result.Error);
        }   
        
        string token = _jwtTokenService.GenerateToken(
            result.UserId!.Value,
            result.Email!
        );

        return new LoginResponse
        {
            UserId = result.UserId!.Value,
            Email = command.Email,
            Role = (int)result.Role!.Value,
            Token = token
        };
    }
}