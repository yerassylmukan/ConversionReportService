using Microsoft.EntityFrameworkCore;

namespace ConversionReportService.Worker.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ProductReport> ProductReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductReport>(entity =>
        {
            entity.ToTable("product_reports");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConversionRatio).HasColumnName("conversion_ratio");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.GeneratedAt).HasColumnName("generated_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentsCount).HasColumnName("payments_count");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.RequestedAt).HasColumnName("requested_at");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.ViewsCount).HasColumnName("views_count");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}