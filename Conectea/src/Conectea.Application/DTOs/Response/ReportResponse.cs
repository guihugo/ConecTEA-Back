public class ReportResponse
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string Title { get; set; }
    public ReportType ReportType { get; set; }
    public ReportStatus Status { get; set; }
    public string Content { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}