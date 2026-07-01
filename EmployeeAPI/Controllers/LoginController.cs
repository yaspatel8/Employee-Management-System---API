using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Service.Services.Login;
using EmployeeAPI.Service.Services.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginServices _loginService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILoginServices loginService, IConfiguration configuration, ILogger<LoginController> logger)
        {
            _loginService = loginService;
            _configuration = configuration;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost("/Login")]
        public async Task<ActionResult<ApiResponse<LoginModel>>> Login([FromBody] LoginModel login)
        {
            ApiResponse<LoginModel> response = new();

            var user = await _loginService.LoginUser(login);

            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid Email or Password";
                _logger.LogWarning("Login failed for email: {Email}. User not found.", login.Email);
                return Unauthorized(response);
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(login.PasswordHash, user.PasswordHash);

            if (!isValidPassword)
            {
                response.Success = false;
                response.Message = "Invalid Password";
                _logger.LogWarning("Login failed for email: {Email}. Invalid password.", login.Email);
                return Unauthorized(response);
            }

            string token = JWTToken.GenerateToken(user.UserId, user.FullName, user.Email, user.RoleName, _configuration["Jwt:Key"]);
            response.Success = true;
            response.Message = "Login Successful";
            _logger.LogInformation("Login successful for email: {Email}.", login.Email);

            return Ok(new { Success=response.Success, Message = response.Message, Token = token });

        }

    }
}
