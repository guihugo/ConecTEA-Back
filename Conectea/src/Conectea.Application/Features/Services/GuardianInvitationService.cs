using Conectea.Application.Exceptions;
using Conectea.Application.Features.Invitations.AcceptInvitation;
using Conectea.Application.Interfaces;
using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Enums;

public class GuardianInvitationService : IGuardianInvitationService
{
    private readonly IGuardianInvitationRepository _repository;
    private readonly IGuardianRepository _guardianRepository;
    private readonly IInvitationCodeGenerator _codeGenerator;
    private readonly ICurrentUser _currentUserService;

    public GuardianInvitationService(IGuardianInvitationRepository repository, IInvitationCodeGenerator codeGenerator,
        ICurrentUser currentUserService, IGuardianRepository guardianRepository)
    {
        _repository = repository;
        _codeGenerator = codeGenerator;
        _currentUserService = currentUserService;
        _guardianRepository = guardianRepository;
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
    public async Task<AcceptInvitationResponse> AcceptAsync(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            throw new ValidationException(
                "CÃ³digo do convite Ã© obrigatÃ³rio.");
        }

        var userId = _currentUserService.UserId;

        var guardian = await _guardianRepository.GetByUserIdAsync(userId);

        if (guardian is null)
        {
            throw new NotFoundException(
                "ResponsÃ¡vel nÃ£o encontrado.");
        }


        var invitation = await _repository.GetByTokenAsync(code);

        if (invitation is null)
        {
            throw new NotFoundException(
                "Convite nÃ£o encontrado.");
        }

        if (invitation.Status != InvitationStatus.Pending)
        {
            throw new ConflictException("Este convite jÃ¡ foi utilizado.");
        }

        await _guardianRepository.LinkPatientAsync(
            guardian.Id,
            invitation.PatientId);

        invitation.Status = InvitationStatus.Accepted;
        invitation.AcceptedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(invitation);

        return new AcceptInvitationResponse
        {
            PatientId = invitation.PatientId
        };
    }

    public async Task<bool> HasLinked()
    {
        var userId = _currentUserService.UserId;

        var guardian = await _guardianRepository.GetByUserIdAsync(userId)
            ?? throw new ValidationException("Usuï¿½rio nï¿½o possui responsï¿½vel associado.");

        return await _guardianRepository.LinkedPatientExistsAsync(guardian.Id);
    }
}

