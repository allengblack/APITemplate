using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        // Edit this line
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Template Ting v1"));
}

app.UseSerilogRequestLogging(options =>
    {
        // Customize the message template
        options.MessageTemplate = "Handled {RequestPath}";

        // Emit debug-level events instead of the defaults
        options.GetLevel = (httpContext, elapsed, ex) =>
            LogEventLevel.Debug;

        // Attach additional properties to the request completion event
        options.EnrichDiagnosticContext = (
            diagnosticContext,
            httpContext
        ) =>
        {
            diagnosticContext
                .Set("RequestHost", httpContext.Request.Host.Value);
            diagnosticContext
                .Set("RequestScheme", httpContext.Request.Scheme);
        };
    });

// app.UseMiddleware<LogResponse>();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(e =>
{
    e.MapMetrics();
    e.MapControllers();
});

Log.Logger = new LoggerConfiguration()
                 .MinimumLevel
                 .Override("Microsoft", LogEventLevel.Information)
                 .Enrich
                 .FromLogContext()
                 .WriteTo
                 .Console()
                 .CreateLogger();

try
{
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
