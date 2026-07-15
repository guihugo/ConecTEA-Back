using Conectea.Domain.Enums;

namespace Conectea.Application.Interfaces;

public interface IUserProfileService
{
    Task CreateAsync( Guid userId, UserRole role);
}