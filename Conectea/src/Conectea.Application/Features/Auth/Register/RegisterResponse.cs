using Conectea.Domain.Enums;

namespace Conectea.Application.Features.Auth.Register;

public class RegisterResponse
{
    public bool Succeeded { get; set; }
    public IReadOnlyCollection<string> Errors { get; init; } = [];
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}