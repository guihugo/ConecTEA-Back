using Conectea.Domain.Enums;

namespace Conectea.Application.Features.Auth.Register;

public class RegisterResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}