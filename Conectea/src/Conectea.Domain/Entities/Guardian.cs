namespace Conectea.Domain.Entities;

public class Guardian
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public ICollection<PatientGuardian> Patients { get; set; } = [];
}