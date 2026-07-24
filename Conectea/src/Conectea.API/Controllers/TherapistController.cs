using Conectea.Application.Features.Invitations.AcceptInvitation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers
{
    [ApiController]
    [Route("api/therapist")]
    [Authorize(Roles = "Therapist")]
    public class TherapistController : ControllerBase
    {
        private readonly ITherapistService _therapistService;
        public TherapistController(ITherapistService therapistService)
        {
            _therapistService = therapistService;
        }
        [HttpGet("patients/all")]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> GetTherapistPatients()
        {
            IEnumerable<PatientResponse> patients = await _therapistService.GetByPacientByTherapistIdAsync();

            return Ok(patients);
        }
    }
}
