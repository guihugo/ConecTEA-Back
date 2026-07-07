using Conectea.Domain.Entities;
using Conectea.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conectea.Infrastructure.Persistence.Configurations;

public class PatientTherapistConfiguration : IEntityTypeConfiguration<PatientTherapist>
{
    public void Configure(EntityTypeBuilder<PatientTherapist> builder)
    {
        builder.ToTable("PatientTherapists");

        builder.HasKey(x => new
        {
            x.PatientId,
            x.TherapistId
        });

        builder.HasOne(x => x.Patient)
            .WithMany(x => x.Therapists)
            .HasForeignKey(x => x.PatientId);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.TherapistId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.IsMainTherapist)
            .IsRequired();
    }
}