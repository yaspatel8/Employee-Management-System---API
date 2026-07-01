using EmployeeAPI.Common;
using EmployeeAPI.Data.DBRepositry.Employee;
using EmployeeAPI.Model.Model;
using System;
using System.Collections.Generic;
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
        public async Task<List<EmployeeModel>> GetAllEmployees(CommonPaginationModel model)
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
    }
}
