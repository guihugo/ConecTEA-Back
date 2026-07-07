using Conectea.Domain.Enums;

namespace Conectea.Domain.Entities;

public class PatientGuardian
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public Guid GuardianId { get; set; }
    public GuardianRelationship Relationship { get; set; }
}