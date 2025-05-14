using ConversionReportService.Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<ReportService.ReportServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["ServerUrl"]!);
});

var app = builder.Build();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();

app.UseSwaggerUI();

app.MapControllers();

app.Run();