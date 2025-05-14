namespace ConversionReportService.Contracts.Dtos;

public class ReportResultDto
{
    public int ViewsCount { get; init; }
    public int PaymentsCount { get; init; }
    public double ConversionRatio { get; init; }
    public DateTime GeneratedAt { get; init; }
}