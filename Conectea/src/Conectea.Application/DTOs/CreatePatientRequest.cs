using Conectea.Domain.Enums;

public class CreatePatientRequest
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Diagnosis { get; set; }
    public string? Observation { get; set; }
    public Guid GuardianId { get; set; }
    public Guid? TherapistId { get; set; }
    public GuardianRelationship GuardianRelationship { get; set; }
}