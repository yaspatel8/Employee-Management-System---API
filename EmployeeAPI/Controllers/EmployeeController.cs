using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Service.Services.Department;
using EmployeeAPI.Service.Services.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpPost("/SaveEmployee")]

        public async Task<ApiResponseModel> SaveEmployee([FromForm] EmployeeModel employee)
        {

            ApiResponseModel response = new();

            // Upload folder
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(),"Documents","ProfileImage");

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // File Upload
            if (employee.ProfilePicture != null)
            {
                string extension = Path.GetExtension(employee.ProfilePicture.FileName).ToLower();

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    response.Success = false;
                    response.Message = "Only jpg, jpeg and png files are allowed.";

                    return response;
                }
                const long maxFileSize = 2 * 1024 * 1024;

                if (employee.ProfilePicture.Length > maxFileSize)
                {
                    response.Success = false;
                    response.Message = "Maximum image size is 2 MB.";

                    return response;
                }

                // Generate unique filename
                string uniqueFileName = $"{Guid.NewGuid()}{extension}";

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {   
                    await employee.ProfilePicture.CopyToAsync(stream);
                }

                employee.ProfileImage = uniqueFileName;
            }

            // Default Password only for Add
            if (employee.EmployeeId == 0)
            {
                employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            }
            var result = await _employeeService.SaveEmployee(employee);

            if (result.Code == (int)DbResponseCode.Success)
            {

                // Delete old image after successful update
                if (employee.EmployeeId > 0 && !string.IsNullOrEmpty(result.OldFileName) && employee.ProfilePicture != null)
                {
                    string oldPath = Path.Combine(uploadsFolder, result.OldFileName);

                    if (System.IO.File.Exists(oldPath))
                    {
                        try
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex.Message);
                        }
                    }
                }
                response.Success = true;
                response.Message = result.Message;
                _logger.LogInformation(result.Message, result);
            }
            else if (result.Code == (int)DbResponseCode.AlreadyExists)
            {
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, result);
            }
            else
            {
                // SP failed, delete newly uploaded image
                if (!string.IsNullOrEmpty(employee.ProfileImage))
                {
                    string newPath = Path.Combine(uploadsFolder, employee.ProfileImage);

                    if (System.IO.File.Exists(newPath))
                    {
                        System.IO.File.Delete(newPath);
                    }
                }
                response.Success = false;
                response.Message = result.Message;
                _logger.LogWarning(result.Message, result);
            }
            return response;
        }

        //[HttpPost("/CreateEmployee")]
        //public async Task<ApiResponseModel> CreateEmployee(EmployeeModel employee)
        //{
        //    ApiResponseModel response = new();
        //    try
        //    {
        //        var result = await _employeeService.AddEmployee(employee);
        //        if (result > 0)
        //        {
        //            response.Success = true;
        //            response.Message = "Employee created successfully.";
        //        }
        //        else
        //        {
        //            response.Success = false;
        //            response.Message = "Failed to create employee.";
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = $"An error occurred while creating the employee: {ex.Message}";
        //        return response;
        //    }
        //}

        [HttpPost("/GetAllEmployees")]
        public async Task<ApiList<EmployeeModel>> GetAllEmployees([FromBody] CommonPaginationModel model)
        {
            ApiList<EmployeeModel> response = new() { Data = [] };

            var result = await _employeeService.GetAllEmployees(model);
            if (result != null && result.Count > 0)
            {
                response.Success = true;
                response.Message = "Employees retrieved successfully.";
                response.Data = result;
                _logger.LogInformation("Retrieved {EmployeeCount} employees successfully.", result.Count);
            }
            else
            {
                response.Success = false;
                response.Message = "No employees found.";
                _logger.LogWarning("No employees found.");
            }
            return response;

        }
        //[HttpGet("/GetEmployeeById/{id}")]
        //public async Task<ApiResponse<EmployeeModel?>> GetEmployeeById(int id)
        //{
        //    ApiResponse<EmployeeModel?> response = new();
        //    try
        //    {
        //        var result = await _employeeService.GetEmployeeById(id);
        //        if (result != null)
        //        {
        //            response.Success = true;
        //            response.Message = "Employee retrieved successfully.";
        //            response.Data = result;
        //        }
        //        else
        //        {
        //            response.Success = false;
        //            response.Message = "Employee not found.";
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = $"An error occurred while retrieving the employee: {ex.Message}";
        //        return response;
        //    }
        //}
        //[HttpPut("/UpdateEmployee")]
        //public async Task<ApiResponseModel> UpdateEmployee(EmployeeModel employee)
        //{
        //    ApiResponseModel response = new();
        //    try
        //    {
        //        var result = await _employeeService.UpdateEmployee(employee);
        //        if (result > 0)
        //        {
        //            response.Success = true;
        //            response.Message = "Employee updated successfully.";
        //        }
        //        else
        //        {
        //            response.Success = false;
        //            response.Message = "Failed to update employee.";
        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Success = false;
        //        response.Message = $"An error occurred while updating the employee: {ex.Message}";
        //        return response;
        //    }
        //}
        [HttpDelete("/DeleteEmployee/{id}")]
        public async Task<ApiResponseModel> DeleteEmployee(int id)
        {
            ApiResponseModel response = new();

            var result = await _employeeService.DeleteEmployee(id);
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
        [HttpGet("/GetEmployeesWithDepartment")]
        public async Task<ApiResponse<List<EmployeeWithDepartmentModel>>> GetEmployeesWithDepartment()
        {
            ApiResponse<List<EmployeeWithDepartmentModel>> response = new();

            var result = await _employeeService.GetEmployeesWithDepartment();
            if (result != null)
            {
                response.Success = true;
                response.Message = "Employee with department retrieved successfully.";
                response.Data = result;
                _logger.LogInformation("Employee with department retrieved successfully. Count: {Count}", result.Count);
            }
            else
            {
                response.Success = false;
                response.Message = "Employee with department not found.";
                _logger.LogWarning("Employee with department not found.");
            }
            return response;
        
        }
        [HttpPost("/BulkSaveEmployees")]
        public async Task<BulkDbResponseModel> BulkSaveEmployees([FromBody] List<EmployeeModel> employees)
        {
            BulkDbResponseModel response = new();
            foreach (var employee in employees)
            {
                employee.PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123");
            }
            var result = await _employeeService.BulkSaveEmployees(employees);
            if (result.Code == (int)DbResponseCode.Success)
            {
                response.Success = true;
                response.Message = result.Message;
                response.InsertedCount = result.InsertedCount;
                response.SkippedCount = result.SkippedCount;
                response.DuplicateEmails = result.DuplicateEmails;
                _logger.LogInformation("Bulk employee upload completed. Message: {Message}", result.Message,result);
            }
            else if(result.Code == (int)DbResponseCode.AlreadyExists)
            {
                response.Success = false;
                response.Message = result.Message;
                response.InsertedCount = result.InsertedCount;
                response.SkippedCount = result.SkippedCount;
                response.DuplicateEmails = result.DuplicateEmails;
                _logger.LogWarning("Bulk employee upload failed. Message: {Message}", result.Message, result);
            }
            else
            {
                response.Success = false;
                response.Message = result.Message;
                response.InsertedCount = result.InsertedCount;
                response.SkippedCount = result.SkippedCount;
                response.DuplicateEmails = result.DuplicateEmails;

                _logger.LogWarning("Bulk employee upload failed. Message: {Message}", result.Message, result);
            }
            return response;
        }
    }
}
