using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);


        builder.HasOne(a => a.Patient)
            .WithMany()
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(a => a.Therapist)
            .WithMany()
            .HasForeignKey(a => a.TherapistId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(a => a.Status)
            .HasConversion<int>();


        builder.Property(a => a.Notes)
            .HasMaxLength(1000);


        builder.Property(a => a.CreatedAt)
            .HasDefaultValueSql("NOW()");


        builder.Property(a => a.UpdatedAt)
            .HasDefaultValueSql("NOW()");
    }
}