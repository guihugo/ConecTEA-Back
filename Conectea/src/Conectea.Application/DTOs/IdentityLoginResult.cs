namespace Conectea.Application.Abstractions.Authentication;

public class IdentityLoginResult
{
    public bool Succeeded { get; set; }

    public Guid? UserId { get; set; }

    public string? Error { get; set; }
}