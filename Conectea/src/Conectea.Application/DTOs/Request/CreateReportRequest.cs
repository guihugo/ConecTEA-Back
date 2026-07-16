public class CreateReportRequest
{
    public Guid PatientId { get; set; }

    public string Title { get; set; } = "";

    public ReportType ReportType { get; set; }

    public string Content { get; set; } = "";

    public Guid CreatedBy { get; set; }
}