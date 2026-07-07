using Conectea.Infrastructure.Authentication;
using Conectea.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conectea.Infrastructure.Persistence.Configurations;

public class PatientGuardianConfiguration : IEntityTypeConfiguration<PatientGuardian>
{
    public void Configure(EntityTypeBuilder<PatientGuardian> builder)
    {
        builder.ToTable("PatientGuardians");

        builder.HasKey(x => new
        {
            x.PatientId,
            x.GuardianId
        });

        builder.HasOne(x => x.Patient)
            .WithMany(x => x.Guardians)
            .HasForeignKey(x => x.PatientId);

        builder.HasOne(x => x.Guardian)
            .WithMany()
            .HasForeignKey(x => x.GuardianId);

        builder.Property(x => x.Relationship)
            .IsRequired();
    }
}