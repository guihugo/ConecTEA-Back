using Microsoft.EntityFrameworkCore;
using Conectea.Application.Interfaces.Repositories;

namespace Conectea.Infrastructure.Persistence.Repositories;

class ReportRepository : IReportRepository
{
   private readonly ApplicationDbContext _context;

    public ReportRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Report?> GetByIdAsync(Guid id)
    {
        return await _context.Reports
            .FirstOrDefaultAsync(r => r.Id == id);
    }


    public async Task<IEnumerable<Report>> GetByPatientIdAsync(Guid patientId)
    {
        return await _context.Reports
            .Where(r => r.PatientId == patientId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }


    public async Task AddAsync(Report report)
    {
        await _context.Reports.AddAsync(report);
    }


    public Task UpdateAsync(Report report)
    {
        _context.Reports.Update(report);

        return Task.CompletedTask;
    }
}