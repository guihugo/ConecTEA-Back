using Conectea.Domain.Entities;
using Conectea.Infrastructure.Authentication;
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

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.GuardianId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.Relationship)
            .IsRequired();
    }
}