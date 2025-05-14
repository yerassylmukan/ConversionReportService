namespace ConversionReportService.Contracts.Dtos;

public class GetReportResponseDto
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public int OrderId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime RequestedAt { get; set; }
    public DateTime GeneratedAt { get; set; }
    public string Status { get; init; }
    public int ViewsCount { get; init; }
    public int PaymentsCount { get; init; }
    public double ConversionRatio { get; init; }
}