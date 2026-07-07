using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Employee
{
    public interface IEmployeeService
    {

        Task<DbResponseModel> SaveEmployee(EmployeeModel employee);
        //Task<int> AddEmployee(EmployeeModel employee);
        Task<List<EmployeeModel>> GetAllEmployees(CommonPaginationModel model);
        //Task<EmployeeModel?> GetEmployeeById(int id);
        //Task<int> UpdateEmployee(EmployeeModel employee);
        Task<DbResponseModel> DeleteEmployee(int id);
        Task<List<EmployeeWithDepartmentModel>> GetEmployeesWithDepartment();
        Task<BulkDbResponseModel> BulkSaveEmployees(List<EmployeeModel> employees);
        Task SendEmployeeCreatedEmailAsync(string toEmail, string fullName, string password, string loginLink);
        Task<ApiResponseModel> BulkDeleteEmployees(BulkDeleteEmployeeModel model);
        Task<ApiResponseModel> ChangeEmployeeStatus(int employeeId, bool isActive, int updatedBy);
    }
}
