using Conectea.Domain.Enums;
using Conectea.Infrastructure.Authentication;


namespace Conectea.Infrastructure.Persistence.Entities;

public class PatientGuardian
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;
    public Guid GuardianId { get; set; }
    public ApplicationUser Guardian { get; set; } = null!;
    public GuardianRelationship Relationship { get; set; }
}