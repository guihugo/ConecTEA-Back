public class CreateInvitationCommand
{
    public Guid PatientId { get; set; }
    public string Email { get; set; } = string.Empty;
}