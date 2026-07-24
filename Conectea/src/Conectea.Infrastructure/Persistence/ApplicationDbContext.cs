using Conectea.Domain.Entities;
using Conectea.Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Conectea.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Therapist> Therapists => Set<Therapist>();
    public DbSet<Guardian> Guardians => Set<Guardian>();
    public DbSet<PatientTherapist> PatientTherapists => Set<PatientTherapist>();
    public DbSet<PatientGuardian> PatientGuardians => Set<PatientGuardian>();
    public DbSet<GuardianInvitation> GuardianInvitations => Set<GuardianInvitation>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);


        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(u => u.FullName)
                .HasMaxLength(200);

            entity.Property(u => u.Email)
                .HasMaxLength(256);
        });


        builder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
    }
}