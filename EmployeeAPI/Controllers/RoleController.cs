using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Service.Services.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpPost("/CreateRoles")]
        //[Route("Roles/CreateRoles")]
        public async Task<ApiResponseModel> CreateRoles([FromBody] RolesModel roles)
        {
            ApiResponseModel response = new();

            var result = await _roleService.AddRole(roles);
            if (result > 0)
            {
                response.Success = true;
                response.Message = "Role created successfully.";
                _logger.LogInformation($"Role '{roles.RoleName}' created successfully with ID: {result}.");
            }
            else
            {
                response.Success = false;
                response.Message = "Failed to create role.";
                _logger.LogWarning($"Failed to create role: {roles.RoleName}");
            }
            return response;

        }

        [HttpGet("/GetAllRoles")]
        public async Task<ApiList<RolesModel>> GetAllRoles()
        {
            ApiList<RolesModel> response = new();

            var result = await _roleService.GetAllRoles();
            if (result != null && result.Count > 0)
            {
                response.Success = true;
                response.Message = "Roles retrieved successfully.";
                response.Data = result;
                _logger.LogInformation("Roles retrieved successfully. Count: {Count}", result.Count);
            }
            else
            {
                response.Success = false;
                response.Message = "No roles found.";
                _logger.LogWarning("No roles found.");
            }
            return response;
        }
    }
}
