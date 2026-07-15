using EmployeeAPI.Common;
using EmployeeAPI.Data.DBRepositry.Employee;
using EmployeeAPI.Model.Model;
using EmployeeAPI.Model.Model.Export;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeAPI.Service.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepositry _employeeRepositry;
        public EmployeeService(IEmployeeRepositry employeeRepositry)
        {
            _employeeRepositry = employeeRepositry;
        }

        public async Task<DbResponseModel> SaveEmployee (EmployeeModel employee)
        {
            return await _employeeRepositry.SaveEmployee(employee);
        }

        //public async Task<int> AddEmployee(EmployeeModel employee)
        //{
        //    return await _employeeRepositry.AddEmployee(employee);
        //}
        public async Task<List<EmployeeModel>> GetAllEmployees(UseridBasedModel model)
        {
            return await _employeeRepositry.GetAllEmployees(model);
        }
        //public async Task<EmployeeModel?> GetEmployeeById(int id)
        //{
        //    return await _employeeRepositry.GetEmployeeById(id);
        //}
        //public async Task<int> UpdateEmployee(EmployeeModel employee)
        //{
        //    return await _employeeRepositry.UpdateEmployee(employee);
        //}
        public async Task<DbResponseModel> DeleteEmployee(int id)
        {
            return await _employeeRepositry.DeleteEmployee(id);
        }
        public async Task<List<EmployeeWithDepartmentModel>> GetEmployeesWithDepartment()
        {
            return await _employeeRepositry.GetEmployeesWithDepartment();
        }
        public async Task<BulkDbResponseModel> BulkSaveEmployees(List<EmployeeModel> employees)
        {
            return await _employeeRepositry.BulkSaveEmployees(employees);
        }
        public async Task SendEmployeeCreatedEmailAsync(string toEmail, string fullName, string password, string loginLink)
        {
            await _employeeRepositry.SendEmployeeCreatedEmailAsync( toEmail,  fullName,  password,  loginLink);
        }
        public async Task<ApiResponseModel> BulkDeleteEmployees(BulkDeleteEmployeeModel model)
        {
            return await _employeeRepositry.BulkDeleteEmployees(model);
        }
        public async Task<ApiResponseModel> ChangeEmployeeStatus(int employeeId, bool isActive, int updatedBy)
        {
            return await _employeeRepositry.ChangeEmployeeStatus(employeeId, isActive, updatedBy);
        }
        public async Task<BulkDbResponseModel> BulkUpdateEmployees(List<BulkUpdateEmployeeModel> employees)
        {
            return await _employeeRepositry.BulkUpdateEmployees(employees);
        }
        public async Task<List<EmployeeExportModel>> ExportEmployees(List<int> ids)
        {
            return await _employeeRepositry.ExportEmployees(ids);
        }
        public async Task<List<ManagerDropdownModel>> GetManagerDropdown(int departmentId, int positionId)
        {
            return await _employeeRepositry.GetManagerDropdown(departmentId, positionId);
        }
    }
}
