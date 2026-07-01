using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Service.Services.Employee;
using EmployeeAPI.Service.Services.Registor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistorServices _registorServices;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(IRegistorServices registorServices, ILogger<RegistrationController> logger)
        {
            _registorServices = registorServices;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost("/RegisterUser")]
        public async Task<ApiResponseModel> RegisterUser(UserModel user)
        {
            ApiResponseModel response = new();

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            var result = await _registorServices.SaveRegister(user);

            //defalut profile picture for user
            
            if (result.Code == (int)DbResponseCode.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, user);
            }
            else if (result.Code == (int)DbResponseCode.AlreadyExists)
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, user);
            }
            else
            {
                response.Success = false;
                response.Message = "Failed to register user.";
                _logger.LogWarning("Failed to register user: {user}", user);
            }
            return response;

        }
    }
}
