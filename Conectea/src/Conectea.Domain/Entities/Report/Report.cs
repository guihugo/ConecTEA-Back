public class Report
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string Title { get; set; } = string.Empty;
    public ReportType ReportType { get; set; }
    public ReportStatus Status { get; set; }
    public string EncryptedContent { get; set; } = string.Empty;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Patient Patient { get; set; } = null!;
}