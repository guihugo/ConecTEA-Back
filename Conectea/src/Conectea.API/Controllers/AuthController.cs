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
    public async Task<ActionResult<RegisterResponse>> Register(
        [FromBody] RegisterCommand command,
        [FromServices] RegisterHandler handler)
    {
        var response = await handler.Handle(command);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginCommand command,
        [FromServices] LoginHandler handler)
    {
        var response = await handler.Handle(command);

        return Ok(response);
    }

    [HttpGet]
    [Authorize]
    public IActionResult Get()
    {
        return Ok("JWT funcionando!");
    }
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("Teste realizado com sucesso!");
    }
}