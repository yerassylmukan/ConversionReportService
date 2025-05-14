namespace ConversionReportService.Contracts.Dtos;

public class CreateReportRequestDto
{
    public Guid ProductId { get; init; }
    public Guid OrderId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}