public interface IReportService
{
    Task CreateAsync(CreateReportRequest request);
    Task<Report?> GetByIdAsync(Guid id);
    Task<IEnumerable<Report>> GetByPatientIdAsync(Guid patientId);
}