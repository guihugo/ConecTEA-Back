using Conectea.Domain.Entities;

namespace Conectea.Application.Interfaces.Repositories;

public interface ITherapistRepository
{
    Task<Therapist?> GetByUserIdAsync(Guid userId);
    Task AddAsync(Therapist therapist);

}