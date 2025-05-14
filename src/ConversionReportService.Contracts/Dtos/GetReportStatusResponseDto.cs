namespace ConversionReportService.Contracts.Dtos;

public class GetReportStatusResponseDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime RequestedAt { get; set; }
    public string Status { get; set; }
}