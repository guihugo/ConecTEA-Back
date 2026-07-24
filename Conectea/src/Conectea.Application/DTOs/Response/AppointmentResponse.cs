using Conectea.Application.DTOs;
using Conectea.Application.DTOs.Patients;

public class AppointmentResponse
{
    public Guid Id { get; set; }

    public PatientSummaryResponse Patient { get; set; } = null!;

    public TherapistSummaryResponse Therapist { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public AppointmentStatus Status { get; set; }

    public string? Notes { get; set; }
}