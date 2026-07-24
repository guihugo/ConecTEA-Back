public class UpdateAppointmentRequest
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Notes { get; set; }
}