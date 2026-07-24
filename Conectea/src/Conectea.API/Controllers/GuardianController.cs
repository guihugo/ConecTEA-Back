using Conectea.Application.Features.Invitations.AcceptInvitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers
{
    [ApiController]
    [Route("api/guardian")]
    [Authorize(Roles = "Guardian")]
    public class GuardianController : ControllerBase
    {
        private readonly IGuardianService _guardianService;
        public GuardianController(IGuardianService guardianService)
        {
            _guardianService = guardianService;
        }
        [HttpGet("patient")]
        public async Task<ActionResult<PatientResponse>> GetGuardianPatient()
        {
            PatientResponse? patient = await _guardianService.GetByPacientByGuardiantIdAsync();

            return Ok(patient);
        }
    }
}
