namespace ConversionReportService.Contracts.Messages;

public class ReportRequested
{
    public Guid RequestId { get; init; }
    public Guid ProductId { get; init; }
    public Guid OrderId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}