public interface IReportService
{
    Task CreateAsync(CreateReportRequest request);
    Task<Report?> GetByIdAsync(Guid id);
    Task<IEnumerable<ReportResponse>> GetByPatientIdAsync(Guid patientId);
    Task<IEnumerable<ReportResponse>> GetAllAsync();
}