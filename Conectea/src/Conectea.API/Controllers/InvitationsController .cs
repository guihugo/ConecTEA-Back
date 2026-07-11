using Conectea.Application.Features.Invitations.AcceptInvitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationsController : ControllerBase
    {
        private readonly IGuardianInvitationService _guardianInvitationService;
        public InvitationsController(IGuardianInvitationService guardianInvitationService)
        {
            _guardianInvitationService = guardianInvitationService;
        }

        [HttpPut("accept")]
        public async Task<IActionResult> AcceptInvite([FromBody] AcceptInvitationCommand command)
        {
            var response = await _guardianInvitationService.AcceptAsync(command.Code);

            return Ok(response);
        }

        [HttpGet("linked-patient")]
        public async Task<IActionResult> HasLinkedPatient()
        {
            var hasLinkedPatient = await _guardianInvitationService.HasLinked();

            return Ok(new
            {
                hasLinkedPatient
            });
        }
    }
}
