namespace Conectea.Domain.Entities;

public class PatientTherapist
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public Guid TherapistId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsMainTherapist { get; set; }
}