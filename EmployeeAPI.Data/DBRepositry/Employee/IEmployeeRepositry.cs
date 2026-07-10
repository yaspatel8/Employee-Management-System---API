using EmployeeAPI.Common;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Model.Model.Export;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Data.DBRepositry.Employee
{
    public interface IEmployeeRepositry
    {
        Task<DbResponseModel> SaveEmployee(EmployeeModel employee);

        //Task<int> AddEmployee(EmployeeModel employee);
        Task<List<EmployeeModel>> GetAllEmployees(CommonPaginationModel model);
        //Task<EmployeeModel?> GetEmployeeById(int id);
        //Task<int> UpdateEmployee(EmployeeModel employee);
        Task<DbResponseModel> DeleteEmployee(int id);
        Task<List<EmployeeWithDepartmentModel>> GetEmployeesWithDepartment();

        Task<BulkDbResponseModel> BulkSaveEmployees(List<EmployeeModel> employees);
        Task SendEmployeeCreatedEmailAsync(string toEmail,string fullName,string password,string loginLink);
        
        Task<ApiResponseModel> BulkDeleteEmployees(BulkDeleteEmployeeModel model);

        Task<ApiResponseModel> ChangeEmployeeStatus(int employeeId, bool isActive,int updatedBy);
        Task<BulkDbResponseModel> BulkUpdateEmployees(List<BulkUpdateEmployeeModel> employees);
        Task<List<EmployeeExportModel>> ExportEmployees(List<int> ids);
        Task<List<ManagerDropdownModel>> GetManagerDropdown(int departmentId, int positionId);
    }
}
