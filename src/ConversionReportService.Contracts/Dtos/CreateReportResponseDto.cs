namespace ConversionReportService.Contracts.Dtos;

public class CreateReportResponseDto
{
    public Guid RequestId { get; init; }
    public string Status { get; init; }
}