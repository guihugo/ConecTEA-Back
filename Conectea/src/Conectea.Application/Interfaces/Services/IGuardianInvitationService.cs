using Conectea.Application.Features.Invitations.AcceptInvitation;

public interface IGuardianInvitationService
{
    Task<string> CreateAsync(Guid patientId);

    Task<AcceptInvitationResponse> AcceptAsync(string code);
    Task<bool> HasLinked();
}