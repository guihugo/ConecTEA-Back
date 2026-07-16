using Conectea.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conectea.Infrastructure.Persistence.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Title)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(r => r.ReportType)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(r => r.EncryptedContent)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .HasDefaultValueSql("NOW()")
            .IsRequired();

        builder.Property(r => r.UpdatedAt);


        builder.HasOne(r => r.Patient)
            .WithMany(p => p.Reports)
            .HasForeignKey(r => r.PatientId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasIndex(r => new
        {
            r.PatientId,
            r.CreatedAt
        });
    }
}