using Conectea.Domain.Enums;

public class CreatePatientRequest
{
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Diagnosis { get; set; }
    public string? Observation { get; set; }
    public string? GuardianEmail { get; internal set; }
}