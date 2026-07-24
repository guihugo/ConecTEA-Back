public class CreateAppointmentRequest
{
    public Guid PatientId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Notes { get; set; }
}