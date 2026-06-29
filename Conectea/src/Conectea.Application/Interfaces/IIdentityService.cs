using Conectea.Application.Abstractions.Authentication;

namespace Conectea.Application.Interfaces;

public interface IIdentityService
{
    Task<IdentityOperationResult> RegisterAsync(
        string fullName,
        string email,
        string password,
        DateTime dateOfBirth
    );
    Task<IdentityLoginResult> LoginAsync(string email, string password);
}