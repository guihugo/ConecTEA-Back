using Conectea.Domain.Enums;

namespace Conectea.Application.DTOs.Patients;

public class PatientSummaryResponse
{
    public Guid Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public int Age { get; set; }

    public Gender Gender { get; set; }

    public string? Diagnosis { get; set; }
}