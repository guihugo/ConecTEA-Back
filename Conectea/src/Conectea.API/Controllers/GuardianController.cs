using Conectea.Application.Features.Invitations.AcceptInvitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers
{
    [ApiController]
    [Route("api/guardian")]
    public class GuardianController : ControllerBase
    {
        private readonly IGuardianService _guardianService;
        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            return Ok();
        }
    }
}
