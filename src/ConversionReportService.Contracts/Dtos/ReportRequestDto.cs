namespace ConversionReportService.Contracts.Dtos;

public class ReportRequestDto
{
    public int Id { get; init; }
    public int OrderId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}