using Conectea.Domain.Enums;

namespace Conectea.Domain.Entities;

public class PatientGuardian
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public Guid GuardianId { get; set; }
    public Guardian Guardian { get; set; } = null!;
    public GuardianRelationship Relationship { get; set; }
}