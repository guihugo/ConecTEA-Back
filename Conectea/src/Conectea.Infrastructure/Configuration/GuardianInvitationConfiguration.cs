using Conectea.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conectea.Infrastructure.Persistence.Configurations;

public class GuardianInvitationConfiguration : IEntityTypeConfiguration<GuardianInvitation>
{
    public void Configure(
        EntityTypeBuilder<GuardianInvitation> builder)
    {
        builder.HasKey(x => x.Id);


        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);


        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(200);


        builder.HasOne(x => x.Patient)
            .WithMany()
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.Property(x => x.Status)
            .HasConversion<int>();
    }
}