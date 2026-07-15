using Conectea.Domain.Entities;
using Conectea.Domain.Enums;

public class Patient
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? Diagnosis { get; set; }
    public string? Observation { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public ICollection<PatientTherapist> Therapists { get; set; } = [];
    public ICollection<PatientGuardian> Guardians { get; set; } = [];

    // Futuramente
    // public ICollection<Session> Sessions { get; set; } = [];
    // public ICollection<Report> Reports { get; set; } = [];
}