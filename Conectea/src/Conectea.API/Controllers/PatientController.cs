using Conectea.Application.Features.Invitations.AcceptInvitation;
using Conectea.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatePatientResponse>> CreatePatient(CreatePatientRequest request)
    {
        var response = await _patientService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id = response.PatientId }, response);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetAll()
    {
        var patients = await _patientService.GetAllAsync();

        return Ok(patients);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PatientResponse>> GetById(Guid id)
    {
        var patient = await _patientService.GetByIdAsync(id);

        if (patient is null)
            return NotFound();

        return Ok(patient);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdatePatientRequest request)
    {
        await _patientService.UpdateAsync(id, request);

        return NoContent();
    }
    [HttpGet("appointment/{id:guid}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var appointment = await _patientService.GetPatientAppointments(id);

        return Ok(appointment);
    }
    

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeleteAsync(id);

        return NoContent();
    }
}