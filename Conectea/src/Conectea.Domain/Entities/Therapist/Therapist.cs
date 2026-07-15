namespace Conectea.Domain.Entities;

public class Therapist
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ICollection<PatientTherapist> Patients { get; set; } = [];
}