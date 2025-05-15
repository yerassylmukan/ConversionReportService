using ConversionReportService.Contracts.Enums;
using ConversionReportService.Request.Data;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace ConversionReportService.Request.Services;

public class ReportServiceImp : ReportService.ReportServiceBase
{
    private readonly AppDbContext _appDbContext;

    public ReportServiceImp(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public override async Task<GetReportResponse> GetReport(ReportRequest request, ServerCallContext context)
    {
        var report = await _appDbContext.ProductReports.FirstOrDefaultAsync(r =>
            r.ProductId == request.Id && r.StartDate == request.StartDate.ToDateTime().ToUniversalTime() &&
            r.EndDate == request.EndDate.ToDateTime().ToUniversalTime());

        if (report == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product report not found"));
        }

        if (report.Status != nameof(ReportStatus.Completed))
        {
            throw new RpcException(new Status(StatusCode.FailedPrecondition, "Product report not completed"));
        }

        var response = new GetReportResponse
        {
            Id = report.Id,
            ProductId = report.ProductId,
            OrderId = report.OrderId,
            StartDate = Timestamp.FromDateTime(report.StartDate),
            EndDate = Timestamp.FromDateTime(report.EndDate),
            RequestedAt = Timestamp.FromDateTime(report.RequestedAt),
            GeneratedAt = Timestamp.FromDateTime(report.GeneratedAt!.Value),
            Status = report.Status,
            ViewsCount = report.ViewsCount!.Value,
            PaymentsCount = report.PaymentsCount!.Value,
            ConversionRatio = report.ConversionRatio!.Value,
        };
            
        return response;
    }

    public override async Task<GetReportStatusResponse> GetReportStatus(ReportRequest request, ServerCallContext context)
    {
        var report = await _appDbContext.ProductReports.FirstOrDefaultAsync(r =>
            r.ProductId == request.Id && r.StartDate == request.StartDate.ToDateTime().ToUniversalTime() &&
            r.EndDate == request.EndDate.ToDateTime().ToUniversalTime());

        if (report == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Product report not found"));
        }

        var response = new GetReportStatusResponse
        {
            Id = report.Id,
            StartDate = Timestamp.FromDateTime(report.StartDate),
            EndDate = Timestamp.FromDateTime(report.EndDate),
            RequestedAt = Timestamp.FromDateTime(report.RequestedAt),
            Status = report.Status,
        };
            
        return response;
    }

    public override async Task<CreateReportResponse> CreateReport(ReportRequest request, ServerCallContext context)
    {
        var isReportExists = await _appDbContext.ProductReports.AnyAsync(r =>
            r.ProductId == request.Id && r.StartDate == request.StartDate.ToDateTime().ToUniversalTime() &&
            r.EndDate == request.EndDate.ToDateTime().ToUniversalTime());

        if (isReportExists)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, "Product report already exists"));
        }

        var report = new ProductReport
        {
            ProductId = request.Id,
            OrderId = request.OrderId,
            StartDate = request.StartDate.ToDateTime().ToUniversalTime(),
            EndDate = request.EndDate.ToDateTime().ToUniversalTime(),
            Status = nameof(ReportStatus.Queued)
        };
        
        _appDbContext.ProductReports.Add(report);
        await _appDbContext.SaveChangesAsync();

        var response = new CreateReportResponse
        {
            Id = report.Id,
            Status = report.Status
        };
        
        return response;
    }
}