using Conectea.Domain.Enums;

public class GuardianInvitation
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public InvitationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? AcceptedAt { get; set; }
}