using Conectea.Domain.Enums;

namespace Conectea.Application.DTOs.Authentication;

public class IdentityOperationResponse
{
    public bool Succeeded { get; init; }

    public Guid? UserId { get; init; }
    public string? Email { get; init; }
    public UserRole? Role { get; init; }

    public IReadOnlyCollection<string> Errors { get; init; } = [];
}