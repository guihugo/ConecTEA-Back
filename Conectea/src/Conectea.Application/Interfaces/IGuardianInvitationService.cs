namespace Conectea.Application.Interfaces;

public interface IGuardianInvitationService
{
    Task<string> CreateAsync(Guid patientId);

    Task AcceptAsync(string code, Guid guardianId);
}