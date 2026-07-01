using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Service.Services.Profile;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog.Web.LayoutRenderers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileServices _profileService;
        private readonly ILogger<ProfileController> _logger;
        public ProfileController(IProfileServices profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }

        [HttpPost("/GetProfile/{id}")]
        public async Task<ApiResponse<EmployeeWithDepartmentModel>> GetProfile(int id)
        {
            ApiResponse<EmployeeWithDepartmentModel> response = new();

            var result = await _profileService.GetProfile(id);
            if (result != null)
            {
                response.Success = true;
                response.Message = "Profile Get SuccessFully";
                response.Data = result;
                _logger.LogInformation("Profile Get SuccessFully", result);
            }
            else
            {
                response.Success = false;
                response.Message = "Profile Not Get";
                _logger.LogWarning("Profile Not Get ", result);
            }
            return response;
        }

    }
}