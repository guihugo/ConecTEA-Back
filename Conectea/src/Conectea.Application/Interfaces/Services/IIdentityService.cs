using Conectea.Application.DTOs.Authentication;
using Conectea.Application.Features.Users.Me;
using Conectea.Domain.Enums;

namespace Conectea.Application.Interfaces;

public interface IIdentityService
{
    Task<IdentityOperationResponse> RegisterAsync(
        string fullName,
        string email,
        string password,
        UserRole role,
        DateTime dateOfBirth
    );
    Task<IdentityLoginResponse> LoginAsync(string email, string password);
    Task<CurrentUserResponse> GetCurrentUserAsync(Guid userId);
}