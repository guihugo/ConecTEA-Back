using Conectea.Application.Interfaces;
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
    public async Task<ActionResult<Guid>> Create(CreatePatientRequest request)
    {
        var id = await _patientService.CreateAsync(request);

        return CreatedAtAction(nameof(GetById), new { id }, id);
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
    [HttpGet("therapists/{therapistId:guid}")]
    public async Task<ActionResult<IEnumerable<PatientResponse>>> GetByTherapistIdAsync(Guid therapistId)
    {
        var patients = await _patientService.GetByTherapistIdAsync(therapistId);

        return Ok(patients);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdatePatientRequest request)
    {
        await _patientService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _patientService.DeleteAsync(id);

        return NoContent();
    }
}