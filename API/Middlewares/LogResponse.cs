using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace APITemplate.Middlewares
{
    public class LogResponse
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogResponse> _logger;

        public LogResponse(RequestDelegate next, ILogger<LogResponse> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stream originalBody = context.Response.Body;

            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);
                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();

                    _logger.LogInformation(responseBody);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}");
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}