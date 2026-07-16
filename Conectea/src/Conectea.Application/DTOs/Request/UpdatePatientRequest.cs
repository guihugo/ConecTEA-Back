using Conectea.Domain.Enums;

public class UpdatePatientRequest
{
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Diagnosis { get; set; }
    public string? Observation { get; set; }    
}