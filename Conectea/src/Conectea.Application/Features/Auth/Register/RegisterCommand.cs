using Conectea.Domain.Enums;

namespace Conectea.Application.Features.Auth.Register;

public class RegisterCommand
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateTime DateOfBirth { get; set; }
}