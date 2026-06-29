namespace Conectea.Application.Abstractions.Authentication;

public class IdentityOperationResult
{
    public bool Succeeded { get; init; }

    public Guid? UserId { get; init; }
    public string? Email { get; init; }

    public IReadOnlyCollection<string> Errors { get; init; } = [];
}