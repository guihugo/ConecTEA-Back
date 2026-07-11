using Conectea.Domain.Entities;

namespace Conectea.Application.Interfaces.Repositories;

public interface IGuardianInvitationRepository
{
    Task AddAsync(GuardianInvitation invitation);

    Task<GuardianInvitation?> GetByTokenAsync(string token);

    Task UpdateAsync(GuardianInvitation invitation);
}