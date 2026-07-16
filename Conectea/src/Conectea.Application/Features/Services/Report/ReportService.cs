using Conectea.Application.Interfaces.Repositories;

public class ReportService : IReportService
{
    private readonly IReportRepository _repository;
    private readonly IEncryptionService _encryption;


    public ReportService(  IReportRepository repository, IEncryptionService encryption)
    {
        _repository = repository;
        _encryption = encryption;
    }


    public async Task CreateAsync(CreateReportRequest dto)
    {
        var encrypted =
            _encryption.Encrypt(dto.Content);


        var report = new Report
        {
            Id = Guid.NewGuid(),
            PatientId = dto.PatientId,
            Title = dto.Title,
            ReportType = dto.ReportType,
            Status = ReportStatus.Generated,
            EncryptedContent = encrypted,
            CreatedBy = dto.CreatedBy,
            CreatedAt = DateTime.UtcNow
        };


        await _repository.AddAsync(report);
    }
}