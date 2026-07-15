using Conectea.Application.Features.Auth.Login;
using Conectea.Application.Features.Auth.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> Register([FromBody] RegisterCommand command, [FromServices] RegisterHandler handler)
    {
        RegisterResponse response = await handler.Handle(command);

        if (!response.Succeeded)
            return BadRequest(response);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginCommand command, [FromServices] LoginHandler handler)
    {
        LoginResponse response = await handler.Handle(command);

        if (!response.Succeeded)
            return Unauthorized(response);

        return Ok(response);
    }

}