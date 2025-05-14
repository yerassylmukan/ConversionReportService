namespace ConversionReportService.Contracts.Messages;

public class ReportGenerated
{
    public Guid RequestId { get; init; }
    public int ViewsCount { get; init; }
    public int PaymentsCount { get; init; }
    public double ConversionRatio { get; init; }
    public DateTime GeneratedAt { get; init; }
}