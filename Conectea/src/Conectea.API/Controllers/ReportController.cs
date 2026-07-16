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

    [HttpGet("{id:guid}") ]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        Report? report = await _reportService.GetByIdAsync(id);

        if (report is null)
        {
            return NotFound();
        }

        return Ok(report);
    }
}