using Conectea.Application.Interfaces.Repositories;
using Conectea.Domain.Enums;

public class CreateInvitationHandler
{
    private readonly IGuardianInvitationRepository _repository;
    private readonly IInvitationCodeGenerator _generator;

    public CreateInvitationHandler(
        IGuardianInvitationRepository repository,
        IInvitationCodeGenerator generator)
    {
        _repository = repository;
        _generator = generator;
    }

    public async Task<CreateInvitationResponse> Handle(CreateInvitationCommand command)
    {
        string code = _generator.Generate();

        var invitation = new GuardianInvitation
        {
            Id = Guid.NewGuid(),
            PatientId = command.PatientId,
            Email = command.Email,
            Token = code,
            Status = InvitationStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(invitation);

        return new CreateInvitationResponse
        {
            Code = code
        };
    }
}