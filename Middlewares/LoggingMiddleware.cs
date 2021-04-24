using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MiddlewareExperiments.Middlewares
{
   public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerFactory _loggerFactory;


        public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var _logger = _loggerFactory.CreateLogger("request logger");

            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(new 
                {
                    Path = context.Request.PathBase.ToString(),
                    RequestType = context.Request.GetType(),
                }));
 
                await _next(context);
 
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex.Message}");
            }
        }
    }
}