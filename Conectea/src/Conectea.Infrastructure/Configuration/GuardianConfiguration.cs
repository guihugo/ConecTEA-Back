using Conectea.Domain.Entities;
using Conectea.Infrastructure.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conectea.Infrastructure.Persistence.Configurations;

public class GuardianConfiguration : IEntityTypeConfiguration<Guardian>
{
    public void Configure(EntityTypeBuilder<Guardian> builder)
    {
        builder.HasKey(x => x.Id);


        builder.HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<Guardian>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasMany(x => x.Patients)
            .WithOne(x => x.Guardian)
            .HasForeignKey(x => x.GuardianId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}