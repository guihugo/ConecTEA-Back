using Conectea.Domain.Entities;

namespace Conectea.Application.Interfaces.Repositories;

public interface IReportRepository
{
    Task<Report?> GetByIdAsync(Guid id);

    Task<IEnumerable<Report>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<Report>> GetByTherapistIdAsync(Guid therapistId);

    Task AddAsync(Report report);

    Task UpdateAsync(Report report);
}