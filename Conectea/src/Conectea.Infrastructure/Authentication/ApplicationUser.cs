using Microsoft.AspNetCore.Identity;


namespace Conectea.Infrastructure.Authentication;
public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; } 
    public bool IsActive { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}