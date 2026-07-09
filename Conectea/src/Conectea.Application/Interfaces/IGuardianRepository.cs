using Conectea.Domain.Entities;

namespace Conectea.Application.Interfaces.Repositories;

public interface IGuardianRepository
{
    Task<Guardian?> GetByUserIdAsync(Guid userId);

    Task<Guardian?> GetByIdAsync(Guid id);

    Task AddAsync(Guardian guardian);
}