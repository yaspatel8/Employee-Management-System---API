using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Service.Services.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeAPI.Controllers
{
    [Route("api /[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
            
        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ApiResponseModel> SaveDepartment(DepartmentModel department)
        {
            ApiResponseModel response = new();


            var result = await _departmentService.SaveDepartment(department);
            if (result.Code == (int)DbResponseCode.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, department.DepartmentName);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, department.DepartmentName);
            }

            return response;
        }

        [HttpPost("/GetAllDepartment")]
        public async Task<ApiList<DepartmentModel>> GetAllDepartment([FromBody] CommonPaginationModel model)
        {
            ApiList<DepartmentModel> response = new() { Data = [] };

            var result = await _departmentService.GetAllDepartment(model);
            if (result != null && result.Count > 0)

            {
                response.Success = true;
                response.Message = "Departments retrieved successfully.";
                response.Data = result;
                _logger.LogInformation("Departments retrieved successfully. Count: {Count}", result.Count);
            }
            else
            {
                response.Success = false;
                response.Message = "No departments found.";
                _logger.LogWarning("No departments found.");
            }
            return response;
        }

        //[HttpGet("/GetDepartmentById/{id}")]
        //public async Task<ApiResponse<DepartmentModel?>> GetDepartmentById(int id)
        //{
        //    ApiResponse<DepartmentModel?> response = new();
        //    try
        //    {
        //        var result = await _departmentService.GetDepartmentById(id);
        //        if (result != null)
        //        {
        //            response.Success = true;
        //            response.Message = "Department retrieved successfully.";
        //            response.Data = result;
        //        }
        //        else
        //        {
        //            response.Success = false;
        //            response.Message = "Department not found.";
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = $"An error occurred while retrieving the department: {ex.Message}";
        //        return response;
        //    }
        //}
        //[Authorize(Roles = "admin,manager")]

        [HttpDelete("/DeleteDepartment/{id}")]
        public async Task<ApiResponseModel> DeleteDepartment(int id)
        {
            ApiResponseModel response = new();
            var result = await _departmentService.DeleteDepartment(id);
            if (result.Code == (int)DbResponseCode.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, id);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, id);
            }
            return response;
        }

        [HttpGet("/GetDepartment")]
        public async Task<ApiList<DepartmentModel>> GetDepartment( )
        {
            ApiList<DepartmentModel> response = new();
            var result = await _departmentService.GetDepartment();
            if (result != null && result.Count > 0)
            {
                response.Success = true;
                response.Message = "Departments retrieved successfully.";
                response.Data = result;
                _logger.LogInformation("Departments retrieved successfully. Count: {Count}", result.Count);
            }
            else
            {
                response.Success = false;
                response.Message = "No departments found.";
                _logger.LogWarning("No departments found.");
            }
            return response;
        }
        [HttpPost("/UpdateDepartmentStatus")]
        public async Task<ApiResponseModel> UpdateDepartmentStatus( int departmentId, bool isActive, int updatedBy)
        {
            ApiResponseModel response = new();
            var result = await _departmentService.UpdateDepartmentStatus(departmentId, isActive, updatedBy);
            if (result.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, departmentId);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, departmentId);
            }
            return response;
        }
    }

}
