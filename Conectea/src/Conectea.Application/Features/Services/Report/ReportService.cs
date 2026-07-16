using Conectea.Application.Exceptions;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;

public class ReportService : IReportService
{
    private readonly IReportRepository _repository;
    private readonly IEncryptionService _encryption;
    private readonly ICurrentUser _currentUserService;
    private readonly ITherapistRepository _therapistRepository;


    public ReportService(IReportRepository repository, IEncryptionService encryption, ICurrentUser currentUserService, ITherapistRepository therapistRepository)
    {
        _repository = repository;
        _encryption = encryption;
        _currentUserService = currentUserService;
        _therapistRepository = therapistRepository;
    }


    public async Task CreateAsync(CreateReportRequest dto)
    {
        var userId = _currentUserService.UserId;


        var therapist = await _therapistRepository
            .GetByUserIdAsync(userId);

        if (therapist is null)
        {
            throw new NotFoundException("Terapeuta não encontrado.");
        }
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
            CreatedBy = therapist.Id,
            CreatedAt = DateTime.UtcNow
        };


        await _repository.AddAsync(report);
    }

    public async Task<Report?> GetByIdAsync(Guid id)
    {
        var report = await _repository.GetByIdAsync(id);

        if (report is null)
            return null;

        report.EncryptedContent =
            _encryption.Decrypt(report.EncryptedContent);

        return report;
    }
    public async Task<IEnumerable<Report>> GetByPatientIdAsync(Guid patientId)
    {
        return await _repository.GetByPatientIdAsync(patientId);
    }
}