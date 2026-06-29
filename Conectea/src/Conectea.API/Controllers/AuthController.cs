using Conectea.Application.Features.Auth.Register;
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
}