using Conectea.Domain.Entities;
using Conectea.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conectea.Infrastructure.Persistence.Configurations;

public class TherapistConfiguration : IEntityTypeConfiguration<Therapist>
{
    public void Configure(EntityTypeBuilder<Therapist> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Therapist>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(x => x.Patients)
            .WithOne(x => x.Therapist)
            .HasForeignKey(x => x.TherapistId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}