using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Grafana.Loki;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo {
            Title = "Middleware Experiments",
            Version = "v1"
        });
});

builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg
        .WriteTo
        .GrafanaLoki(
            uri: "http://loki:3100/"
            )
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
        .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName);

    if (ctx.HostingEnvironment.IsDevelopment())
    {
        cfg.WriteTo.Console(new CompactJsonFormatter());
    }
});

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        // Edit this line
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template Ting v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(e =>
{
    e.MapMetrics();
    e.MapControllers();
});

app.Run();