using EmployeeAPI.Common;
using System;
using System.Net;
using System.Text.Json;

namespace EmployeeAPI.MiddleWare
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                //_logger.LogInformation("Processing request: {Method} {Path}", context.Request.Method, context.Request.Path);
                await _next(context);
                //_logger.LogInformation("Request processed successfully: {Method} {Path}", context.Request.Method, context.Request.Path);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    Success = false,
                    Message = "Something went wrong."
                });
            }
        }

    }
}