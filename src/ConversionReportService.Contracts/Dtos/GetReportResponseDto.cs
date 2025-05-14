namespace ConversionReportService.Contracts.Dtos;

public class GetReportResponseDto
{
    public Guid RequestId { get; init; }
    public int ViewsCount { get; init; }
    public int PaymentsCount { get; init; }
    public double ConversionRatio { get; init; }
    public string Status { get; init; }
}