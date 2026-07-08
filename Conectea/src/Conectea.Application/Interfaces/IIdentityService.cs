using Conectea.Application.DTOs.Authentication;
using Conectea.Application.Features.Users.Me;
using Conectea.Domain.Enums;

namespace Conectea.Application.Interfaces;

public interface IIdentityService
{
    Task<IdentityOperationResult> RegisterAsync(
        string fullName,
        string email,
        string password,
        UserRole role,
        DateTime dateOfBirth
    );
    Task<IdentityLoginResult> LoginAsync(string email, string password);
    Task<CurrentUserResponse> GetCurrentUserAsync(Guid userId);
}