using ConversionReportService.Contracts.Dtos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace ConversionReportService.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ReportController : ControllerBase
{
    private readonly ILogger<ReportController> _logger;
    private readonly ReportService.ReportServiceClient _reportServiceClient;

    public ReportController(ILogger<ReportController> logger, ReportService.ReportServiceClient reportServiceClient)
    {
        _logger = logger;
        _reportServiceClient = reportServiceClient;
    }

    [HttpGet]
    public async Task<ActionResult<GetReportResponseDto>> GetReport([FromQuery] ReportRequestDto request)
    {
        try
        {
            var response = await _reportServiceClient.GetReportAsync(new ReportRequest
            {
                Id = request.Id,
                OrderId = request.OrderId,
                StartDate = Timestamp.FromDateTime(request.StartDate),
                EndDate = Timestamp.FromDateTime(request.EndDate)
            });

            var result = new GetReportResponseDto
            {
                Id = response.Id,
                ProductId = response.ProductId,
                OrderId = response.OrderId,
                StartDate = response.StartDate.ToDateTime().ToUniversalTime(),
                EndDate = response.EndDate.ToDateTime().ToUniversalTime(),
                RequestedAt = response.RequestedAt.ToDateTime().ToUniversalTime(),
                GeneratedAt = response.GeneratedAt.ToDateTime().ToUniversalTime(),
                Status = response.Status,
                ViewsCount = response.ViewsCount,
                PaymentsCount = response.PaymentsCount,
                ConversionRatio = response.ConversionRatio
            };

            return Ok(result);
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            _logger.LogWarning("Report not found. RequestId: {RequestId}", request.Id);
            return NotFound(new
            {
                Message = ex.Status.Detail,
                RequestId = request.Id
            });
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.FailedPrecondition)
        {
            _logger.LogInformation("Report not ready yet. RequestId: {RequestId}", request.Id);
            return Accepted(new
            {
                Message = "Report is still being generated. Try again later.",
                RequestId = request.Id
            });
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Unexpected gRPC error while fetching report {RequestId}", request.Id);
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred while processing your request.",
                Details = ex.Status.Detail
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<GetReportStatusResponseDto>> GetReportStatus([FromQuery] ReportRequestDto request)
    {
        try
        {
            var response = await _reportServiceClient.GetReportAsync(new ReportRequest
            {
                Id = request.Id,
                OrderId = request.OrderId,
                StartDate = Timestamp.FromDateTime(request.StartDate),
                EndDate = Timestamp.FromDateTime(request.EndDate)
            });

            var result = new GetReportStatusResponseDto
            {
                Id = response.Id,
                StartDate = response.StartDate.ToDateTime().ToUniversalTime(),
                EndDate = response.EndDate.ToDateTime().ToUniversalTime(),
                RequestedAt = response.RequestedAt.ToDateTime().ToUniversalTime(),
                Status = response.Status
            };

            return Ok(result);
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            _logger.LogWarning("Report not found. RequestId: {RequestId}", request.Id);
            return NotFound(new
            {
                Message = ex.Status.Detail,
                RequestId = request.Id
            });
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Unexpected gRPC error while fetching report {RequestId}", request.Id);
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred while processing your request.",
                Details = ex.Status.Detail
            });
        }
    }

    [HttpGet]
    public async Task<ActionResult<CreateReportResponse>> CreateReport([FromQuery] ReportRequestDto request)
    {
        try
        {
            var response = await _reportServiceClient.CreateReportAsync(new ReportRequest
            {
                Id = request.Id,
                OrderId = request.OrderId,
                StartDate = Timestamp.FromDateTime(request.StartDate),
                EndDate = Timestamp.FromDateTime(request.EndDate)
            });

            var result = new CreateReportResponseDto
            {
                Id = response.Id,
                Status = response.Status
            };

            return Ok(result);
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.NotFound)
        {
            _logger.LogWarning("Report not found. RequestId: {RequestId}", request.Id);
            return NotFound(new
            {
                Message = ex.Status.Detail,
                RequestId = request.Id
            });
        }
        catch (RpcException ex) when (ex.StatusCode == Grpc.Core.StatusCode.AlreadyExists)
        {
            _logger.LogWarning("Report already exists. RequestId: {RequestId}", request.Id);
            return Conflict(new
            {
                Message = ex.Status.Detail,
                RequestId = request.Id
            });
        }
        catch (RpcException ex)
        {
            _logger.LogError(ex, "Unexpected gRPC error while fetching report {RequestId}", request.Id);
            return StatusCode(500, new
            {
                Message = "An unexpected error occurred while processing your request.",
                Details = ex.Status.Detail
            });
        }
    }
}