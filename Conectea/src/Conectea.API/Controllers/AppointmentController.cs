using Conectea.Application.DTOs;
using Conectea.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers;

[ApiController]
[Route("api/appointment")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [Authorize(Roles = "Therapist")]
    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        AppointmentResponse appointment = await _appointmentService.CreateAsync(request);
        return Ok(appointment);
    }

    [Authorize(Roles = "Therapist")]
    [HttpGet("{appointmentId:guid}")]
    public async Task<IActionResult> GetById(Guid appointmentId)
    {
        AppointmentResponse? appointment = await _appointmentService.GetByIdAsync(appointmentId);
        return Ok(appointment);
    }

    [Authorize(Roles = "Therapist")]
    [HttpGet("therapist")]
    public async Task<IActionResult> GetTherapistAppointments()
    {
        var appointments = await _appointmentService.GetTherapistAppointmentsAsync();
        return Ok(appointments);
    }

    [Authorize(Roles = "Guardian")]
    [HttpGet("guardian/next")]
    public async Task<IActionResult> GetGuardianNextAppointment()
    {
        var appointment = await _appointmentService.GetGuardianNextAppointmentAsync();
        return Ok(appointment);
    }

    [Authorize(Roles = "Therapist")]
    [HttpPut("{appointmentId:guid}")]
    public async Task<IActionResult> UpdateAppointment(
        Guid appointmentId,
        [FromBody] UpdateAppointmentRequest request)
    {
        await _appointmentService.UpdateAsync(appointmentId, request);
        return NoContent();
    }

    [Authorize(Roles = "Therapist")]
    [HttpPatch("{appointmentId:guid}/status")]
    public async Task<IActionResult> UpdateStatus(
        Guid appointmentId,
        [FromBody] UpdateAppointmentStatusRequest request)
    {
        await _appointmentService.UpdateStatusAsync(appointmentId, request);
        return NoContent();
    }

    [Authorize(Roles = "Therapist")]
    [HttpDelete("{appointmentId:guid}")]
    public async Task<IActionResult> DeleteAppointment(Guid appointmentId)
    {
        await _appointmentService.DeleteAsync(appointmentId);
        return NoContent();
    }
}