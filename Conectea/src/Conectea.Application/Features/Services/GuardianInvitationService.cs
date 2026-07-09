using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Enums;

public class GuardianInvitationService : IGuardianInvitationService
{
    private readonly IGuardianInvitationRepository _repository;
    private readonly IInvitationCodeGenerator _codeGenerator;

    public GuardianInvitationService( IGuardianInvitationRepository repository, IInvitationCodeGenerator codeGenerator)
    {
        _repository = repository;
        _codeGenerator = codeGenerator;
    }

    public Task AcceptAsync(string code, Guid guardianId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> CreateAsync(Guid patientId)
    {
        string code = _codeGenerator.Generate();

        var invitation = new GuardianInvitation
        {
            Id = Guid.NewGuid(),
            PatientId = patientId,
            Token = code,
            Status = InvitationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(invitation);

        return code;
    }
}

