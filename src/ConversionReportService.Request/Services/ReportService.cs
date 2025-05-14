using Grpc.Core;

namespace ConversionReportService.Request.Services;

public class ReportServiceImp : ReportService.ReportServiceBase
{
    public override Task<GetReportResponse> GetReport(ReportRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<GetReportStatusResponse> GetReportStatus(ReportRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }

    public override Task<CreateReportResponse> CreateReport(ReportRequest request, ServerCallContext context)
    {
        throw new NotImplementedException();
    }
}