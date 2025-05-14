using ConversionReportService.Request.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<ReportServiceImp>();

app.UseHttpsRedirection();

app.Run();