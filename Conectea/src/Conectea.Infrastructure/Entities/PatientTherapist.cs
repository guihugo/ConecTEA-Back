using Conectea.Infrastructure.Authentication;

namespace Conectea.Infrastructure.Persistence.Entities;

public class PatientTherapist
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public Guid TherapistId { get; set; }
    public ApplicationUser Therapist { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsMainTherapist { get; set; }
}