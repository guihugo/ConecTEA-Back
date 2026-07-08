namespace Conectea.Application.Features.Users.Me;

using Conectea.Domain.Enums;

public class CurrentUserResponse
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}