using Conectea.Domain.Enums;

namespace Conectea.Application.Features.Auth.Login;

public class LoginResponse
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } 
    public int Role { get; set; }
}