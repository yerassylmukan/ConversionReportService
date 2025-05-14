namespace ConversionReportService.Worker.Data;

public class ProductReport
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime RequestedAt { get; set; }

    public DateTime? GeneratedAt { get; set; }

    public string Status { get; set; } = null!;

    public int? ViewsCount { get; set; }

    public int? PaymentsCount { get; set; }

    public double? ConversionRatio { get; set; }
}