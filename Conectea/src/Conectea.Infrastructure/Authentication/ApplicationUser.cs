using Microsoft.AspNetCore.Identity;


namespace Conectea.Infrastructure.Authentication;
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; } 
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}