namespace Conectea.Application.DTOs;

public class TherapistSummaryResponse
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string? Specialty { get; set; }
}