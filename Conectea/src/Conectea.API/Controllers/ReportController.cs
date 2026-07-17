using Conectea.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conectea.API.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize]
public class ReportController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateReportRequest request)
    {
        await _reportService.CreateAsync(request);

        return Ok();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByReportIdAsync(Guid id)
    {
        Report? report = await _reportService.GetByIdAsync(id);

        if (report is null)
        {
            return NotFound();
        }

        return Ok(report);
    }

    // GET /api/reports/patient/{patientId}
    [HttpGet("patient/{patientId:guid}")]
    public async Task<ActionResult<IEnumerable<ReportResponse>>> GetByPatientIdAsync(Guid patientId)
    {
        IEnumerable<ReportResponse>? reports = await _reportService.GetByPatientIdAsync(patientId);

        if (reports is null)
        {
            return NotFound();
        }

        return Ok(reports);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReportResponse>>> GetAllAsync()
    {
        IEnumerable<ReportResponse>? reports = await _reportService.GetAllAsync();

        if(reports is null)
        {
            return NotFound();
        }
        return Ok(reports);
    }
}