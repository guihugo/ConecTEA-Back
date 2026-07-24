using Conectea.Application.Features.Invitations.AcceptInvitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers
{
    [ApiController]
    [Route("api/appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly IGuardianService _guardianService;
        public AppointmentController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }
        [Authorize(Roles = "Therapist")]
        [HttpGet("create")]
        public async Task<IActionResult> CreateAppointment()
        {
            return Ok();
        }
    }
}
