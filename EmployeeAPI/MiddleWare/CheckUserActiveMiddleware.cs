using System.Data;
using System.Security.Claims;
using Dapper;

namespace EmployeeAPI.MiddleWare
{
    public class CheckUserActiveMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CheckUserActiveMiddleware> _logger;

        public CheckUserActiveMiddleware(
            RequestDelegate next,
            ILogger<CheckUserActiveMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IDbConnection db)
        {
            // Skip if user is not authenticated
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst("UserId")?.Value;
                var fullName = context.User.FindFirst("FullName")?.Value;
                var email = context.User.FindFirst("Email")?.Value;
                var role = context.User.FindFirst("Role")?.Value;

                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out int userId))
                {
                    _logger.LogInformation("Request from {FullName} ({Email}) with Role: {Role}", fullName, email, role);
                    //int userId = Convert.ToInt32(userIdClaim);

                    bool? isActive = await db.QueryFirstOrDefaultAsync<bool?>(
                        @"SELECT IsActive
                          FROM Users
                          WHERE UserId = @UserId",
                        new
                        {
                            UserId = userId
                        });

                    if (isActive != true)
                    {
                        _logger.LogWarning(
                            "Inactive user attempted to access API. UserId: {UserId}",
                            userId);

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            Success = false,
                            Message = "Your account has been deactivated. Please contact Administrator."
                        });

                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}